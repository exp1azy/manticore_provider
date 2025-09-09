using ManticoreSearch.Provider.Exceptions;
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
    public sealed class ManticoreProvider : IDisposable
    {
        private HttpClient? _httpClient;
        private bool _disposed;

        private const string _baseAddress = "http://localhost:9308";
        private const int _defaultTimeoutInSeconds = 30;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with the default base address (http://localhost:9308).
        /// </summary>
        /// <param name="timeout">The timeout for HTTP requests, default is 30 seconds.</param>
        public ManticoreProvider(TimeSpan timeout = default)
        {
            InitializeHttpClient(_baseAddress, timeout);
            ConfigureJsonSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a specified base address.
        /// </summary>
        /// <param name="address">The address of the Manticore Search server.</param>
        /// <param name="timeout">The timeout for HTTP requests, default is 30 seconds.</param>
        public ManticoreProvider(string address, TimeSpan timeout = default)
        {
            if (string.IsNullOrEmpty(address))
                throw new BaseAddressNullException(ExceptionError.BaseAddressNullError);

            InitializeHttpClient(address, timeout);
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
        /// Inserts a new document into the specified table synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="insert">The modification request containing document data.</param>
        /// <returns>Response indicating success or failure of the insert operation.</returns>
        public ManticoreResponse<ModificationSuccess, ErrorResponse> Insert<TDocument>(ModificationRequest<TDocument> insert)
            where TDocument : ManticoreDocument => ProcessModificationAsync(insert, "/insert").GetAwaiter().GetResult();

        /// <summary>
        /// Inserts a new document into the specified table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="insert">The modification request containing document data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous insert operation.</returns>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> InsertAsync<TDocument>(ModificationRequest<TDocument> insert, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessModificationAsync(insert, "/insert", cancellationToken);

        /// <summary>
        /// Executes a raw SQL query against Manticore Search synchronously.
        /// </summary>
        /// <param name="sql">The SQL query string to execute.</param>
        /// <returns>Raw response string from the SQL execution.</returns>
        public string Sql(string sql) =>
            ProcessSqlAsync(sql).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a raw SQL query against Manticore Search asynchronously.
        /// </summary>
        /// <param name="sql">The SQL query string to execute.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous SQL execution.</returns>
        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default) =>
            await ProcessSqlAsync(sql, cancellationToken);

        /// <summary>
        /// Performs bulk insert operations synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk insert requests.</param>
        /// <returns>Response containing bulk operation results.</returns>
        public ManticoreResponse<BulkSuccess, List<BulkError>> Bulk<TDocument>(List<BulkInsertRequest<TDocument>> bulk)
            where TDocument : ManticoreDocument => ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Performs bulk insert operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk insert requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk operation.</returns>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkAsync<TDocument>(List<BulkInsertRequest<TDocument>> bulk, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Replaces an existing document in the table synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="replace">The modification request containing replacement data.</param>
        /// <returns>Response indicating success or failure of the replace operation.</returns>
        public ManticoreResponse<ModificationSuccess, ErrorResponse> Replace<TDocument>(ModificationRequest<TDocument> replace)
            where TDocument : ManticoreDocument => ProcessModificationAsync(replace, "/replace").GetAwaiter().GetResult();

        /// <summary>
        /// Replaces an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="replace">The modification request containing replacement data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous replace operation.</returns>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ReplaceAsync<TDocument>(ModificationRequest<TDocument> replace, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessModificationAsync(replace, "/replace", cancellationToken);

        /// <summary>
        /// Performs bulk replace operations synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk replace requests.</param>
        /// <returns>Response containing bulk operation results.</returns>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkReplace<TDocument>(List<BulkReplaceRequest<TDocument>> bulk)
            where TDocument : ManticoreDocument => ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Performs bulk replace operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk replace requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk replace operation.</returns>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkReplaceAsync<TDocument>(List<BulkReplaceRequest<TDocument>> bulk, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Updates an existing document in the table synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="update">The update request containing modification data.</param>
        /// <returns>Response indicating success or failure of the update operation.</returns>
        public ManticoreResponse<UpdateSuccess, ErrorResponse> Update<TDocument>(UpdateRequest<TDocument> update)
            where TDocument : ManticoreDocument => ProcessUpdateAsync(update).GetAwaiter().GetResult();

        /// <summary>
        /// Updates an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="update">The update request containing modification data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous update operation.</returns>
        public async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> UpdateAsync<TDocument>(UpdateRequest<TDocument> update, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessUpdateAsync(update, cancellationToken);

        /// <summary>
        /// Performs bulk update operations synchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk update requests.</param>
        /// <returns>Response containing bulk operation results.</returns>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkUpdate<TDocument>(List<BulkUpdateRequest<TDocument>> bulk)
            where TDocument : ManticoreDocument => ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Performs bulk update operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulk">List of bulk update requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk update operation.</returns>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkUpdateAsync<TDocument>(List<BulkUpdateRequest<TDocument>> bulk, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Executes a search query against the Manticore Search table synchronously.
        /// </summary>
        /// <param name="search">The search request containing query parameters and options.</param>
        /// <returns>Search results or error message if the operation fails.</returns>
        /// <exception cref="SearchException">Thrown if an error occurred while executing a search request.</exception>
        public ManticoreResponse<SearchSuccess, ErrorMessage> Search(SearchRequest search) =>
            ProcessSearchAsync(search).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a search query against the Manticore Search table asynchronously.
        /// </summary>
        /// <param name="search">The search request containing query parameters and options.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous search operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="SearchException">Thrown if an error occurred while executing a search request.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> SearchAsync(SearchRequest search, CancellationToken cancellationToken = default) =>
            await ProcessSearchAsync(search, cancellationToken);

        /// <summary>
        /// Deletes documents from the table synchronously based on the provided criteria.
        /// </summary>
        /// <param name="delete">The delete request containing document identifiers.</param>
        /// <returns>Response indicating success or failure of the delete operation.</returns>
        /// <exception cref="DeleteException">Thrown if an error occurred while deleting a document.</exception>
        public DeleteResponse Delete(DeleteRequest delete) =>
            ProcessDeleteAsync(delete).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes documents from the table asynchronously based on the provided criteria.
        /// </summary>
        /// <param name="delete">The delete request containing document identifiers.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous delete operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="DeleteException">Thrown if an error occurred while deleting a document.</exception>
        public async Task<DeleteResponse> DeleteAsync(DeleteRequest delete, CancellationToken cancellationToken = default) =>
            await ProcessDeleteAsync(delete, cancellationToken);

        /// <summary>
        /// Performs bulk delete operations synchronously.
        /// </summary>
        /// <param name="bulk">List of bulk delete requests.</param>
        /// <returns>Response containing bulk operation results.</returns>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk operations.</exception>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkDelete(List<BulkDeleteRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Performs bulk delete operations asynchronously.
        /// </summary>
        /// <param name="bulk">List of bulk delete requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk delete operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk operations.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkDeleteAsync(List<BulkDeleteRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Submits a percolate request to match a document against existing percolation queries.
        /// </summary>
        /// <param name="percolate">The percolation request containing the document.</param>
        /// <param name="index">The target index name.</param>
        /// <returns>Percolation results including matched queries.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public PercolateResponse IndexPercolate(PercolationActionRequest percolate, string index) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Submits a percolate request asynchronously to match a document against existing percolation queries.
        /// </summary>
        /// <param name="percolate">The percolation request containing the document.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolate, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Indexes a percolate request with a specific document ID.
        /// </summary>
        /// <param name="percolate">The percolation request to index.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier.</param>
        /// <returns>Response indicating success or failure of the indexing operation.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public PercolateResponse IndexPercolate(PercolationActionRequest percolate, string index, long id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Indexes a percolate request with a specific document ID asynchronously.
        /// </summary>
        /// <param name="percolate">The percolation request to index.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation indexing operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolate, string index, long id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Executes a percolate request to match a query against stored percolation documents.
        /// </summary>
        /// <typeparam name="TDocument">The type of document used for percolation. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="percolate">The percolate request containing the query.</param>
        /// <param name="index">The target index name.</param>
        /// <returns>Percolation results including matched documents.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public PercolateResponse Percolate<TDocument>(PercolateRequest<TDocument> percolate, string index)
            where TDocument : ManticoreDocument => ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a percolate request asynchronously to match a query against stored percolation documents.
        /// </summary>
        /// <typeparam name="TDocument">The type of document used for percolation. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="percolate">The percolate request containing the query.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public async Task<PercolateResponse> PercolateAsync<TDocument>(PercolateRequest<TDocument> percolate, string index, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument => await ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post, cancellationToken);

        /// <summary>
        /// Updates an existing percolate request and optionally refreshes the index.
        /// </summary>
        /// <param name="percolate">The updated percolation request.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier to update.</param>
        /// <returns>Response indicating success or failure of the update operation.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public PercolateResponse UpdatePercolate(PercolationActionRequest percolate, string index, int id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Updates an existing percolate request asynchronously and optionally refreshes the index.
        /// </summary>
        /// <param name="percolate">The updated percolation request.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="id">The document identifier to update.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation update operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred during percolation.</exception>
        public async Task<PercolateResponse> UpdatePercolateAsync(PercolationActionRequest percolate, string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Retrieves a specific percolate request by its ID from the specified index.
        /// </summary>
        /// <param name="index">The index name where the percolate request is stored.</param>
        /// <param name="id">The identifier of the percolate request to retrieve.</param>
        /// <returns>Percolate request details or error message if not found.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred during retrieval.</exception>
        public ManticoreResponse<SearchSuccess, ErrorMessage> GetPercolate(string index, int id) =>
            ProcessGetPercolateAsync(index, id).GetAwaiter().GetResult();

        /// <summary>
        /// Retrieves a specific percolate request asynchronously by its ID from the specified index.
        /// </summary>
        /// <param name="index">The index name where the percolate request is stored.</param>
        /// <param name="id">The identifier of the percolate request to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous retrieval operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred during retrieval.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> GetPercolateAsync(string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessGetPercolateAsync(index, id, cancellationToken);

        /// <summary>
        /// Retrieves autocomplete suggestions based on the provided request.
        /// </summary>
        /// <param name="autocomplete">The autocomplete request parameters.</param>
        /// <returns>List of autocomplete suggestions or error message.</returns>
        /// <exception cref="AutocompleteException">Thrown if an error occurred during autocomplete operation.</exception>
        public ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage> Autocomplete(AutocompleteRequest autocomplete) =>
            ProcessAutocompleteAsync(autocomplete).GetAwaiter().GetResult();

        /// <summary>
        /// Retrieves autocomplete suggestions asynchronously based on the provided request.
        /// </summary>
        /// <param name="autocomplete">The autocomplete request parameters.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous autocomplete operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="AutocompleteException">Thrown if an error occurred during autocomplete operation.</exception>
        public async Task<ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>> AutocompleteAsync(AutocompleteRequest autocomplete, CancellationToken cancellationToken = default) =>
            await ProcessAutocompleteAsync(autocomplete, cancellationToken);

        /// <summary>
        /// Defines a new table structure using mapping properties.
        /// </summary>
        /// <param name="properties">The mapping properties describing the table structure.</param>
        /// <param name="index">The target index name.</param>
        /// <returns>Response indicating success or failure of the mapping operation.</returns>
        /// <exception cref="MappingException">Thrown if an error occurred during mapping.</exception>
        public ManticoreResponse<List<MappingSuccess>, ErrorMessage> UseMapping(MappingRequest properties, string index) =>
            ProcessMappingAsync(properties, index).GetAwaiter().GetResult();

        /// <summary>
        /// Defines a new table structure asynchronously using mapping properties.
        /// </summary>
        /// <param name="properties">The mapping properties describing the table structure.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous mapping operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="MappingException">Thrown if an error occurred during mapping.</exception>
        public async Task<ManticoreResponse<List<MappingSuccess>, ErrorMessage>> UseMappingAsync(MappingRequest properties, string index, CancellationToken cancellationToken = default) =>
            await ProcessMappingAsync(properties, index, cancellationToken);

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient!.Dispose();
                    _httpClient = null;
                }

                _disposed = true;
            }
        }

        private static StringContent CreateStringContent(object data, string contentType)
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
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };
        }

        private async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ProcessModificationAsync<TDocument>(ModificationRequest<TDocument> request, string endpoint, CancellationToken cancellationToken = default)
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
                    result.Error = JsonConvert.DeserializeObject<List<BulkError>>(response.Response)!;
                    result.IsSuccess = false;
                }
                catch
                {
                    result.Response = JsonConvert.DeserializeObject<BulkSuccess>(response.Response);
                    result.IsSuccess = true;
                }

                return result;
            }
            catch (BulkException ex)
            {
                throw new BulkException(ExceptionError.BulkError, ex);
            }
        }

        private async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> ProcessUpdateAsync<TDocument>(UpdateRequest<TDocument> document, CancellationToken cancellationToken = default)
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
                    result.Response = JsonConvert.DeserializeObject<SearchSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response);
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

                var result = new DeleteResponse
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
                    result.Response = JsonConvert.DeserializeObject<PercolateSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else if (jsonResponse.ContainsKey("hits"))
                {
                    result.ResponseIfSearch = JsonConvert.DeserializeObject<SearchSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
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
                    result.Response = JsonConvert.DeserializeObject<SearchSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
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
                    result.Response = JsonConvert.DeserializeObject<List<AutocompleteSuccess>>(response.Response);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
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
                    result.Response = JsonConvert.DeserializeObject<List<MappingSuccess>>(response.Response);
                    result.IsSuccess = true;
                }
                catch
                {
                    result.Error = JsonConvert.DeserializeObject<ErrorMessage>(response.Response);
                    result.IsSuccess = false;
                }

                return result;
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

        private static void ConfigureJsonSettings()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}