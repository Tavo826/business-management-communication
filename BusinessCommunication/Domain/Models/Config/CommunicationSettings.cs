namespace Domain.Models.Config
{
    public class CommunicationSettings
    {
        public string MessageProduct { get; set; } = string.Empty;
        public string RecipientType { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public GoogleSheetsSettings GoogleSheetsSettings { get; set; }
    }
}
