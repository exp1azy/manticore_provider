using ManticoreSearch.Provider.Models.Responses;
using ManticoreSearch.Provider.Resources;

namespace ManticoreSearch.Provider
{
    internal class HttpClientHelper : IDisposable
    {
        private bool _disposed;
        private HttpClient? _httpClient;
        private const int _defaultTimeoutInSeconds = 30;

        public HttpClientHelper(string uri, TimeSpan timeout = default)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(uri),
                Timeout = timeout == default ? TimeSpan.FromSeconds(_defaultTimeoutInSeconds) : timeout
            };
        }

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponse> SendAsync(string endpoint, HttpMethod method, StringContent? content = null, CancellationToken cancellationToken = default)
        {
            if (_httpClient == null)
                throw new InvalidOperationException(ProviderError.HttpClientNotInitialized);

            using var request = new HttpRequestMessage(method, endpoint);

            if (content != null)
                request.Content = content;

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return new HttpResponse
            {
                Response = await response.Content.ReadAsStringAsync(cancellationToken),
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _httpClient?.Dispose();
                _httpClient = null;

                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
