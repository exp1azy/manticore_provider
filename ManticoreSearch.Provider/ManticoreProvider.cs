using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Interfaces;
using ManticoreSearch.Provider.Models.Requests;
using ManticoreSearch.Provider.Models.Responses;
using ManticoreSearch.Provider.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace ManticoreSearch.Provider
{
    /// <summary>
    /// Provides a client for interacting with the Manticore Search server.
    /// <see cref="ManticoreProvider"/> based on HTTP and use <see cref="HttpClient"/> for sending requests to the server.
    /// </summary>
    public sealed class ManticoreProvider : IManticoreProvider
    {
        private HttpClient? _httpClient;
        private JsonSerializerSettings _jsonSettings;
        private bool _disposed;

        private readonly bool _disposeHttpClient;
        private const string _baseAddress = "http://localhost:9308";
        private const int _defaultTimeoutInSeconds = 30;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with the default base address (http://localhost:9308).
        /// </summary>
        /// <param name="timeout">The timeout for HTTP requests, default is 30 seconds.</param>
        public ManticoreProvider(TimeSpan timeout = default)
        {
            _disposeHttpClient = true;

            InitializeHttpClient(_baseAddress, timeout);
            ConfigureJsonSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a specified address.
        /// </summary>
        /// <param name="address">The address of the Manticore Search server.</param>
        /// <param name="timeout">The timeout for HTTP requests, default is 30 seconds.</param>
        public ManticoreProvider(string address, TimeSpan timeout = default)
        {
            if (string.IsNullOrEmpty(address))
                throw new BaseAddressNullException(ExceptionError.BaseAddressNullError);

            _disposeHttpClient = true;

            InitializeHttpClient(address, timeout);
            ConfigureJsonSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a specified instance of <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="disposeHttpClient"><c>true</c>, if the specified instance of <see cref="HttpClient"/> need to be disposed; otherwise, <c>false</c>.</param>
        public ManticoreProvider(HttpClient httpClient, bool disposeHttpClient = false)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _disposeHttpClient = disposeHttpClient;

            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri(_baseAddress);

            ConfigureJsonSettings();
        }

        /// <summary>
        /// Releases the resources used by the <see cref="ManticoreProvider"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Inserts a new document into the specified table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="insertRequest">The modification request containing document data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous insert operation.</returns>
        /// <exception cref="ModificationException">Thrown when an error occurs during the insert operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> InsertAsync<TDocument>(ModificationRequest<TDocument> insertRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessModificationAsync(insertRequest, "/insert", cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Executes a raw SQL query against Manticore Search server asynchronously.
        /// </summary>
        /// <param name="sql">The SQL query string to execute.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous SQL execution.</returns>
        /// <exception cref="SqlException">Thrown when an error occurs during the SQL execution.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default) =>
            await ProcessSqlAsync(sql, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Performs bulk insert operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk insert requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkAsync<TDocument>(List<BulkInsertRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulkRequests, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Replaces an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="replaceRequest">The modification request containing replacement data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous replace operation.</returns>
        /// <exception cref="ModificationException">Thrown when an error occurs during the replace operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ReplaceAsync<TDocument>(ModificationRequest<TDocument> replaceRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessModificationAsync(replaceRequest, "/replace", cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Performs bulk replace operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk replace requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk replace operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk replace operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkReplaceAsync<TDocument>(List<BulkReplaceRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulkRequests, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Updates an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="updateRequest">The update request containing modification data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous update operation.</returns>
        /// <exception cref="UpdateException">Thrown when an error occurs during the update operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> UpdateAsync<TDocument>(UpdateRequest<TDocument> updateRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessUpdateAsync(updateRequest, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Performs bulk update operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk update requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk update operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk update operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkUpdateAsync<TDocument>(List<BulkUpdateRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulkRequests, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Executes a search query against the Manticore Search table asynchronously.
        /// </summary>
        /// <param name="searchRequest">The search request containing query parameters and options.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous search operation.</returns>
        /// <exception cref="SearchException">Thrown when an error occurs during the search operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> SearchAsync(SearchRequest searchRequest, CancellationToken cancellationToken = default) =>
            await ProcessSearchAsync(searchRequest, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Deletes documents from the table asynchronously based on the provided criteria.
        /// </summary>
        /// <param name="deleteRequest">The delete request containing document identifiers.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous delete operation.</returns>
        /// <exception cref="DeleteException">Thrown when an error occurs during the delete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<DeleteResponse> DeleteAsync(DeleteRequest deleteRequest, CancellationToken cancellationToken = default) =>
            await ProcessDeleteAsync(deleteRequest, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Performs bulk delete operations asynchronously.
        /// </summary>
        /// <param name="bulkRequests">List of bulk delete requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk delete operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk delete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkDeleteAsync(List<BulkDeleteRequest> bulkRequests, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulkRequests, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Submits a percolate request asynchronously to match a document against existing percolation queries.
        /// </summary>
        /// <param name="percolateRequest">The percolation request containing the document.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the percolation operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolateRequest, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolateRequest, $"/pq/{index}/doc/", HttpMethod.Put, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Indexes a percolate request with a specific document ID asynchronously.
        /// </summary>
        /// <param name="percolateRequest">The percolation request to index.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation indexing operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the percolation indexing operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolateRequest, string index, long id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolateRequest, $"/pq/{index}/doc/{id}", HttpMethod.Put, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Executes a percolate request asynchronously to match a query against stored percolation documents.
        /// </summary>
        /// <typeparam name="TDocument">The type of document used for percolation. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="percolateRequest">The percolate request containing the query.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the percolation operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<PercolateResponse> PercolateAsync<TDocument>(PercolateRequest<TDocument> percolateRequest, string index, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessPercolateAsync(percolateRequest, $"/pq/{index}/search", HttpMethod.Post, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Updates an existing percolate request asynchronously and optionally refreshes the index.
        /// </summary>
        /// <param name="percolateRequest">The updated percolation request.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier to update.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation update operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the percolation update operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<PercolateResponse> UpdatePercolateAsync(PercolationActionRequest percolateRequest, string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolateRequest, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Retrieves a specific percolate request asynchronously by its ID from the specified index.
        /// </summary>
        /// <param name="index">The index name where the percolate request is stored.</param>
        /// <param name="id">The identifier of the percolate request to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous retrieval operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the retrieval operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> GetPercolateAsync(string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessGetPercolateAsync(index, id, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Retrieves autocomplete suggestions asynchronously based on the provided request.
        /// </summary>
        /// <param name="autocompleteRequest">The autocomplete request parameters.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous autocomplete operation.</returns>
        /// <exception cref="AutocompleteException">Thrown when an error occurs during the autocomplete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>> AutocompleteAsync(AutocompleteRequest autocompleteRequest, CancellationToken cancellationToken = default) =>
            await ProcessAutocompleteAsync(autocompleteRequest, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Defines a new table structure asynchronously using mapping properties.
        /// </summary>
        /// <param name="mappingRequest">The mapping properties describing the table structure.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous mapping operation.</returns>
        /// <exception cref="MappingException">Thrown when an error occurs during the mapping operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public async Task<ManticoreResponse<List<MappingSuccess>, ErrorMessage>> UseMappingAsync(MappingRequest mappingRequest, string index, CancellationToken cancellationToken = default) =>
            await ProcessMappingAsync(mappingRequest, index, cancellationToken).ConfigureAwait(false);

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _httpClient != null && _disposeHttpClient)
                {
                    _httpClient.Dispose();
                    _httpClient = null;
                }

                _disposed = true;
            }
        }

        private StringContent CreateStringContent(object data, string contentType)
        {
            var json = JsonConvert.SerializeObject(data, _jsonSettings);
            return new StringContent(json, Encoding.UTF8, contentType);
        }

        private async Task<HttpResponse> SendAsync(string endpoint, HttpMethod method, StringContent? content = null, CancellationToken cancellationToken = default)
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

        private async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ProcessModificationAsync<TDocument>(ModificationRequest<TDocument> request, string endpoint, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument
        {
            if (request == null || request.Document == null)
            {
                throw new ModificationException(string.Format(ProviderError.ArgumentNull,
                    request == null ? nameof(request) : nameof(request.Document)));
            }

            try
            {
                var content = CreateStringContent(request, "application/x-ndjson");
                var response = await SendAsync(endpoint, HttpMethod.Post, content, cancellationToken);

                var result = new ManticoreResponse<ModificationSuccess, ErrorResponse>()
                {
                    RawResponse = response.Response
                };

                var jsonResponse = JObject.Parse(response.Response);
                if (!jsonResponse.ContainsKey("error"))
                {
                    result.Response = JsonConvert.DeserializeObject<ModificationSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ModificationException(endpoint.Equals("/insert") ?
                    ExceptionError.InsertError : ExceptionError.ReplaceError, ex);
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
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SqlException(ExceptionError.SqlError, ex);
            }
        }

        private async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> ProcessBulkAsync<TBulkRequest>(List<TBulkRequest> documents, CancellationToken cancellationToken = default)
        {
            if (documents == null)
                throw new BulkException(ProviderError.DocumentsNull);

            try
            {
                var json = string.Join("\n", documents.Select(d => JsonConvert.SerializeObject(d)));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");
                var response = await SendAsync("/bulk", HttpMethod.Post, content, cancellationToken);

                var result = new ManticoreResponse<BulkSuccess, List<BulkError>>
                {
                    RawResponse = response.Response
                };

                try
                {
                    result.Error = JsonConvert.DeserializeObject<List<BulkError>>(response.Response, _jsonSettings)!;
                    result.IsSuccess = false;
                }
                catch
                {
                    result.Response = JsonConvert.DeserializeObject<BulkSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BulkException(ExceptionError.BulkError, ex);
            }
        }

        private async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> ProcessUpdateAsync<TDocument>(UpdateRequest<TDocument> document, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument
        {
            try
            {
                var content = CreateStringContent(document, "application/json");
                var response = await SendAsync("/update", HttpMethod.Post, content, cancellationToken);

                var result = new ManticoreResponse<UpdateSuccess, ErrorResponse>
                {
                    RawResponse = response.Response
                };

                if (response.IsSuccessStatusCode)
                {
                    result.Response = JsonConvert.DeserializeObject<UpdateSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UpdateException(ExceptionError.UpdateError, ex);
            }
        }

        private async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> ProcessSearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(request, "application/json");
                var response = await SendAsync("/search", HttpMethod.Post, content, cancellationToken);

                var result = new ManticoreResponse<SearchSuccess, ErrorMessage>
                {
                    RawResponse = response.Response
                };

                if (response.IsSuccessStatusCode)
                {
                    result.Response = JsonConvert.DeserializeObject<SearchSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
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

                var result = new DeleteResponse
                {
                    RawResponse = response.Response
                };

                var jsonResponse = JObject.Parse(response.Response);
                if (jsonResponse.ContainsKey("error"))
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }
                else if (jsonResponse.ContainsKey("deleted"))
                {
                    result.ResponseIfQuery = JsonConvert.DeserializeObject<DeleteByQuerySuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Response = JsonConvert.DeserializeObject<DeleteSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DeleteException(ExceptionError.DeleteError, ex);
            }
        }

        private async Task<PercolateResponse> ProcessPercolateAsync<TPercolateRequest>(TPercolateRequest document, string endpoint, HttpMethod httpMethod, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContent(document, "application/json");
                var response = await SendAsync(endpoint, httpMethod, content, cancellationToken);

                var result = new PercolateResponse()
                {
                    RawResponse = response.Response
                };

                var jsonResponse = JObject.Parse(response.Response);
                if (jsonResponse.ContainsKey("result"))
                {
                    result.Response = JsonConvert.DeserializeObject<PercolateSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else if (jsonResponse.ContainsKey("hits"))
                {
                    result.ResponseIfSearch = JsonConvert.DeserializeObject<SearchSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PercolateException(ExceptionError.PercolateError, ex);
            }
        }

        private async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> ProcessGetPercolateAsync(string index, int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await SendAsync(
                    endpoint: $"/pq/{index}/doc/{id}",
                    method: HttpMethod.Get,
                    cancellationToken: cancellationToken
                );

                var result = new ManticoreResponse<SearchSuccess, ErrorMessage>
                {
                    RawResponse = response.Response
                };

                if (response.IsSuccessStatusCode)
                {
                    result.Response = JsonConvert.DeserializeObject<SearchSuccess>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PercolateException(ExceptionError.PercolateError, ex);
            }
        }

        private async Task<ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>> ProcessAutocompleteAsync(AutocompleteRequest autocomplete, CancellationToken cancellationToken = default)
        {
            try
            {
                var stringContent = CreateStringContent(autocomplete, "application/json");
                var response = await SendAsync("/autocomplete", HttpMethod.Post, stringContent, cancellationToken);

                var result = new ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>
                {
                    RawResponse = response.Response
                };

                if (response.IsSuccessStatusCode)
                {
                    result.Response = JsonConvert.DeserializeObject<List<AutocompleteSuccess>>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AutocompleteException(ExceptionError.AutocompleteError, ex);
            }
        }

        private async Task<ManticoreResponse<List<MappingSuccess>, ErrorMessage>> ProcessMappingAsync(MappingRequest properties, string index, CancellationToken cancellationToken = default)
        {
            try
            {
                var stringContent = CreateStringContent(properties, "application/json");
                var response = await SendAsync($"/{index}/_mapping", HttpMethod.Post, stringContent, cancellationToken);

                var result = new ManticoreResponse<List<MappingSuccess>, ErrorMessage>
                {
                    RawResponse = response.Response
                };

                try
                {
                    result.Response = JsonConvert.DeserializeObject<List<MappingSuccess>>(response.Response, _jsonSettings);
                    result.IsSuccess = true;
                }
                catch
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response, _jsonSettings);
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MappingException(ExceptionError.MappingError, ex);
            }
        }

        private void InitializeHttpClient(string uri, TimeSpan timeout = default)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(uri),
                Timeout = timeout == default ? TimeSpan.FromSeconds(_defaultTimeoutInSeconds) : timeout
            };
        }

        private void ConfigureJsonSettings()
        {
            _jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}