namespace Domain.Dtos.Request
{    
    public class SendMessageToBotDto
    {
        public List<Content> Contents { get; set; }
    }

    public class Content
    {
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        public string Text { get; set; } = string.Empty;
    }
    
}
