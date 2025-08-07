namespace Domain.Models.Config
{
    public class HttpSettings
    {
        public string ApiMetaUrl { get; set; } = string.Empty;
        public string BusinessManagerPersistanceUrl { get; set; } = string.Empty;
        public string ApiModelUrl { get; set; } = string.Empty;
        public string TokenMeta { get; set; } = string.Empty;
        public string TokenKeyMeta { get; set; } = string.Empty;
        public string TokenGoogle { get; set; } = string.Empty;
    }
}
