using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;
using ManticoreSearch.Api.Models.Responses;
using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace ManticoreSearch.Api
{
    public sealed class ManticoreProvider : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;
      
        public ManticoreProvider()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:9308")
            };
        }

        public ManticoreProvider(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<ModificationResponse> InsertAsync(InsertRequest document, CancellationToken cancellationToken = default) =>
            await ProcessInsertAsync(document, cancellationToken);

        public ModificationResponse Insert(InsertRequest document) =>
            ProcessInsertAsync(document).GetAwaiter().GetResult();

        public string Sql(string sql) =>
            ProcessSqlAsync(sql).GetAwaiter().GetResult();

        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default) =>
            await ProcessSqlAsync(sql, cancellationToken);

        public BulkResponse Bulk(IEnumerable<BulkInsertRequest> documents) =>
            ProcessBulkAsync(documents).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkAsync(IEnumerable<BulkInsertRequest> documents, CancellationToken cancellationToken) =>
            await ProcessBulkAsync(documents, cancellationToken);

        public ModificationResponse Replace(InsertRequest document) =>
            ProcessReplaceAsync(document).GetAwaiter().GetResult();

        public async Task<ModificationResponse> ReplaceAsync(InsertRequest document, CancellationToken cancellationToken = default) =>
            await ProcessReplaceAsync(document, cancellationToken);

        public BulkResponse BulkReplace(IEnumerable<BulkReplaceRequest> documents) =>
            ProcessBulkAsync(documents).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkReplaceAsync(IEnumerable<BulkReplaceRequest> documents, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(documents, cancellationToken);

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                _disposed = true;
            }
        }

        private StringContent CreateStringContent(object data, string contentType)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, contentType);
        }

        private async Task<string> SendAsync(string endpoint, HttpMethod method, StringContent content, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        private async Task<ModificationResponse> ProcessRequiredAsync(string endpoint, InsertRequest document, CancellationToken cancellationToken = default)
        {
            var content = CreateStringContent(document, "application/x-ndjson");
            var response = await SendAsync(endpoint, HttpMethod.Post, content, cancellationToken);
            var jsonResponse = JObject.Parse(response);
            var result = new ModificationResponse();

            if (jsonResponse.ContainsKey("error"))
            {
                result.Error = jsonResponse.ToObject<ModificationError>();
                result.IsSuccess = false;
            }
            else
            {
                result.Response = jsonResponse.ToObject<ModificationSuccess>();
                result.IsSuccess = true;
            }

            return result;
        }

        private async Task<ModificationResponse> ProcessInsertAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            if (document == null || document.Document == null)
                throw new InsertException(string.Format(ProviderError.ArgumentNull, 
                    document == null ? nameof(document) : nameof(document.Document)));

            if (document.Document.Count <= 0)
                throw new InsertException(ProviderError.DocumentCount);

            try
            {
                return await ProcessRequiredAsync("/insert", document, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InsertException(ExceptionError.InsertError, ex);
            }
        }

        public async Task<string> ProcessSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            if (sql == null)
                throw new SqlException(ProviderError.SqlNull);

            try
            {
                var content = new StringContent(sql);

                return await SendAsync("/cli", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new SqlException(ExceptionError.SqlError, ex);
            }
        }

        private async Task<BulkResponse> ProcessBulkAsync<TBulkRequest>(IEnumerable<TBulkRequest> documents, CancellationToken cancellationToken = default)
        {
             if (documents == null)
                throw new BulkException(ProviderError.DocumentsNull);

            try
            {
                var json = string.Join("\n", documents.Select(d => JsonConvert.SerializeObject(d)));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");
                string response = await SendAsync("/bulk", HttpMethod.Post, content, cancellationToken);
                var result = new BulkResponse();

                try
                {
                    result.Response = JsonConvert.DeserializeObject<BulkSuccess>(response);
                    result.IsSuccess = true;
                }
                catch
                {
                    result.Error = JsonConvert.DeserializeObject<List<BulkError>>(response)!;
                    result.IsSuccess = false;
                }

                return result;             
            }
            catch (BulkException ex)
            {
                throw new BulkException(ExceptionError.BulkError, ex);
            }
        }

        private async Task<ModificationResponse> ProcessReplaceAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            if (document == null || document.Document == null)
                throw new ReplaceException(string.Format(ProviderError.ArgumentNull,
                    document == null ? nameof(document) : nameof(document.Document)));

            if (document.Document.Count <= 0)
                throw new ReplaceException(ProviderError.DocumentCount);

            try
            {
                return await ProcessRequiredAsync("/replace", document, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ReplaceException(ExceptionError.ReplaceError, ex);
            }
        } 

        public object Update(UpdateRequest document)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return Send("/update", HttpMethod.Post, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<object> UpdateAsync(UpdateRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");

                return await SendAsync("/update", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SearchResponse Search(SearchRequest request)
        {
            //try
            //{
            //    var content = CreateStringContent(request, "application/json");

            //    return Send("/search", HttpMethod.Post, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
        {
            //try
            //{
            //    var content = CreateStringContent(request, "application/json");

            //    return await SendAsync("/search", HttpMethod.Post, content, cancellationToken);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public object Delete(DeleteRequest document)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return Send("/delete", HttpMethod.Post, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<object> DeleteAsync(DeleteRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");

                return await SendAsync("/delete", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object BulkDelete(IEnumerable<BulkDeleteRequest> documents)
        {
            //try
            //{
            //    var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
            //    var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

            //    return Send("/bulk", HttpMethod.Post, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<object> BulkDeleteAsync(IEnumerable<BulkDeleteRequest> documents, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return await SendAsync("/bulk", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SearchResponse IndexPercolate(IndexPercolateRequest document, string index)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return Send($"/pq/{index}/doc/", HttpMethod.Put, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<SearchResponse> IndexPercolateAsync(IndexPercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return await SendAsync<SearchResponse>($"/pq/{index}/doc/", HttpMethod.Put, content, cancellationToken);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public SearchResponse Percolate(PercolateRequest document, string index)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return Send<SearchResponse>($"/pq/{index}/search", HttpMethod.Post, content);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }

        public async Task<SearchResponse> PercolateAsync(PercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
            //try
            //{
            //    var content = CreateStringContent(document, "application/json");

            //    return await SendAsync<SearchResponse>($"/pq/{index}/search", HttpMethod.Post, content, cancellationToken);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return new();
        }
    }
}
