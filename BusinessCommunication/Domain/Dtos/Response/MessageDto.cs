namespace Domain.Dtos.Response
{
    public class MessageHistoryDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public IEnumerable<MessageData> Data { get; set; }
    }

    public class MessageDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public MessageData Data { get; set; }
    }

    public class MessageData
    {
        public Guid Id { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public string ReceivedMessage { get; set; } = string.Empty;
        public string SenderPhone { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
