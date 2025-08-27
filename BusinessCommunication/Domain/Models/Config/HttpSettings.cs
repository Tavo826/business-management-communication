namespace Domain.Models.Config
{
    public class HttpSettings
    {
        public string ApiMetaUrl { get; set; } = string.Empty;
        public string BusinessManagerPersistanceUrl { get; set; } = string.Empty;
        public string ApiModelUrl { get; set; } = string.Empty;
        public string MetaAccessKey { get; set; } = string.Empty;
        public string MetaKeyId { get; set; } = string.Empty;
        public string GoogleApiKey { get; set; } = string.Empty;
    }
}
