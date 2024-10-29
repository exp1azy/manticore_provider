using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;
using ManticoreSearch.Api.Models.Responses;
using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.Text;

namespace ManticoreSearch.Api
{
    /// <summary>
    /// Provides an API for interacting with the Manticore Search engine.
    /// </summary>
    public sealed class ManticoreProvider : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManticoreProvider"/> class with a default connection to the Manticore Search server at http://localhost:9308.
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
        /// <param name="baseAddress">The base address of the Manticore Search server.</param>
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
        /// Asynchronously sends an HTTP request to the specified endpoint and returns the deserialized response.
        /// This method is designed to handle HTTP operations with a specified HTTP method and content,
        /// making it reusable for various types of requests within the library.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response to be deserialized from the HTTP response content.</typeparam>
        /// <param name="endpoint">The URL of the endpoint to which the request is sent.</param>
        /// <param name="method">The HTTP method to be used for the request (e.g., GET, POST, PUT, DELETE).</param>
        /// <param name="content">The content of the request, typically in JSON format, wrapped in a <see cref="StringContent"/> object.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. Default is <see cref="default"/>.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the deserialized response of type <typeparamref name="TResponse"/>.
        /// If <typeparamref name="TResponse"/> is <see cref="string"/>, the raw response content is returned.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP response indicates a failure (i.e., when <c>response.IsSuccessStatusCode</c> is false).
        /// The exception contains the string representation of the HTTP response for debugging purposes.
        /// </exception>
        private async Task<TResponse> SendAsync<TResponse>(string endpoint, HttpMethod method, StringContent content, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var stringResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (typeof(TResponse) == typeof(string) || !response.IsSuccessStatusCode)
                return (TResponse)(object)stringResponse;

            return JsonConvert.DeserializeObject<TResponse>(stringResponse)!;
        }

        /// <summary>
        /// Sends an HTTP request synchronously to the specified endpoint and returns the deserialized response.
        /// This method is designed to handle HTTP operations with a specified HTTP method and content,
        /// making it reusable for various types of requests within the library.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response to be deserialized from the HTTP response content.</typeparam>
        /// <param name="endpoint">The URL of the endpoint to which the request is sent.</param>
        /// <param name="method">The HTTP method to be used for the request (e.g., GET, POST, PUT, DELETE).</param>
        /// <param name="content">The content of the request, typically in JSON format, wrapped in a <see cref="StringContent"/> object.</param>
        /// <returns>
        /// The deserialized response of type <typeparamref name="TResponse"/>. If <typeparamref name="TResponse"/> is <see cref="string"/>,
        /// the raw response content is returned.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP response indicates a failure (i.e., when <c>response.IsSuccessStatusCode</c> is false).
        /// The exception contains the string representation of the HTTP response for debugging purposes.
        /// </exception>
        private TResponse Send<TResponse>(string endpoint, HttpMethod method, StringContent content)
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
            var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (typeof(TResponse) == typeof(string) || !response.IsSuccessStatusCode)
                return (TResponse)(object)stringResponse;

            return JsonConvert.DeserializeObject<TResponse>(stringResponse)!;
        }

        /// <summary>
        /// Creates a <see cref="StringContent"/> object from the provided data serialized in JSON format.
        /// This method is useful for preparing the body of HTTP requests that require JSON content type.
        /// </summary>
        /// <param name="data">The object to be serialized into JSON. This can be any object that is serializable by JSON.NET.</param>
        /// <param name="contentType">The content type to be set for the <see cref="StringContent"/>. Common values include "application/json" or "text/json".</param>
        /// <returns>A <see cref="StringContent"/> instance containing the serialized JSON string and the specified content type.</returns>
        private StringContent CreateStringContentFromJson(object data, string contentType)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, contentType);
        }

        /// <summary>
        /// Executes a SQL query against the Manticore Search API.
        /// This method sends the provided SQL query to the Manticore Search server
        /// via a POST request to the specified endpoint and returns the response as a string.
        /// </summary>
        /// <param name="sql">The SQL query to be executed. This must be a valid SQL statement recognized by Manticore Search.</param>
        /// <returns>A string containing the response from the Manticore Search API after executing the SQL query.</returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="sql"/> parameter is null. This ensures that a valid SQL statement is provided for execution.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Asynchronously executes a SQL query against the Manticore Search API.
        /// This method sends the provided SQL query to the Manticore Search server
        /// via a POST request to the specified endpoint and returns the response as a string.
        /// </summary>
        /// <param name="sql">The SQL query to be executed. This must be a valid SQL statement recognized by Manticore Search.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation. Defaults to <see langword="default"/>.</param>
        /// <returns>A task that represents the asynchronous operation, containing a string with the response from the Manticore Search API after executing the SQL query.</returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="sql"/> parameter is null. This ensures that a valid SQL statement is provided for execution.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Inserts a new document into the Manticore Search index.
        /// This method sends a request to the Manticore Search server to insert the specified document.
        /// </summary>
        /// <param name="document">The <see cref="InsertRequest"/> object containing the document to be inserted.
        /// This must include valid fields as defined in the Manticore Search index.</param>
        /// <returns>An <see cref="InsertResponse"/> object containing the response from the Manticore Search API, 
        /// including information about the insertion result.</returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="document"/> parameter is null. This ensures that a valid insert request is provided.
        /// </exception>
        /// <exception cref="InsertException">
        /// Thrown when the <paramref name="document"/> does not contain any fields. This ensures that at least one field is provided
        /// for the document to be inserted.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
        public object Insert(InsertRequest document)
        {
            if (document.Document == null)
                throw new NullException(nameof(document));

            if (document.Document.Count <= 0)
                throw new InsertException(ProviderError.InsertError);

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<object>("/insert", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously inserts a new document into the Manticore Search index.
        /// This method sends an asynchronous request to the Manticore Search server to insert the specified document.
        /// </summary>
        /// <param name="document">The <see cref="InsertRequest"/> object containing the document to be inserted.
        /// This must include valid fields as defined in the Manticore Search index.</param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        /// </param>
        /// <returns>A task representing the asynchronous operation. The task result is an <see cref="InsertResponse"/> object
        /// containing the response from the Manticore Search API, including information about the insertion result.</returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="document"/> parameter is null. This ensures that a valid insert request is provided.
        /// </exception>
        /// <exception cref="InsertException">
        /// Thrown when the <paramref name="document"/> does not contain any fields. This ensures that at least one field is provided
        /// for the document to be inserted.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
        public async Task<object> InsertAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new NullException(nameof(document));

            if (document.Document.Count == 0)
                throw new InsertException(ProviderError.DocumentRequired);

            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<object>("/insert", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sends a bulk insert request to the Manticore Search server.
        /// This method allows for multiple documents to be inserted in a single request,
        /// improving performance by reducing the number of network calls.
        /// </summary>
        /// <param name="documents">An <see cref="IEnumerable{BulkInsertRequest}"/> containing the documents to be inserted.
        /// Each document must be structured according to the Manticore Search index requirements.</param>
        /// <returns>
        /// An object representing the response from the Manticore Search API.
        /// This response will provide information about the success or failure of the bulk insert operation.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection of documents
        /// is provided for the bulk operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Asynchronously sends a bulk insert request to the Manticore Search server.
        /// This method allows for multiple documents to be inserted in a single request,
        /// improving performance by reducing the number of network calls.
        /// </summary>
        /// <param name="documents">An <see cref="IEnumerable{BulkInsertRequest}"/> containing the documents to be inserted.
        /// Each document must be structured according to the Manticore Search index requirements.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the asynchronous operation to complete.
        /// This can be used to cancel the operation if necessary.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an object
        /// representing the response from the Manticore Search API, providing information about the success
        /// or failure of the bulk insert operation.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection of documents
        /// is provided for the bulk operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Replaces an existing document in the Manticore Search index with a new document.
        /// If the document with the specified ID does not exist, it will be created.
        /// This method allows for updating the contents of a document while maintaining its ID.
        /// </summary>
        /// <param name="document">An <see cref="InsertRequest"/> object containing the document data.
        /// The document must include the ID of the existing document to be replaced and the new content
        /// that will replace the old content.</param>
        /// <returns>
        /// An <see cref="InsertResponse"/> object containing the response from the Manticore Search API,
        /// which indicates whether the replace operation was successful and provides additional details.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="document"/> parameter is null. This ensures that a valid document
        /// is provided for the replace operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
        public object Replace(InsertRequest document)
        {
            if (document == null || document.Document == null)
                throw new NullException();

            if (document.Document.Count == 0)
                throw new ReplaceException();

            try
            {
                var content = CreateStringContentFromJson(document, "application/x-ndjson");

                return Send<object>("/replace", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously replaces an existing document in the Manticore Search index with a new document.
        /// If the document with the specified ID does not exist, it will be created.
        /// This method allows for updating the contents of a document while maintaining its ID.
        /// </summary>
        /// <param name="document">An <see cref="InsertRequest"/> object containing the document data.
        /// The document must include the ID of the existing document to be replaced and the new content
        /// that will replace the old content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>
        /// An asynchronous <see cref="Task{InsertResponse}"/> representing the result of the replace operation.
        /// The task result will be an <see cref="InsertResponse"/> object containing the response from the 
        /// Manticore Search API, which indicates whether the replace operation was successful and provides 
        /// additional details.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="document"/> parameter is null. This ensures that a valid document
        /// is provided for the replace operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
        public async Task<object> ReplaceAsync(InsertRequest document, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new NullException();

            if (document.Document.Count == 0)
                throw new ReplaceException();

            try
            {
                var content = CreateStringContentFromJson(document, "application/x-ndjson");

                return await SendAsync<object>("/replace", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Replaces multiple existing documents in the Manticore Search index with new documents in bulk.
        /// Each document in the provided collection will replace the corresponding document in the index
        /// based on its ID. If a document does not exist, it will be created instead.
        /// This method is optimized for bulk operations to improve performance when handling large datasets.
        /// </summary>
        /// <param name="documents">An <see cref="IEnumerable{BulkReplaceRequest}"/> containing the documents
        /// to be replaced. Each document must specify the ID of the existing document to be replaced, as well
        /// as the new content that will replace the old content.</param>
        /// <returns>
        /// An <see cref="object"/> representing the response from the Manticore Search API after performing
        /// the bulk replace operation. The response will indicate the success or failure of the operation for
        /// each document processed.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection
        /// of documents is provided for the bulk replace operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Asynchronously replaces multiple existing documents in the Manticore Search index with new documents in bulk.
        /// Each document in the provided collection will replace the corresponding document in the index
        /// based on its ID. If a document does not exist, it will be created instead.
        /// This method is optimized for bulk operations to improve performance when handling large datasets.
        /// </summary>
        /// <param name="documents">An <see cref="IEnumerable{BulkReplaceRequest}"/> containing the documents
        /// to be replaced. Each document must specify the ID of the existing document to be replaced, as well
        /// as the new content that will replace the old content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the
        /// asynchronous operation if needed.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is an <see cref="object"/> 
        /// representing the response from the Manticore Search API after performing the bulk replace operation.
        /// The response will indicate the success or failure of the operation for each document processed.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection
        /// of documents is provided for the bulk replace operation.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur if the server is unreachable,
        /// the request is malformed, or the API returns an error status code.
        /// </exception>
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
        /// Updates an existing document in the Manticore Search index. This method allows for partial updates
        /// to the document specified in the <paramref name="document"/> parameter. If the document does not exist,
        /// it will not be created.
        /// </summary>
        /// <param name="document">An <see cref="UpdateRequest"/> instance containing the details of the document
        /// to be updated, including the index name, document ID, and the fields to be modified.</param>
        /// <returns>
        /// An <see cref="UpdateResponse"/> object representing the response from the Manticore Search API
        /// after performing the update operation. This response includes information such as the index,
        /// document ID, and the result of the update operation.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public object Update(UpdateRequest document)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<object>("/update", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing document in the Manticore Search index. This method allows for partial updates
        /// to the document specified in the <paramref name="document"/> parameter. If the document does not exist,
        /// it will not be created.
        /// </summary>
        /// <param name="document">An <see cref="UpdateRequest"/> instance containing the details of the document
        /// to be updated, including the index name, document ID, and the fields to be modified.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to propagate notification
        /// that operations should be canceled. This is useful for long-running operations and helps prevent resource
        /// leakage by allowing callers to cancel the operation.</param>
        /// <returns>
        /// An <see cref="UpdateResponse"/> object representing the response from the Manticore Search API
        /// after performing the update operation. This response includes information such as the index,
        /// document ID, and the result of the update operation.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public async Task<object> UpdateAsync(UpdateRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<object>("/update", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Synchronously searches the Manticore Search index using the specified search request parameters.
        /// This method sends a query to the Manticore Search API and returns the search results synchronously.
        /// </summary>
        /// <param name="request">An instance of <see cref="SearchRequest"/> that contains the search parameters
        /// including the query string, filters, and pagination options.</param>
        /// <returns>
        /// An instance of <see cref="SearchResponse"/> that includes the results of the search operation, 
        /// including the number of documents found, time taken for the search, and the actual hits.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
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
        /// Asynchronously searches the Manticore Search index using the specified search request parameters.
        /// This method sends a query to the Manticore Search API and returns the search results asynchronously.
        /// </summary>
        /// <param name="request">An instance of <see cref="SearchRequest"/> that contains the search parameters
        /// including the query string, filters, and pagination options.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation. This is useful 
        /// for managing long-running tasks and ensuring responsive applications.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an instance of 
        /// <see cref="SearchResponse"/> that includes the results of the search operation, 
        /// including the number of documents found, time taken for the search, and the actual hits.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
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
        /// Synchronously deletes a document from the Manticore Search index based on the specified delete request.
        /// This method sends a delete request to the Manticore Search API to remove a document identified by the 
        /// provided parameters.
        /// </summary>
        /// <param name="document">An instance of <see cref="DeleteRequest"/> that contains the parameters necessary
        /// to identify the document to be deleted, such as the document ID or any specific criteria required 
        /// for deletion.</param>
        /// <returns>
        /// An instance of <see cref="DeleteResponse"/> that indicates the result of the delete operation, including
        /// the status of the operation and whether the document was found and successfully deleted.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public object Delete(DeleteRequest document)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<object>("/delete", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a document from the Manticore Search index based on the specified delete request.
        /// This method sends an asynchronous delete request to the Manticore Search API to remove a document 
        /// identified by the provided parameters.
        /// </summary>
        /// <param name="document">An instance of <see cref="DeleteRequest"/> that contains the parameters necessary
        /// to identify the document to be deleted, such as the document ID or any specific criteria required 
        /// for deletion.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the 
        /// asynchronous operation if needed.</param>
        /// <returns>
        /// An instance of <see cref="DeleteResponse"/> that indicates the result of the delete operation, including
        /// the status of the operation and whether the document was found and successfully deleted.
        /// </returns>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public async Task<object> DeleteAsync(DeleteRequest document, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<object>("/delete", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes multiple documents from the Manticore Search index in a single bulk operation.
        /// This method constructs a bulk delete request to the Manticore Search API based on the provided 
        /// collection of documents to be deleted.
        /// </summary>
        /// <param name="documents">An enumerable collection of <see cref="BulkDeleteRequest"/> objects, each
        /// containing the necessary parameters to identify the documents to be deleted, such as document IDs.
        /// The collection must not be null or empty to execute a successful delete operation.</param>
        /// <returns>
        /// An instance of <see cref="BulkResponse"/> that represents the result of the bulk delete operation, 
        /// including the status and outcome for each delete request in the bulk operation.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection 
        /// of delete requests is provided for processing.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public object BulkDelete(IEnumerable<BulkDeleteRequest> documents)
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
        /// Asynchronously deletes multiple documents from the Manticore Search index in a single bulk operation.
        /// This method constructs a bulk delete request to the Manticore Search API based on the provided 
        /// collection of documents to be deleted.
        /// </summary>
        /// <param name="documents">An enumerable collection of <see cref="BulkDeleteRequest"/> objects, each
        /// containing the necessary parameters to identify the documents to be deleted, such as document IDs.
        /// The collection must not be null or empty to execute a successful delete operation.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation
        /// asynchronously. This token allows for cooperative cancellation between threads.</param>
        /// <returns>
        /// An instance of <see cref="BulkResponse"/> that represents the result of the bulk delete operation, 
        /// including the status and outcome for each delete request in the bulk operation.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when the <paramref name="documents"/> parameter is null. This ensures that a valid collection 
        /// of delete requests is provided for processing.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to network issues,
        /// malformed requests, or error responses from the API.
        /// </exception>
        public async Task<object> BulkDeleteAsync(IEnumerable<BulkDeleteRequest> documents, CancellationToken cancellationToken = default)
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
        /// Indexes a percolate query document into a specified index in Manticore Search.
        /// This method allows for the storage of percolation queries that can be matched against 
        /// future documents, facilitating the retrieval of relevant queries based on the content 
        /// of those documents.
        /// </summary>
        /// <param name="document">An instance of <see cref="IndexPercolateRequest"/> containing the percolate 
        /// query to be indexed. This object must not be null, and it must contain the necessary properties
        /// required for defining the percolation query.</param>
        /// <param name="index">The name of the index into which the percolate query will be stored.
        /// This parameter must not be null and should correspond to an existing index in the Manticore Search
        /// server.</param>
        /// <returns>
        /// An instance of <see cref="SearchResponse"/> that represents the result of the indexing operation.
        /// This may include information about the success of the operation, the indexed document's ID, 
        /// and other metadata returned by the Manticore Search server.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when either the <paramref name="document"/> or <paramref name="index"/> parameters are null.
        /// This ensures that valid inputs are provided for the indexing operation, preventing execution errors.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to various reasons,
        /// including network issues, incorrect indexing format, or server-side errors.
        /// </exception>
        public SearchResponse IndexPercolate(IndexPercolateRequest document, string index)
        {
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
        /// Asynchronously indexes a percolate query document into a specified index in Manticore Search.
        /// This asynchronous method enables the storage of percolation queries that can be matched against 
        /// future documents, facilitating the retrieval of relevant queries based on the content 
        /// of those documents while allowing for non-blocking operations.
        /// </summary>
        /// <param name="document">An instance of <see cref="IndexPercolateRequest"/> containing the percolate 
        /// query to be indexed. This object must not be null and should include the necessary properties
        /// required for defining the percolation query.</param>
        /// <param name="index">The name of the index into which the percolate query will be stored.
        /// This parameter must not be null and should correspond to an existing index in the Manticore Search
        /// server.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> that can be used to 
        /// cancel the asynchronous operation. This provides a mechanism for managing long-running requests
        /// and gracefully handling cancellations.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an instance of 
        /// <see cref="SearchResponse"/> that represents the result of the indexing operation. This may include
        /// information about the success of the operation, the indexed document's ID, 
        /// and other metadata returned by the Manticore Search server.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when either the <paramref name="document"/> or <paramref name="index"/> parameters are null.
        /// This ensures that valid inputs are provided for the indexing operation, preventing execution errors.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to various reasons,
        /// including network issues, incorrect indexing format, or server-side errors.
        /// </exception>
        public async Task<SearchResponse> IndexPercolateAsync(IndexPercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
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
        /// Executes a percolation search query against a specified index in Manticore Search.
        /// This method allows for determining which pre-defined queries (percolate queries) 
        /// match a given document, facilitating the retrieval of relevant queries based on 
        /// the document's content.
        /// </summary>
        /// <param name="document">An instance of <see cref="PercolateRequest"/> representing the document 
        /// to be checked against the percolation queries stored in the specified index. This parameter must 
        /// not be null and should include the necessary properties required for defining the document content.</param>
        /// <param name="index">The name of the index to be searched for matching percolate queries. 
        /// This parameter must not be null and should correspond to an existing index in the Manticore Search 
        /// server.</param>
        /// <returns>
        /// An instance of <see cref="SearchResponse"/> containing the results of the percolation search. 
        /// This includes information about which queries matched the document and any associated metadata 
        /// returned by the Manticore Search server.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when either the <paramref name="document"/> or <paramref name="index"/> parameters are null.
        /// This ensures that valid inputs are provided for the percolation operation, preventing execution errors.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to various reasons,
        /// including network issues, incorrect request format, or server-side errors.
        /// </exception>
        public SearchResponse Percolate(PercolateRequest document, string index)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return Send<SearchResponse>($"/pq/{index}/search", HttpMethod.Post, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously executes a percolation search query against a specified index in Manticore Search.
        /// This method allows for determining which pre-defined queries (percolate queries) 
        /// match a given document, facilitating the retrieval of relevant queries based on 
        /// the document's content.
        /// </summary>
        /// <param name="document">An instance of <see cref="PercolateRequest"/> representing the document 
        /// to be checked against the percolate queries stored in the specified index. This parameter must 
        /// not be null and should include the necessary properties required for defining the document content.</param>
        /// <param name="index">The name of the index to be searched for matching percolate queries. 
        /// This parameter must not be null and should correspond to an existing index in the Manticore Search 
        /// server.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the 
        /// asynchronous operation. This allows the caller to signal that the operation should be aborted 
        /// if needed.</param>
        /// <returns>
        /// An instance of <see cref="SearchResponse"/> containing the results of the percolation search. 
        /// This includes information about which queries matched the document and any associated metadata 
        /// returned by the Manticore Search server.
        /// </returns>
        /// <exception cref="NullException">
        /// Thrown when either the <paramref name="document"/> or <paramref name="index"/> parameters are null.
        /// This ensures that valid inputs are provided for the percolation operation, preventing execution errors.
        /// </exception>
        /// <exception cref="HttpRequestFailureException">
        /// Thrown when the HTTP request to the Manticore Search API fails. This can occur due to various reasons,
        /// including network issues, incorrect request format, or server-side errors.
        /// </exception>
        public async Task<SearchResponse> PercolateAsync(PercolateRequest document, string index, CancellationToken cancellationToken = default)
        {
            try
            {
                var content = CreateStringContentFromJson(document, "application/json");

                return await SendAsync<SearchResponse>($"/pq/{index}/search", HttpMethod.Post, content, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
