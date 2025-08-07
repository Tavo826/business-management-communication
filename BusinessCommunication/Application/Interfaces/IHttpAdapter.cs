namespace Application.Interfaces
{
    public interface IHttpAdapter
    {
        Task<HttpResponseMessage> ExecutePostWithHttpClientAsync(string url, Dictionary<string, string> headers, object message);
        Task<HttpResponseMessage> ExecuteGetWithHttpClientAsync(string url);
    }
}
