using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;
using ManticoreSearch.Provider.Models.Responses;
using ManticoreSearch.Provider.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ManticoreSearch.Provider
{
    /// <summary>
    /// Provides a client for interacting with the Manticore Search API.
    /// </summary>
    public sealed class ManticoreProvider : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with the default base address.
        /// </summary>
        public ManticoreProvider()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:9308")
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a specified base address.
        /// </summary>
        /// <param name="baseAddress">The base address of the Manticore Search API.</param>
        public ManticoreProvider(string baseAddress)
        {
            if (string.IsNullOrEmpty(baseAddress))
                throw new BaseAddressNullException(ExceptionError.BaseAddressNullError);

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
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
        /// Inserts a new document into the Manticore Search index synchronously.
        /// </summary>
        /// <param name="insert">The request containing the document to be inserted.</param>
        /// <returns>A response containing the result of the modification operation.</returns>
        /// <exception cref="ModificationException">Thrown if a modification error occurred.</exception>
        public ManticoreResponse<ModificationSuccess, ErrorResponse> Insert(ModificationRequest insert) =>
            ProcessModificationAsync(insert,"/insert").GetAwaiter().GetResult();

        /// <summary>
        /// Inserts a new document into the Manticore Search index asynchronously.
        /// </summary>
        /// <param name="insert">The request containing the document to be inserted.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation, if needed.</param>
        /// <returns>A task that represents the asynchronous insert operation, containing the response.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="ModificationException">Thrown if a modification error occurred.</exception>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> InsertAsync(ModificationRequest insert, CancellationToken cancellationToken = default) =>
            await ProcessModificationAsync(insert, "/insert", cancellationToken);

        /// <summary>
        /// Executes a SQL query synchronously against the Manticore Search database.
        /// </summary>
        /// <param name="sql">The SQL query to be executed.</param>
        /// <returns>The result of the SQL query as a string.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the SQL execution fails.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred while generating a SQL query.</exception>
        public string Sql(string sql) =>
            ProcessSqlAsync(sql).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a SQL query asynchronously against the Manticore Search database.
        /// </summary>
        /// <param name="sql">The SQL query to be executed.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation, if needed.</param>
        /// <returns>A task that represents the asynchronous SQL execution operation, containing the result as a string.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred while generating a SQL query.</exception>
        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default) =>
            await ProcessSqlAsync(sql, cancellationToken);

        /// <summary>
        /// Executes a bulk insert operation synchronously to add multiple documents to the Manticore Search index.
        /// </summary>
        /// <param name="bulk">A collection of bulk insert requests containing the documents to be added.</param>
        /// <returns>A response containing the result of the bulk operation, including any errors encountered.</returns>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public ManticoreResponse<BulkSuccess, List<BulkError>> Bulk(IEnumerable<BulkInsertRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a bulk insert operation asynchronously to add multiple documents to the Manticore Search index.
        /// </summary>
        /// <param name="bulk">A collection of bulk insert requests containing the documents to be added.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation, if needed.</param>
        /// <returns>A task that represents the asynchronous bulk operation, containing the response with the result and errors.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkAsync(IEnumerable<BulkInsertRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Replaces an existing document in the Manticore Search index synchronously with the provided modification request.
        /// </summary>
        /// <param name="replace">The modification request containing the new document data.</param>
        /// <returns>A response indicating the success or failure of the replace operation, along with any errors encountered.</returns>
        /// <exception cref="ModificationException">Thrown if a modification error occurred.</exception>
        public ManticoreResponse<ModificationSuccess, ErrorResponse> Replace(ModificationRequest replace) =>
            ProcessModificationAsync(replace, "/replace").GetAwaiter().GetResult();

        /// <summary>
        /// Replaces an existing document in the Manticore Search index asynchronously with the provided modification request.
        /// </summary>
        /// <param name="replace">The modification request containing the new document data.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation, if needed.</param>
        /// <returns>A task that represents the asynchronous replace operation, containing the response with the result and errors.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="ModificationException">Thrown if a modification error occurred.</exception>
        public async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ReplaceAsync(ModificationRequest replace, CancellationToken cancellationToken = default) =>
            await ProcessModificationAsync(replace, "/replace", cancellationToken);

        /// <summary>
        /// Replaces multiple existing documents in the Manticore Search index synchronously based on the provided bulk replace requests.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk replace requests containing the documents to be replaced.</param>
        /// <returns>A response indicating the success or failure of the bulk replace operation, along with a list of any errors encountered for individual requests.</returns>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkReplace(IEnumerable<BulkReplaceRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Replaces multiple existing documents in the Manticore Search index asynchronously based on the provided bulk replace requests.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk replace requests containing the documents to be replaced.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation, if needed.</param>
        /// <returns>A task that represents the asynchronous bulk replace operation, containing the response with the result and any errors encountered.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkReplaceAsync(IEnumerable<BulkReplaceRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Updates an existing document in the Manticore Search index synchronously based on the provided update request.
        /// </summary>
        /// <param name="update">The update request containing the details of the document to be updated.</param>
        /// <returns>A response indicating the success or failure of the update operation, along with any errors encountered during the process.</returns>
        /// <exception cref="UpdateException">Thrown if an error occurred while updating the document.</exception>
        public ManticoreResponse<UpdateSuccess, ErrorResponse> Update(UpdateRequest update) =>
            ProcessUpdateAsync(update).GetAwaiter().GetResult();

        /// <summary>
        /// Updates an existing document in the Manticore Search index asynchronously based on the provided update request.
        /// </summary>
        /// <param name="update">The update request containing the details of the document to be updated.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous update operation, containing the response with the result and any errors encountered.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="UpdateException">Thrown if an error occurred while updating the document.</exception>
        public async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> UpdateAsync(UpdateRequest update, CancellationToken cancellationToken = default) =>
            await ProcessUpdateAsync(update, cancellationToken);

        /// <summary>
        /// Performs a bulk update of multiple documents in the Manticore Search index synchronously based on the provided bulk update requests.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk update requests containing the details of the documents to be updated.</param>
        /// <returns>A response indicating the success or failure of the bulk update operation, along with a list of any errors encountered for individual updates.</returns>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkUpdate(IEnumerable<BulkUpdateRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Performs a bulk update of multiple documents in the Manticore Search index asynchronously based on the provided bulk update requests.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk update requests containing the details of the documents to be updated.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous bulk update operation, containing the response with the result and any errors encountered for individual updates.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkUpdateAsync(IEnumerable<BulkUpdateRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Executes a search query against the Manticore Search index synchronously using the specified search request parameters.
        /// </summary>
        /// <param name="search">The search request object containing the parameters for the search query, including index name, query, and options.</param>
        /// <returns>A response indicating the success of the search operation, containing the results of the search or an error message if the search fails.</returns>
        /// <exception cref="SearchException">Thrown if an error occurred while executing a search request.</exception>
        public ManticoreResponse<SearchSuccess, ErrorMessage> Search(SearchRequest search) =>
            ProcessSearchAsync(search).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a search query against the Manticore Search index asynchronously using the specified search request parameters.
        /// </summary>
        /// <param name="search">The search request object containing the parameters for the search query, including index name, query, and options.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous search operation, containing the response with the search results or an error message if the search fails.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="SearchException">Thrown if an error occurred while executing a search request.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> SearchAsync(SearchRequest search, CancellationToken cancellationToken = default) =>
            await ProcessSearchAsync(search, cancellationToken);

        /// <summary>
        /// Deletes documents from the Manticore Search index synchronously using the specified delete request parameters.
        /// </summary>
        /// <param name="delete">The delete request object containing the identifiers of the documents to be deleted.</param>
        /// <returns>A response indicating the success of the delete operation, including any relevant information about the deletion.</returns>
        /// <exception cref="DeleteException">Thrown if an error occurred while deleting a document.</exception>
        public DeleteResponse Delete(DeleteRequest delete) =>
            ProcessDeleteAsync(delete).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes documents from the Manticore Search index asynchronously using the specified delete request parameters.
        /// </summary>
        /// <param name="delete">The delete request object containing the identifiers of the documents to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous delete operation, containing the response with the result of the delete operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="DeleteException">Thrown if an error occurred while deleting a document.</exception>
        public async Task<DeleteResponse> DeleteAsync(DeleteRequest delete, CancellationToken cancellationToken = default) =>
            await ProcessDeleteAsync(delete, cancellationToken);

        /// <summary>
        /// Deletes multiple documents from the Manticore Search index synchronously using a bulk delete request.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk delete requests, each specifying the identifiers of the documents to be deleted.</param>
        /// <returns>A response indicating the success of the bulk delete operation, including details of any errors encountered for individual deletions.</returns>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public ManticoreResponse<BulkSuccess, List<BulkError>> BulkDelete(IEnumerable<BulkDeleteRequest> bulk) =>
            ProcessBulkAsync(bulk).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes multiple documents from the Manticore Search index asynchronously using a bulk delete request.
        /// </summary>
        /// <param name="bulk">An enumerable collection of bulk delete requests, each specifying the identifiers of the documents to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous bulk delete operation, containing the response with the result of the bulk delete operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="BulkException">Thrown if an error occurred during bulk loading.</exception>
        public async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> BulkDeleteAsync(IEnumerable<BulkDeleteRequest> bulk, CancellationToken cancellationToken = default) =>
            await ProcessBulkAsync(bulk, cancellationToken);

        /// <summary>
        /// Submits a percolate request to the specified index, allowing the given document to be matched against existing percolation queries.
        /// </summary>
        /// <param name="percolate">The request containing the document to be percolated, which will be evaluated against the queries in the specified index.</param>
        /// <param name="index">The name of the index where the percolation queries are stored.</param>
        /// <returns>A response indicating the result of the percolate operation, including details of matches found.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public PercolateResponse IndexPercolate(PercolationActionRequest percolate, string index) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously submits a percolate request to the specified index, allowing the given document to be matched against existing percolation queries.
        /// </summary>
        /// <param name="percolate">The request containing the document to be percolated, which will be evaluated against the queries in the specified index.</param>
        /// <param name="index">The name of the index where the percolation queries are stored.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task that represents the asynchronous percolate operation, containing the response with the result of the percolate operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolate, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Indexes a percolate request against the specified index and document ID, enabling the query to be stored and evaluated later.
        /// </summary>
        /// <param name="percolate">The request containing the percolation query to be indexed.</param>
        /// <param name="index">The name of the index where the percolate request will be stored.</param>
        /// <param name="id">The unique identifier for the document being indexed.</param>
        /// <returns>A response indicating the success or failure of the indexing operation.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public PercolateResponse IndexPercolate(PercolationActionRequest percolate, string index, long id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously indexes a percolate request against the specified index and document ID, enabling the query to be stored and evaluated later.
        /// </summary>
        /// <param name="percolate">The request containing the percolation query to be indexed.</param>
        /// <param name="index">The name of the index where the percolate request will be stored.</param>
        /// <param name="id">The unique identifier for the document being indexed.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous indexing operation, containing the response indicating success or failure.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public async Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolate, string index, long id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Executes a percolate request against the specified index, allowing the given query to be evaluated against the stored percolation queries.
        /// </summary>
        /// <param name="percolate">The request containing the query to be percolated, which will be matched against the percolation queries in the specified index.</param>
        /// <param name="index">The name of the index where the percolation queries are stored.</param>
        /// <returns>A response containing the results of the percolate operation, including any matching queries.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public PercolateResponse Percolate(PercolateRequest percolate, string index) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously executes a percolate request against the specified index, allowing the given query to be evaluated against the stored percolation queries.
        /// </summary>
        /// <param name="percolate">The request containing the query to be percolated, which will be matched against the percolation queries in the specified index.</param>
        /// <param name="index">The name of the index where the percolation queries are stored.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous percolate operation, containing the response with the results of the percolate operation.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public async Task<PercolateResponse> PercolateAsync(PercolateRequest percolate, string index, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/search", HttpMethod.Post, cancellationToken);

        /// <summary>
        /// Updates an existing percolate request at the specified index and document ID, with an option to refresh the index immediately.
        /// </summary>
        /// <param name="percolate">The request containing the updated percolation query.</param>
        /// <param name="index">The name of the index where the percolate request is stored.</param>
        /// <param name="id">The unique identifier for the document being updated.</param>
        /// <returns>A response indicating the success or failure of the update operation.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public PercolateResponse UpdatePercolate(PercolationActionRequest percolate, string index, int id) =>
            ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously updates an existing percolate request at the specified index and document ID, with an option to refresh the index immediately.
        /// </summary>
        /// <param name="percolate">The request containing the updated percolation query.</param>
        /// <param name="index">The name of the index where the percolate request is stored.</param>
        /// <param name="id">The unique identifier for the document being updated.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous update operation, containing the response indicating success or failure.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public async Task<PercolateResponse> UpdatePercolateAsync(PercolationActionRequest percolate, string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessPercolateAsync(percolate, $"/pq/{index}/doc/{id}?refresh=1", HttpMethod.Put, cancellationToken);

        /// <summary>
        /// Retrieves a specific percolate request by its ID from the specified index.
        /// </summary>
        /// <param name="index">The name of the index where the percolate request is stored.</param>
        /// <param name="id">The unique identifier for the percolate request to be retrieved.</param>
        /// <returns>A response containing the percolate request's details if found, or an error message if not.</returns>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public ManticoreResponse<SearchSuccess, ErrorMessage> GetPercolate(string index, int id) =>
            ProcessGetPercolateAsync(index, id).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously retrieves a specific percolate request by its ID from the specified index.
        /// </summary>
        /// <param name="index">The name of the index where the percolate request is stored.</param>
        /// <param name="id">The unique identifier for the percolate request to be retrieved.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous retrieval operation, containing a response with the percolate request's details or an error message.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="PercolateException">Thrown if an error occurred while performing percolation.</exception>
        public async Task<ManticoreResponse<SearchSuccess, ErrorMessage>> GetPercolateAsync(string index, int id, CancellationToken cancellationToken = default) =>
            await ProcessGetPercolateAsync(index, id, cancellationToken);

        /// <summary>
        /// Retrieves autocomplete suggestions based on the provided autocomplete request.
        /// </summary>
        /// <param name="autocomplete">The request object containing the parameters for the autocomplete operation.</param>
        /// <returns>A response containing a list of autocomplete suggestions or an error message if the operation fails.</returns>
        /// <exception cref="AutocompleteException">Thrown if an error occurred while performing.</exception>
        public ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage> Autocomplete(AutocompleteRequest autocomplete) =>
            ProcessAutocompleteAsync(autocomplete).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously retrieves autocomplete suggestions based on the provided autocomplete request.
        /// </summary>
        /// <param name="autocomplete">The request object containing the parameters for the autocomplete operation.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous autocomplete operation, containing a response with a list of autocomplete suggestions or an error message.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="AutocompleteException">Thrown if an error occurred while performing.</exception>
        public async Task<ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>> AutocompleteAsync(AutocompleteRequest autocomplete, CancellationToken cancellationToken = default) =>
            await ProcessAutocompleteAsync(autocomplete, cancellationToken);

        /// <summary>
        /// Defines a new table structure in Manticore search engine using the specified mapping properties, mimicking Elasticsearch-like table definitions.
        /// </summary>
        /// <param name="properties">The request object containing the mapping properties that describe the table structure to be created.</param>
        /// <param name="index">The name of the index for which the new table structure will be defined.</param>
        /// <returns>A response containing a list of successful mapping operations or an error message if the operation fails.</returns>
        /// <exception cref="MappingException">Thrown if an error occurred while performing mapping.</exception>
        public ManticoreResponse<List<MappingSuccess>, ErrorMessage> UseMapping(MappingRequest properties, string index) =>
            ProcessMappingAsync(properties, index).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously defines a new table structure in Manticore search engine using the specified mapping properties, mimicking Elasticsearch-like table definitions.
        /// </summary>
        /// <param name="properties">The request object containing the mapping properties that describe the table structure to be created.</param>
        /// <param name="index">The name of the index for which the new table structure will be defined.</param>
        /// <param name="cancellationToken">A cancellation token to allow the operation to be canceled if needed.</param>
        /// <returns>A task representing the asynchronous mapping operation, containing a response with a list of successful mapping operations or an error message.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        /// <exception cref="MappingException">Thrown if an error occurred while performing mapping.</exception>
        public async Task<ManticoreResponse<List<MappingSuccess>, ErrorMessage>> UseMappingAsync(MappingRequest properties, string index, CancellationToken cancellationToken = default) =>
            await ProcessMappingAsync(properties, index, cancellationToken);

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)               
                    _httpClient.Dispose();               

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

        private async Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ProcessModificationAsync(ModificationRequest request, string endpoint, CancellationToken cancellationToken = default)
        {
            if (request == null || request.Document == null)
            {
                throw new ModificationException(string.Format(ProviderError.ArgumentNull,
                    request == null ? nameof(request) : nameof(request.Document)));
            }

            if (request.Document.Count <= 0)
                throw new ModificationException(ProviderError.DocumentCount);

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

        private async Task<ManticoreResponse<BulkSuccess, List<BulkError>>> ProcessBulkAsync<TBulkRequest>(IEnumerable<TBulkRequest> documents, CancellationToken cancellationToken = default)
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
                    result.Response = JsonConvert.DeserializeObject<BulkSuccess>(response.Response);
                    result.IsSuccess = true;
                }
                catch
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

        private async Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> ProcessUpdateAsync(UpdateRequest document, CancellationToken cancellationToken = default)
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
    }
}
