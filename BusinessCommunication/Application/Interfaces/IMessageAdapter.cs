using Domain.Dtos.Response;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IMessageAdapter
    {
        Task<ResponseDto<SendMessageToUserResponseDto>> SendMessageToUserAsync(Message message);
        Task<IEnumerable<MessageData>> GetMessageHistory(string id);

        Task<SendMessageToBotResponseDto> SendMessageToBotAsync(string stock, string messageHistory, string actualMessage);
        Task<MessageData> SaveMessage(string messageId, string receivedMessage, string senderPhone, string responseMessage);
    }
}
