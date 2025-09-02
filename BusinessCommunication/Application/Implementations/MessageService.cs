using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageAdapter _messageAdapter;
        private readonly IStockAdapter _stockAdapter;
        private readonly ILogger<MessageService> _logger;
        private readonly IMapper _mapper;

        public MessageService(IMessageAdapter messageAdapter, IStockAdapter stockAdapter, ILogger<MessageService> logger, IMapper mapper)
        {
            _messageAdapter = messageAdapter;
            _stockAdapter = stockAdapter;
            _logger = logger;
            _mapper = mapper;
        }

        public SendMessageRequestDto SendToBotAndSaveMessage(WebHookRequestDto webHookRequestDto)
        {
            try
            {

                string messageHistoryString = "";

                string conversationId = webHookRequestDto.Entry.First().Id;
                string actualMessage = webHookRequestDto.Entry.First().Changes.First().Value.Messages.First().Text.Body;
                string phoneNumber = webHookRequestDto.Entry.First().Changes.First().Value.Messages.First().From;
                string phoneNumberId = webHookRequestDto.Entry.First().Changes.First().Value.Metadata.PhoneNumberId;

                var stock = _stockAdapter.GetStockData("1zBgltJRX1RQk40fUWJc1uWxCV_NuoaT0EuLetGvORoI", "Productos").Result;
                var stockString = PromptConverter.GetProductsString(stock);

                var messageHistory = _messageAdapter.GetMessageHistory(conversationId).Result;

                if (messageHistory != null)
                {
                    messageHistoryString = PromptConverter.GetMessageHistoryString(messageHistory);
                }

                var actualMessageString = PromptConverter.GetActualMessageString(actualMessage);

                var botResponse = _messageAdapter.SendMessageToBotAsync(stockString, messageHistoryString, actualMessageString).Result;
                string botMessageResponse = botResponse.Candidates.First().Content.Parts.First().Text;

                var savedMessage = _messageAdapter.SaveMessage(conversationId, actualMessage, phoneNumber, botMessageResponse);

                var messageInformation = new SendMessageRequestDto
                {
                    SenderPhone = phoneNumber,
                    SenderPhoneId = phoneNumberId,
                    TextMessage = botMessageResponse,
                };

                return messageInformation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred in communication");
                throw new Exception("An error ocurred in communication");
            }

        }

        public Task<ResponseDto<SendMessageToUserResponseDto>> SendMessageToUserAsync(SendMessageRequestDto sendMessageRequest)
        {
            try
            {
                var messageToSend = _mapper.Map<Domain.Models.Message>(sendMessageRequest);

                return _messageAdapter.SendMessageToUserAsync(messageToSend);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while sending the messsage");
                throw new Exception("An error ocurred while sending the messsage");
            }
        }
    }
}
