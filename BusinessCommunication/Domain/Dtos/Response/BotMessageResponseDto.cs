using Domain.Dtos.Request;

namespace Domain.Dtos.Response
{
    public class SendMessageToBotResponseDto
    {
        public IEnumerable<Candidate> Candidates { get; set; }
        public UsageMetadata UsageMetadata { get; set; }
        public string modelVersion { get; set; } = string.Empty;
        public string responseId { get; set; } = string.Empty;
    }

    public class Candidate
    {
        public CandidateContent Content { get; set; }
        public string FinishReason { get; set; } = string.Empty;
        public float AvgLogprobs { get; set; }
    }

    public class CandidateContent
    {
        public IEnumerable<Part> Parts { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class UsageMetadata
    {
        public int PromptTokenCount { get; set; }
        public int CandidatesTokenCount { get; set; }
        public int TotalTokenCount { get; set; }
        public IEnumerable<TokenDetail> PromptTokensDetails { get; set; }
        public IEnumerable<TokenDetail> CandidatesTokensDetails { get; set; }
    }

    public class TokenDetail
    {
        public string Modality { get; set; } = string.Empty;
        public int TokenCount { get; set; }
    }
}
