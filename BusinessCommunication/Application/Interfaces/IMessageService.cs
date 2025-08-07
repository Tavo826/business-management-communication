using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        SendMessageRequestDto SendToBotAndSaveMessage(WebHookRequestDto webHookRequestDto);
        Task<ResponseDto<SendMessageToUserResponseDto>> SendMessageToUserAsync(SendMessageRequestDto sendMessageRequest);
    }
}
