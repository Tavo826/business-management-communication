using System.Text.Json.Serialization;

namespace Domain.Dtos.Request
{
    public class SendMessageToUserDto
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = string.Empty;
        [JsonPropertyName("recipient_type")]
        public string RecipientType { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Text Text { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("preview_url")]
        public bool PreviewUrl { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
