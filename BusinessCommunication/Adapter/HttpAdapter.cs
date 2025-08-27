using Application.Interfaces;
using System.Net.Http.Json;

namespace Adapter
{
    public class HttpAdapter : IHttpAdapter
    {
        private readonly HttpClient _httpClient;

        public HttpAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> ExecuteGetWithHttpClientAsync(string url)
        {
            return await _httpClient.GetAsync(url).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> ExecutePostWithHttpClientAsync(string url, Dictionary<string, string> headers, object message)
        {
            if (headers.Count != 0)
            {
                foreach (var item in headers)
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }

            return await _httpClient.PostAsJsonAsync(url, message).ConfigureAwait(false);
        }
    }
}
