using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;
using ManticoreSearch.Api.Models.Responses;
using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

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

        public ModificationResponse Insert(InsertRequest insert) =>
            ProcessModificationAsync(insert,"/insert",
                (message, innerEx) => new InsertException(message, innerEx)).GetAwaiter().GetResult();

        public async Task<ModificationResponse> InsertAsync(InsertRequest insert, CancellationToken cancellationToken = default) =>
            await ProcessModificationAsync(insert, "/insert",
                (message, innerEx) => new InsertException(message, innerEx), cancellationToken);  

        public string Sql(string sql) =>
            ProcessSqlAsync(sql).GetAwaiter().GetResult();

        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default) =>
            await ProcessSqlAsync(sql, cancellationToken);

        public BulkResponse Bulk(IEnumerable<BulkInsertRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkAsync(IEnumerable<BulkInsertRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        public ModificationResponse Replace(InsertRequest replace) =>
            ProcessModificationAsync(replace, "/replace",
                (message, innerEx) => new ReplaceException(message, innerEx)).GetAwaiter().GetResult();

        public async Task<ModificationResponse> ReplaceAsync(InsertRequest replace, CancellationToken cancellationToken = default) =>
            await ProcessModificationAsync(replace, "/insert",
                (message, innerEx) => new InsertException(message, innerEx), cancellationToken);

        public BulkResponse BulkReplace(IEnumerable<BulkReplaceRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkReplaceAsync(IEnumerable<BulkReplaceRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        public UpdateResponse Update(UpdateRequest update) =>
            ProcessUpdateAsync(update).GetAwaiter().GetResult();

        public async Task<UpdateResponse> UpdateAsync(UpdateRequest update, CancellationToken cancellationToken = default) =>
            await ProcessUpdateAsync(update, cancellationToken);

        public BulkResponse BulkUpdate(IEnumerable<BulkUpdateRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkUpdateAsync(IEnumerable<BulkUpdateRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        public SearchResponse Search(SearchRequest search) =>
            ProcessSearchAsync(search).GetAwaiter().GetResult();

        public async Task<SearchResponse> SearchAsync(SearchRequest search, CancellationToken cancellationToken = default) =>
            await ProcessSearchAsync(search, cancellationToken);

        public DeleteResponse Delete(DeleteRequest delete) =>
            ProcessDeleteAsync(delete).GetAwaiter().GetResult();

        public async Task<DeleteResponse> DeleteAsync(DeleteRequest delete, CancellationToken cancellationToken = default) =>
            await ProcessDeleteAsync(delete, cancellationToken);

        public BulkResponse BulkDelete(IEnumerable<BulkDeleteRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        public async Task<BulkResponse> BulkDeleteAsync(IEnumerable<BulkDeleteRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        public IndexPercolateResponse IndexPercolate(IndexPercolateRequest percolate, string index) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put).GetAwaiter().GetResult();

        public async Task<IndexPercolateResponse> IndexPercolateAsync(IndexPercolateRequest percolate, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put, cancellationToken);

        public IndexPercolateResponse IndexPercolate(IndexPercolateRequest percolate, string index, long id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put).GetAwaiter().GetResult();

        public async Task<IndexPercolateResponse> IndexPercolateAsync(IndexPercolateRequest percolate, string index, long id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put, cancellationToken);

        public object Percolate(PercolateRequest percolate, string index) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post).GetAwaiter().GetResult();

        public async Task<object> PercolateAsync(PercolateRequest percolate, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post, cancellationToken);

        public object UpdatePercolate(IndexPercolateRequest percolate, string index, int id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put).GetAwaiter().GetResult();

        public async Task<object> UpdatePercolateAsync(IndexPercolateRequest percolate, string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put, cancellationToken);

        public SearchResponse GetPercolate(string index, int id) =>
            ProcessGetPercolateAsync(index, id).GetAwaiter().GetResult();

        public async Task<SearchResponse> GetPercolateAsync(string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessGetPercolateAsync(index, id, cancellationToken);

        public List<AutocompleteResponse> Autocomplete(object request) =>
            ProcessAutocompleteAsync(request).GetAwaiter().GetResult();

        public async Task<List<AutocompleteResponse>> AutocompleteAsync(object request, CancellationToken cancellationToken = default) =>
            await ProcessAutocompleteAsync(request, cancellationToken);

        public object UseMapping(MappingRequest properties, string index) =>
            ProcessMappingAsync(properties, index).GetAwaiter().GetResult();

        public async Task<object> UseMappingAsync(MappingRequest properties, string index, CancellationToken cancellationToken = default) =>
            await ProcessMappingAsync(properties, index, cancellationToken);

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

        private async Task<HttpResponse> SendAsync(string endpoint, HttpMethod method, StringContent? content = null, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, endpoint);
            if (content != null)
                request.Content = content;

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return new HttpResponse
            {
                Response = await response.Content.ReadAsStringAsync(cancellationToken),
                StatusCode = (int)response.StatusCode
            };
        }

        private async Task<ModificationResponse> ProcessModificationAsync(InsertRequest document, string endpoint, Func<string, Exception?, Exception> exceptionFactory, CancellationToken cancellationToken = default)
        {
            if (document == null || document.Document == null)
            {
                throw exceptionFactory?.Invoke(
                    string.Format(ProviderError.ArgumentNull,
                    document == null ? nameof(document) : nameof(document.Document)), null
                )!;
            }               

            if (document.Document.Count <= 0)
                throw exceptionFactory?.Invoke(ProviderError.DocumentCount, null)!;

            try
            {
                var content = CreateStringContent(document, "application/x-ndjson");
                var response = await SendAsync(endpoint, HttpMethod.Post, content, cancellationToken);
                var result = new ModificationResponse()
                {
                    RawResponse = response.Response
                };

                if (response.StatusCode >= 200 && response.StatusCode < 300)
                {
                    result.Response = JsonConvert.DeserializeObject<ModificationSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response);
                    result.IsSuccess = false;                   
                }

                return result;
            }
            catch (Exception ex)
            {
                throw exceptionFactory?.Invoke(ExceptionError.InsertError, ex)!;
            }
        }

        private async Task<string> ProcessSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            if (sql == null)
                throw new SqlException(ProviderError.SqlNull);

            try
            {
                var content = new StringContent(sql);

                return (await SendAsync("/cli", HttpMethod.Post, content, cancellationToken)).Response;
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
                var response = await SendAsync("/bulk", HttpMethod.Post, content, cancellationToken);
                var result = new BulkResponse()
                {
                    RawResponse = response.Response
                };

                if (response.StatusCode >= 200 && response.StatusCode < 300)
                {
                    result.Response = JsonConvert.DeserializeObject<BulkSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<List<BulkError>>(response.Response)!;
                    result.IsSuccess = false;
                }

                return result;             
            }
            catch (BulkException ex)
            {
                throw new BulkException(ExceptionError.BulkError, ex);
            }
        }

        private async Task<UpdateResponse> ProcessUpdateAsync(UpdateRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");
                var response = await SendAsync("/update", HttpMethod.Post, content, cancellationToken);               
                var result = new UpdateResponse
                {
                    RawResponse = response.Response
                };

                if (response.StatusCode >= 200 && response.StatusCode < 300)
                {
                    result.Response = JsonConvert.DeserializeObject<UpdateSuccess>(response.Response);
                    result.IsSuccess = true;                    
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new UpdateException(ExceptionError.UpdateError, ex);
            }
        }

        private async Task<SearchResponse> ProcessSearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(request, "application/json");
                var response = await SendAsync("/search", HttpMethod.Post, content, cancellationToken);
                var result = new SearchResponse
                {
                    RawResponse = response.Response
                };

                if (response.StatusCode >= 200 && response.StatusCode < 300)
                {
                    result.Response = JsonConvert.DeserializeObject<SearchSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<SearchError>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SearchException(ExceptionError.SearchError, ex);
            }
        }

        private async Task<DeleteResponse> ProcessDeleteAsync(DeleteRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");
                var response = await SendAsync("/delete", HttpMethod.Post, content, cancellationToken);
                var result = new DeleteResponse()
                {
                    RawResponse = response.Response
                };

                var jsonResponse = JObject.Parse(response.Response);
                if (jsonResponse.ContainsKey("error"))
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response);
                    result.IsSuccess = false;
                }
                else if (jsonResponse.ContainsKey("deleted"))
                {
                    result.ResponseIfQuery = JsonConvert.DeserializeObject<DeleteByQuerySuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Response = JsonConvert.DeserializeObject<DeleteSuccess>(response.Response);
                    result.IsSuccess = true;              
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DeleteException(ExceptionError.DeleteError, ex);
            }
        }

        private async Task<IndexPercolateResponse> ProcessPercolateAsync<TPercolateRequest>(TPercolateRequest document, string endpoint, HttpMethod httpMethod, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");
                var response = await SendAsync(endpoint, httpMethod, content, cancellationToken);
                var result = new IndexPercolateResponse()
                {
                    RawResponse = response.Response
                };

                result = JsonConvert.DeserializeObject<IndexPercolateResponse>(response.Response);
                result.IsSuccess = true;

                return result;
            }
            catch (Exception ex)
            {
                throw new PercolateException(ExceptionError.PercolateError, ex);
            }
        }

        private async Task<SearchResponse> ProcessGetPercolateAsync(string index, int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await SendAsync(
                    endpoint: $"/pq/{index}/doc/{id}", 
                    method: HttpMethod.Get, 
                    cancellationToken: cancellationToken
                );

                var result = JsonConvert.DeserializeObject<SearchResponse>(response.Response);

                return result;
            }
            catch (Exception ex)
            {
                throw new PercolateException(ExceptionError.PercolateError, ex);
            }
        }

        private async Task<List<AutocompleteResponse>> ProcessAutocompleteAsync(object request, CancellationToken cancellationToken = default)
        {
            try
            {
                var stringContent = CreateStringContent(request, "application/json");
                var response = await SendAsync("/autocomplete", HttpMethod.Post, stringContent, cancellationToken);

                var result = JsonConvert.DeserializeObject<List<AutocompleteResponse>>(response.Response);

                return result;
            }
            catch (Exception ex)
            {
                throw new AutocompleteException(ExceptionError.AutocompleteError, ex);
            }
        }

        private async Task<object> ProcessMappingAsync(MappingRequest properties, string index, CancellationToken cancellationToken = default)
        {
            try
            {
                var stringContent = CreateStringContent(properties, "application/json");
                var response = await SendAsync($"/{index}/_mapping", HttpMethod.Post, stringContent, cancellationToken);

                var result = JsonConvert.DeserializeObject<object>(response.Response);

                return result;
            }
            catch (Exception ex)
            {
                throw new AutocompleteException(ExceptionError.AutocompleteError, ex);
            }
        }
    }
}
