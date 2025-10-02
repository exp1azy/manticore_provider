using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;
using ManticoreSearch.Provider.Models.Responses;

namespace ManticoreSearch.Provider.Interfaces
{
    /// <summary>
    /// Represents an interface that provides a client for interacting with the Manticore Search server.
    /// </summary>
    public interface IManticoreProvider : IDisposable
    {
        /// <summary>
        /// Inserts a new document into the specified table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="insertRequest">The modification request containing document data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous insert operation.</returns>
        /// <exception cref="ModificationException">Thrown when an error occurs during the insert operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> InsertAsync<TDocument>(ModificationRequest<TDocument> insertRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Executes a raw SQL query against Manticore Search server asynchronously.
        /// </summary>
        /// <param name="sql">The SQL query string to execute.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous SQL execution.</returns>
        /// <exception cref="SqlException">Thrown when an error occurs during the SQL execution.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs bulk insert operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to insert. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk insert requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<BulkMessage, List<BulkError>>> BulkAsync<TDocument>(List<BulkInsertRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Replaces an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="replaceRequest">The modification request containing replacement data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous replace operation.</returns>
        /// <exception cref="ModificationException">Thrown when an error occurs during the replace operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<ModificationSuccess, ErrorResponse>> ReplaceAsync<TDocument>(ModificationRequest<TDocument> replaceRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Performs bulk replace operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to replace. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk replace requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk replace operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk replace operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<BulkMessage, List<BulkError>>> BulkReplaceAsync<TDocument>(List<BulkReplaceRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Updates an existing document in the table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="updateRequest">The update request containing modification data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous update operation.</returns>
        /// <exception cref="UpdateException">Thrown when an error occurs during the update operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<UpdateSuccess, ErrorResponse>> UpdateAsync<TDocument>(UpdateRequest<TDocument> updateRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Performs bulk update operations asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of documents to update. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="bulkRequests">List of bulk update requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk update operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk update operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<BulkMessage, List<BulkError>>> BulkUpdateAsync<TDocument>(List<BulkUpdateRequest<TDocument>> bulkRequests, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Executes a search query against the Manticore Search table asynchronously.
        /// </summary>
        /// <param name="searchRequest">The search request containing query parameters and options.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous search operation with full information in the response.</returns>
        /// <exception cref="SearchException">Thrown when an error occurs during the search operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<FullSearchResponse, ErrorMessage>> SearchAsync(SearchRequest searchRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a search query against the Manticore Search table asynchronously.
        /// </summary>
        /// <typeparam name="TDocument">The type of document to return in search results. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
        /// <param name="searchRequest">The search request containing query parameters and options.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous search operation with hits based on specified documents <typeparamref name="TDocument"/>.</returns>
        /// <exception cref="SearchException">Thrown when an error occurs during the search operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<SearchResult<TDocument>, ErrorMessage>> SearchAsync<TDocument>(SearchRequest searchRequest, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

        /// <summary>
        /// Executes a search query against the Manticore Search table asynchronously.
        /// </summary>
        /// <param name="searchRequest">The search request containing query parameters and options.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous search operation with raw JSON string response.</returns>
        /// <exception cref="SearchException">Thrown when an error occurs during the search operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<string> SearchRawAsync(SearchRequest searchRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes documents from the table asynchronously based on the provided criteria.
        /// </summary>
        /// <param name="deleteRequest">The delete request containing document identifiers.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous delete operation.</returns>
        /// <exception cref="DeleteException">Thrown when an error occurs during the delete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<DeleteResponse> DeleteAsync(DeleteRequest deleteRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs bulk delete operations asynchronously.
        /// </summary>
        /// <param name="bulkRequests">List of bulk delete requests.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous bulk delete operation.</returns>
        /// <exception cref="BulkException">Thrown when an error occurs during the bulk delete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<BulkMessage, List<BulkError>>> BulkDeleteAsync(List<BulkDeleteRequest> bulkRequests, CancellationToken cancellationToken = default);

        /// <summary>
        /// Submits a percolate request asynchronously to match a document against existing percolation queries.
        /// </summary>
        /// <param name="percolateRequest">The percolation request containing the document.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous percolation operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the percolation operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolateRequest, string index, CancellationToken cancellationToken = default);

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
        public Task<PercolateResponse> IndexPercolateAsync(PercolationActionRequest percolateRequest, string index, long id, CancellationToken cancellationToken = default);

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
        public Task<PercolateResponse> PercolateAsync<TDocument>(PercolateRequest<TDocument> percolateRequest, string index, CancellationToken cancellationToken = default)
            where TDocument : ManticoreDocument;

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
        public Task<PercolateResponse> UpdatePercolateAsync(PercolationActionRequest percolateRequest, string index, int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a specific percolate request asynchronously by its ID from the specified index.
        /// </summary>
        /// <param name="index">The index name where the percolate request is stored.</param>
        /// <param name="id">The identifier of the percolate request to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous retrieval operation.</returns>
        /// <exception cref="PercolateException">Thrown when an error occurs during the retrieval operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<FullSearchResponse, ErrorMessage>> GetPercolateAsync(string index, int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves autocomplete suggestions asynchronously based on the provided request.
        /// </summary>
        /// <param name="autocompleteRequest">The autocomplete request parameters.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous autocomplete operation.</returns>
        /// <exception cref="AutocompleteException">Thrown when an error occurs during the autocomplete operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<List<AutocompleteSuccess>, ErrorMessage>> AutocompleteAsync(AutocompleteRequest autocompleteRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Defines a new table structure asynchronously using mapping properties.
        /// </summary>
        /// <param name="mappingRequest">The mapping properties describing the table structure.</param>
        /// <param name="index">The target index name.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing the asynchronous mapping operation.</returns>
        /// <exception cref="MappingException">Thrown when an error occurs during the mapping operation.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
        public Task<ManticoreResponse<List<MappingSuccess>, ErrorMessage>> UseMappingAsync(MappingRequest mappingRequest, string index, CancellationToken cancellationToken = default);
    }
}
