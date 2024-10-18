using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;
using ManticoreSearch.Api.Models.Responses;
using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.Text;

namespace ManticoreSearch.Api
{
    /// <summary>
    /// Provides an API for interacting with the Manticoresearch search engine.
    /// </summary>
    public sealed class ManticoreProvider : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a default connection to the Manticoresearch server at http://localhost:9308.
        /// </summary>
        public ManticoreProvider()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:9308")
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a custom server address.
        /// </summary>
        /// <param name="baseAddress">The base address of the Manticoresearch server.</param>
        public ManticoreProvider(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="ManticoreProvider"/> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ManticoreProvider"/> and optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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

        /// <summary>
        /// Sends an asynchronous HTTP request to the specified endpoint and returns the deserialized response or a string result.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response. If the type is <see cref="string"/>, the method returns the raw string response.</typeparam>
        /// <param name="endpoint">The API endpoint to which the request is sent.</param>
        /// <param name="method">The HTTP method (GET, POST, PUT, DELETE, etc.) used for the request.</param>
        /// <param name="content">The HTTP content to be sent in the request body, typically in JSON format.</param>
        /// <param name="cancellationToken">An optional token to cancel the request.</param>
        /// <returns>A task representing the asynchronous operation, with a result of type <typeparamref name="TResponse"/>.</returns>
        /// <exception cref="HttpRequestFailureException">Thrown if the HTTP request fails (non-200 status code).</exception>
        private async Task<TResponse> SendAsync<TResponse>(string endpoint, HttpMethod method, StringContent content, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestFailureException(response.ToString()!);

            var stringResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (typeof(TResponse) == typeof(string))
                return (TResponse)(object)stringResponse;

            return JsonConvert.DeserializeObject<TResponse>(stringResponse)!;
        }

        /// <summary>
        /// Sends a synchronous HTTP request to the specified endpoint and returns the deserialized response or a string result.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response. If the type is <see cref="string"/>, the method returns the raw string response.</typeparam>
        /// <param name="endpoint">The API endpoint to which the request is sent.</param>
        /// <param name="method">The HTTP method (GET, POST, PUT, DELETE, etc.) used for the request.</param>
        /// <param name="content">The HTTP content to be sent in the request body, typically in JSON format.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResponse"/> or a string result if <typeparamref name="TResponse"/> is <see cref="string"/>.</returns>
        /// <exception cref="HttpRequestFailureException">Thrown if the HTTP request fails (non-200 status code).</exception>
        private TResponse Send<TResponse>(string endpoint, HttpMethod method, StringContent content)
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestFailureException(response.ToString()!);

            var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (typeof(TResponse) == typeof(string))
                return (TResponse)(object)stringResponse;

            return JsonConvert.DeserializeObject<TResponse>(stringResponse)!;
        }

        /// <summary>
        /// Serializes an object to JSON and creates a <see cref="StringContent"/> with the specified content type.
        /// </summary>
        /// <param name="data">The object to be serialized to JSON.</param>
        /// <param name="contentType">The MIME type of the content (e.g., "application/json").</param>
        /// <returns>A <see cref="StringContent"/> object containing the serialized JSON data.</returns>
        private StringContent CreateStringContentFromJson(object data, string contentType)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, contentType);
        }

        /// <summary>
        /// Executes a SQL query synchronously against the Manticoresearch engine.
        /// </summary>
        /// <param name="sql">The SQL query to be executed.</param>
        /// <returns>A string representing the result of the SQL query.</returns>
        /// <exception cref="NullException">Thrown if the provided SQL query is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public string Sql(string sql)
        {
            if (sql == null)
                throw new NullException(nameof(sql));

            try
            {
                var content = new StringContent(sql);

                return Send<string>("/cli", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a SQL query asynchronously against the Manticoresearch engine.
        /// </summary>
        /// <param name="sql">The SQL query to be executed.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a string containing the result of the SQL query.</returns>
        /// <exception cref="NullException">Thrown if the provided SQL query is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<string> SqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            if (sql == null)
                throw new NullException(nameof(sql));

            try
            {
                var content = new StringContent(sql);

                return await SendAsync<string>("/cli", HttpMethod.Post, content, cancellationToken);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Synchronously adds a new document to the index.
        /// </summary>
        /// <param name="document">The document to be added to the index.</param>
        /// <returns>Returns the result of the HTTP request to insert the document, encapsulated in a <see cref="InsertResponse"/>.</returns>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public InsertResponse Insert(InsertRequest document)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (document.Document.Count == 0)
                throw new InsertException(ProviderError.DocumentRequired);

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<InsertResponse>("/insert", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously adds a new document to the index.
        /// </summary>
        /// <param name="document">The document to be added to the index.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>Returns a task that represents the asynchronous operation. The task result contains the response as <see cref="InsertResponse"/>.</returns>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<InsertResponse> InsertAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (document.Document.Count == 0)
                throw new InsertException(ProviderError.DocumentRequired);

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<InsertResponse>("/insert", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Synchronously performs bulk loading of documents into the index.
        /// </summary>
        /// <param name="documents">A collection of documents to be added to the index.</param>
        /// <returns>Returns the result of the HTTP request to bulk add documents as <see cref="BulkSuccessResponse"/>.</returns>
        /// <exception cref="NullException">Thrown if the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public object Bulk(IEnumerable<BulkInsertRequest> documents)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return Send<object>("/bulk", HttpMethod.Post, content);             
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously performs bulk loading of documents into the index.
        /// </summary>
        /// <param name="documents">A collection of documents to be added to the index.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>Returns a task that represents the asynchronous operation. The task result contains the response as <see cref="BulkSuccessResponse"/>.</returns>
        /// <exception cref="NullException">Thrown if the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<object> BulkAsync(IEnumerable<BulkInsertRequest> documents, CancellationToken cancellationToken = default)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return await SendAsync<object>("/bulk", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Replaces a document in the index synchronously.
        /// </summary>
        /// <param name="document">The document to be replaced in the index.</param>
        /// <returns>Returns the result of the HTTP request to replace the document as <see cref="InsertResponse"/>.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public InsertResponse Replace(InsertRequest document)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/x-ndjson");

                return Send<InsertResponse>("/replace", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Replaces a document in the index asynchronously.
        /// </summary>
        /// <param name="document">The document to be replaced in the index.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>Returns the result of the HTTP request to replace the document as <see cref="InsertResponse"/>.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<InsertResponse> ReplaceAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/x-ndjson");

                return await SendAsync<InsertResponse>("/replace", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs bulk replacement of documents in the index synchronously.
        /// </summary>
        /// <param name="documents">The collection of documents to be replaced in the index.</param>
        /// <returns>Returns the result of the HTTP request as <see cref="BulkSuccessResponse"/> indicating the outcome of the bulk replace operation.</returns>
        /// <exception cref="NullException">Thrown when the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public object BulkReplace(IEnumerable<BulkReplaceRequest> documents)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return Send<object>("/bulk", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs bulk replacement of documents in the index asynchronously.
        /// </summary>
        /// <param name="documents">The collection of documents to be replaced in the index.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>Returns the result of the HTTP request as <see cref="BulkSuccessResponse"/> indicating the outcome of the bulk replace operation.</returns>
        /// <exception cref="NullException">Thrown when the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<object> BulkReplaceAsync(IEnumerable<BulkReplaceRequest> documents, CancellationToken cancellationToken = default)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return await SendAsync<object>("/bulk", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a document in the index synchronously.
        /// </summary>
        /// <param name="document">The document containing the updated information.</param>
        /// <returns>Returns the result of the HTTP request as <see cref="UpdateResponse"/> indicating the outcome of the update operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public UpdateResponse Update(UpdateRequest document)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<UpdateResponse>("/update", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a document in the index asynchronously.
        /// </summary>
        /// <param name="document">The document containing the updated information.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>Returns the result of the HTTP request as <see cref="UpdateResponse"/> indicating the outcome of the update operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<UpdateResponse> UpdateAsync(UpdateRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<UpdateResponse>("/update", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a synchronous search on the index using the specified query.
        /// </summary>
        /// <param name="request">The search query containing criteria and parameters for the search operation.</param>
        /// <returns>Returns the result of the HTTP search request as <see cref="SearchResponse"/>, encapsulating the search results.</returns>
        /// <exception cref="NullException">Thrown when the provided request is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public SearchResponse Search(SearchRequest request)
        {
            try
            {
                var content = CreateStringContentFromJson(request, "application/json");

                return Send<SearchResponse>("/search", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes an asynchronous search on the index using the specified query.
        /// </summary>
        /// <param name="request">The search query containing criteria and parameters for the search operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the asynchronous operation.</param>
        /// <returns>Returns the result of the HTTP search request as <see cref="SearchResponse"/>, encapsulating the search results.</returns>
        /// <exception cref="NullException">Thrown when the provided request is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(request, "application/json");

                return await SendAsync<SearchResponse>("/search", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a synchronous operation to delete a specified document from the index.
        /// </summary>
        /// <param name="document">The document to be deleted, containing the necessary identifier for the operation.</param>
        /// <returns>Returns the result of the HTTP delete request as <see cref="DeleteResponse"/>, indicating the outcome of the deletion operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public DeleteResponse Delete(DeleteRequest document)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<DeleteResponse>("/delete", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes an asynchronous operation to delete a specified document from the index.
        /// </summary>
        /// <param name="document">The document to be deleted, containing the necessary identifier for the operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the asynchronous operation.</param>
        /// <returns>Returns the result of the HTTP delete request as <see cref="DeleteResponse"/>, indicating the outcome of the deletion operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<DeleteResponse> DeleteAsync(DeleteRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<DeleteResponse>("/delete", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a synchronous operation to delete multiple documents from the index in bulk.
        /// </summary>
        /// <param name="documents">A collection of documents to be deleted, each containing the necessary identifiers for deletion.</param>
        /// <returns>Returns the result of the HTTP bulk delete request as <see cref="BulkSuccessResponse"/>, indicating the outcome of the deletion operation for all specified documents.</returns>
        /// <exception cref="NullException">Thrown when the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public BulkSuccessResponse BulkDelete(IEnumerable<BulkDeleteRequest> documents)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return Send<BulkSuccessResponse>("/bulk", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes an asynchronous operation to delete multiple documents from the index in bulk.
        /// </summary>
        /// <param name="documents">A collection of documents to be deleted, each containing the necessary identifiers for deletion.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the asynchronous operation.</param>
        /// <returns>Returns the result of the HTTP bulk delete request as <see cref="BulkSuccessResponse"/>, indicating the outcome of the deletion operation for all specified documents.</returns>
        /// <exception cref="NullException">Thrown when the provided documents collection is null.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<BulkSuccessResponse> BulkDeleteAsync(IEnumerable<BulkDeleteRequest> documents, CancellationToken cancellationToken = default)
        {
            if (documents == null)
                throw new NullException(nameof(documents));

            try
            {
                var json = string.Join("\n", documents.Select(JsonConvert.SerializeObject));
                var content = new StringContent(json, Encoding.UTF8, "application/x-ndjson");

                return await SendAsync<BulkSuccessResponse>("/bulk", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Synchronously indexes a query document for future percolation use.
        /// </summary>
        /// <param name="document">The query document to be indexed.</param>
        /// <param name="index">The name of the index where the query will be stored.</param>
        /// <returns>Returns the result of the HTTP request for indexing queries as <see cref="SearchResponse"/>, indicating the success or failure of the indexing operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document or index name is null or empty.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public SearchResponse IndexPercolate(IndexPercolateRequest document, string index)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (index == null)
                throw new NullException(nameof(index));

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<SearchResponse>($"/pq/{index}/doc/", HttpMethod.Put, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously indexes a query document for future percolation use.
        /// </summary>
        /// <param name="document">The query document to be indexed.</param>
        /// <param name="index">The name of the index where the query will be stored.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the asynchronous operation.</param>
        /// <returns>Returns the result of the HTTP request for indexing queries as <see cref="SearchResponse"/>, indicating the success or failure of the indexing operation.</returns>
        /// <exception cref="NullException">Thrown when the provided document or index name is null or empty.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<SearchResponse> IndexPercolateAsync(IndexPercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (index == null)
                throw new NullException(nameof(index));

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<SearchResponse>($"/pq/{index}/doc/", HttpMethod.Put, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Synchronously executes a percolation search for the specified query document against the given index.
        /// </summary>
        /// <param name="document">The query document to be percolated.</param>
        /// <param name="index">The name of the index to search against.</param>
        /// <returns>Returns the result of the HTTP request for percolation search as <see cref="SearchResponse"/>, containing matches found for the provided query.</returns>
        /// <exception cref="NullException">Thrown when the provided document or index name is null or empty.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public SearchResponse Percolate(PercolateRequest document, string index)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (index == null)
                throw new NullException(nameof(index));

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<SearchResponse>($"/pq/{index}/_search", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously executes a percolation search for the specified query document against the given index.
        /// </summary>
        /// <param name="document">The query document to be percolated.</param>
        /// <param name="index">The name of the index to search against.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the asynchronous operation.</param>
        /// <returns>Returns the result of the HTTP request for percolation search as <see cref="SearchResponse"/>, containing matches found for the provided query.</returns>
        /// <exception cref="NullException">Thrown when the provided document or index name is null or empty.</exception>
        /// <exception cref="HttpRequestFailureException">Thrown when the HTTP request fails.</exception>
        public async Task<SearchResponse> PercolateAsync(PercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (index == null)
                throw new NullException(nameof(index));

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<SearchResponse>($"/pq/{index}/_search", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
