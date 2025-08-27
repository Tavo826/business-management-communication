using Application.Interfaces;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Adapter
{
    public class MessageAdapter : IMessageAdapter
    {
        private readonly Settings _settings;
        private readonly IHttpAdapter _httpAdapter;
        private readonly ILogger<MessageAdapter> _logger;

        public MessageAdapter(IOptions<Settings> settings, IHttpAdapter httpAdapter, ILogger<MessageAdapter> logger)
        {
            _settings = settings.Value;
            _httpAdapter = httpAdapter;
            _logger = logger;
        }

        public async Task<ResponseDto<SendMessageToUserResponseDto>> SendMessageToUserAsync(Domain.Models.Message message)
        {
            try
            {
                string url = _settings.HttpSettings.ApiMetaUrl + $"/{message.SenderPhoneId}/messages";
                string token = _settings.HttpSettings.MetaAccessKey;
                string messageProduct = _settings.CommunicationSettings.MessageProduct;
                string recipientType = _settings.CommunicationSettings.RecipientType;
                string messageType = _settings.CommunicationSettings.MessageType;

                var request = new SendMessageToUserDto()
                {
                    MessagingProduct = messageProduct,
                    RecipientType = recipientType,
                    To = message.SenderPhone,
                    Type = messageType,
                    Text = new Text
                    {
                        Body = message.TextMessage,
                        PreviewUrl = false
                    }
                };

                var headers = new Dictionary<string, string>();
                headers.Add("Authorization", string.Format("Bearer {0}", token));

                var httpResponse = await _httpAdapter.ExecutePostWithHttpClientAsync(url, headers, request);

                var response = new ResponseDto<SendMessageToUserResponseDto>
                {
                    StatusCode = (int)httpResponse.StatusCode
                };

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Success = true;
                    response.Message = "Message successfully processed";

                    response.Data = httpResponse.Content.ReadFromJsonAsync<SendMessageToUserResponseDto>().Result;
                }
                else
                {
                    response.Success = false;
                    response.Message = "An error ocurred while sendind the message";
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while sendind the message");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MessageData>> GetMessageHistory(string id)
        {

            try
            {
                string url = _settings.HttpSettings.BusinessManagerPersistanceUrl + $"/Message/GetMessageById/{id}";

                var response = await _httpAdapter.ExecuteGetWithHttpClientAsync(url);

                var result = response.Content.ReadFromJsonAsync<MessageHistoryDto>().Result;

                return result.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while getting the message history");
                throw new Exception(ex.Message);
            }
        }

        public async Task<SendMessageToBotResponseDto> SendMessageToBotAsync(string stock, string messageHistory, string actualMessage)
        {
            try
            {
                string url = _settings.HttpSettings.ApiModelUrl;
                string token = _settings.HttpSettings.GoogleApiKey;

                using StreamReader reader = new StreamReader("..\\BusinessCommunication\\prompt.txt");

                var systemPrompt = await reader.ReadToEndAsync();

                var finalPrompt = $"""
                 [SYSTEM PROMPT]
                 {systemPrompt}

                 [CATALOGO]
                 Este es el catálogo actual del negocio:
                 {stock}

                 [HISTORIAL DE CHAT]
                 {messageHistory}

                 [MENSAJE ACTUAL]
                 {actualMessage}
                 """;


                var request = new SendMessageToBotDto
                {
                    Contents = new List<Content>()
                    {
                        new Content
                        {
                            Parts = new List<Part>
                            {
                                new Part
                                {
                                    Text = finalPrompt
                                }
                            }
                        }
                    }
                };

                var headers = new Dictionary<string, string>();
                headers.Add("X-goog-api-key", string.Format(token));

                var response = await _httpAdapter.ExecutePostWithHttpClientAsync(url, headers, request);

                return response.Content.ReadFromJsonAsync<SendMessageToBotResponseDto>().Result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while getting the model answer");
                throw new Exception(ex.Message);
            }

        }

        public async Task<MessageData> SaveMessage(string messageId, string receivedMessage, string senderPhone, string responseMessage)
        {

            try
            {
                string url = _settings.HttpSettings.BusinessManagerPersistanceUrl + $"/Message/CreateMessage";

                var request = new SaveMessageRequestDto
                {
                    MessageId = messageId,
                    ReceivedMessage = receivedMessage,
                    SenderPhone = senderPhone,
                    ResponseMessage = responseMessage
                };

                var response = await _httpAdapter.ExecutePostWithHttpClientAsync(url, new Dictionary<string, string>(), request);

                var result = response.Content.ReadFromJsonAsync<MessageDto>().Result;

                return result.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while saving the message");
                throw new Exception(ex.Message);
            }

        }
    }
