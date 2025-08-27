namespace Domain.Dtos.Request
{
    public class SaveMessageRequestDto
    {
        public string MessageId { get; set; } = string.Empty;
        public string ReceivedMessage { get; set; } = string.Empty;
        public string SenderPhone { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
    }
}
