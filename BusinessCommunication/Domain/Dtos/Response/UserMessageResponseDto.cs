using System.Text.Json.Serialization;

namespace Domain.Dtos.Response
{
    public class SendMessageToUserResponseDto
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = string.Empty;
        [JsonPropertyName("contacts")]
        public IEnumerable<Contact> Contacts { get; set; }
        [JsonPropertyName("messages")]
        public IEnumerable<MessageResponse> Messages { get; set; }
    }

    public class Contact
    {
        [JsonPropertyName("input")]
        public string Input { get; set; }
        [JsonPropertyName("wa_id")]
        public string WaId { get; set; } = string.Empty;
    }

    public class MessageResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
