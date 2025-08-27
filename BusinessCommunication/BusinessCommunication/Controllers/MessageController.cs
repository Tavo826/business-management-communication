using Application.Interfaces;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessCommunication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly Settings _settings;

        public MessageController(IOptions<Settings> settings, IMessageService messageService)
        {
            _settings = settings.Value;
            _messageService = messageService;
        }

        [HttpGet]
        [Route("webhook")]
        public string Webhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.challenge")] string challenge,
            [FromQuery(Name = "hub.verify_token")] string verify_token
        )
        {
            if (verify_token.Equals(_settings.HttpSettings.MetaKeyId))
            {
                return challenge;
            }
            else
            {
                return "";
            }
        }

        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> GetMessageWebHook([FromBody] WebHookRequestDto webHookRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Console.WriteLine(Convert.ToString(webHookRequestDto));
                var messageInformation = _messageService.SendToBotAndSaveMessage(webHookRequestDto);

                var response = await _messageService.SendMessageToUserAsync(messageInformation);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = "An error occurred while getting the message " + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequestDto sendMessageRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _messageService.SendMessageToUserAsync(sendMessageRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = "An error ocurred while sending the message " + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                });
            }

        }
    }
}
