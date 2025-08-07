namespace Domain.Dtos.Request
{
    public class SendMessageRequestDto
    {
        public string SenderPhone { get; set; } = string.Empty;
        public string SenderPhoneId { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
    }
}
