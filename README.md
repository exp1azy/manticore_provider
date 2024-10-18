# ManticoreProvider

[Manticore Search](https://manticoresearch.com/) – easy-to-use open-source fast database for search 

`ManticoreProvider` is a library for interacting with Manticore Search, providing convenient methods for executing SQL queries, inserting, updating, and deleting documents, as well as working with percolators.

**Note:** This library is currently in development and testing. It will be available on NuGet in the future. If you wish, you can build the library using `dotnet build`, extract the DLL from the `bin` folder, and add a reference to this DLL in your project.

## Usage
### Creating an Instance

Create an instance of `ManticoreProvider`, specifying the base address of Manticore Search. If the address is not specified, http://localhost:9308 will be used by default.
```csharp
var provider = new ManticoreProvider(); // defaults to http://localhost:9308
// or
var provider = new ManticoreProvider("http://your-manticore-address");
```

### Executing SQL Queries

To execute an SQL query, use the `Sql` method or its asynchronous variant `SqlAsync`:
```csharp
string result = provider.Sql("SELECT * FROM your_index");
// or
string resultAsync = await provider.SqlAsync("SELECT * FROM your_index");
```

### Inserting Documents

To insert documents into the index, use the `Insert` method or `InsertAsync`:
```csharp
var request = new InsertRequest { /* fill in the data */ };
var insertResponse = provider.Insert(request);
// or
var insertResponseAsync = await provider.InsertAsync(request);
```

### Bulking Documents

To perform bulk upload of documents use `Bulk` method or `BulkAsync`:
```csharp
var request = new List<BulkInsertRequest>(); // fill in the data
var result = provider.Bulk(request);
// or
var resultAsync = await provider.BulkAsync(request);
```

### Replacement of Documents

To replace documents, use `Replace` method or `ReplaceAsync`:
```csharp
var request = new InsertRequest(/* fill in the data */);
var result = provider.Replace(request);
// or
var resultAsync = await provider.ReplaceAsync(request);
```

### Bulk Replacement of Documents

To perform a bulk document replacement, use `BulkReplace` method or `BulkReplaceAsync`:
```csharp
var request = new List<BulkReplaceRequest>(); // fill in the data
var result = provider.BulkReplace(request);
// or
var resultAsync = await provider.BulkReplaceAsync(request);
```

### Updating Documents

To update documents, use `Update` method or `UpdateAsync`:
```csharp
var request = new UpdateRequest(/* fill in the data */);
var result = provider.Update(request);
// or
var resultAsync = await provider.UpdateAsync(request);
```

### Search by Documents

To search documents, use `Search` method or `SearchAsync`:
```csharp
var request = new SearchRequest(/* fill in the data */);
var result = provider.Search(request);
// or
var resultAsync = await provider.SearchAsync(request);
```

### Deleting Documents

To delete documents, use `Delete` method or `DeleteAsync`:
```csharp
var request = new DeleteRequest(/*fill in the data*/);
var result = provider.Delete(request);
// or
var resultAsync = await provider.DeleteAsync(request);
```

### Bulk Deletion of Documents

To perform bulk deletion of documents use `BulkDelete` method or `BulkDeleteAsync`:
```csharp
var request = new List<BulkDeleteRequest>(); // fill in the data
var result = provider.BulkDelete(request);
// or
var resultAsync = await provider.BulkDeleteAsync(request);
```

### Working with Percolators

To index and search using percolators, use the `IndexPercolate` and `Percolate` methods:
```csharp
var percolateRequest = new PercolateRequest (/* fill in the data */);
var percolateResponse = provider.Percolate(percolateRequest, "your_index");
// or
var percolateResponseAsync = await provider.PercolateAsync(percolateRequest, "your_index");
```

## Exceptions

- `HttpRequestFailureException` — on HTTP request failure.
- `InsertException` — on document insertion error.
- `NullException` — when `null` is passed as an argument.

## Cleanup

Remember to release resources by calling the `Dispose` method when you are done using `ManticoreProvider`.
```csharp
provider.Dispose();
```

## Building the Library

To build the library and obtain the DLL, use the following command:
```bash
cd your_directory
dotnet build
```
After building, you can find the DLL in the bin folder of your project and add a reference to it in your own project.
