using System.Text.Json.Serialization;

namespace Domain.Dtos.Request
{
    public class WebHookRequestDto
    {
        public string Object { get; set; }
        [JsonPropertyName("entry")]
        public IEnumerable<Entry> Entry { get; set; }
    }

    public class Entry
    {
        public string Id { get; set; } = string.Empty;
        public IEnumerable<Change> Changes { get; set; }
    }

    public class Change
    {
        public Value Value { get; set; }
        public string Field { get; set; } = string.Empty;
    }

    public class Value
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = string.Empty;
        public Metadata Metadata { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("display_phone_number")]
        public string DisplayPhoneNumber { get; set; } = string.Empty;
        [JsonPropertyName("phone_number_id")]
        public string PhoneNumberId { get; set; } = string.Empty;
    }

    public class Contact
    {
        public Profile Profile { get; set; }
        [JsonPropertyName("wa_id")]
        public string WhatsappId { get; set; } = string.Empty;
    }

    public class Profile
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Message
    {
        public string From { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public Text Text { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
