# ManticoreProvider

[Manticore Search](https://manticoresearch.com/) – easy-to-use open-source fast database for search.

The `ManticoreSearch.Provider` offers an API to interact with **Manticore Search** server using `.NET`. This library includes features for full-text search, autocomplete, fuzzy search, basic CRUD operations, support for custom queries and filters, and flexible indexing and query expansion options. Designed to simplify the integration of **Manticore Search** into `.NET` projects.

**Note:** If you find errors or bugs in the library, please write in the `issues` section.

## Usage
### Creating an Instance

Create an instance of `ManticoreProvider` class, specifying the base address of Manticore Search server. If the address is not specified, `http://localhost:9308` will be used by default.
```csharp
var provider = new ManticoreProvider(); // defaults to http://localhost:9308
// or
var provider = new ManticoreProvider("http://your-address");
```

### Executing SQL Queries

To execute an SQL query, use the `SqlAsync` method:
```csharp
string resultAsync = await provider.SqlAsync("SELECT * FROM your_index");
```

### Declaring Entities

Manticore contains structured tables, so you need to declare a class that describes the table. The class must inherit from `ManticoreDocument`:
```csharp
public class TestIndex : ManticoreDocument
{
    public string Title { get; set; }

    public string Content { get; set; }

    public float? Price { get; set; }

    public string Category { get; set; }
}
```

### Inserting Documents

To insert documents into the index, use the `InsertAsync` method:
```csharp
var request = new ModificationRequest<TestIndex>
{
    Index = "test_index",
    Id = 10,
    Document = new TestIndex
    {
        Title = "Test Document",
        Content = "This is a test content" ,
        Price = 19.99f,
    }
}; // where TestIndex : ManticoreDocument

var result = await provider.InsertAsync(request);
```

### Bulking Documents

To perform bulk upload of documents use the `BulkAsync` method:
```csharp
var bulkRequests = new List<BulkInsertRequest<TestIndex>>
{
    new (new ModificationRequest<TestIndex>
    {
        Index = "test_index",
        Id = 1001,
        Document = new TestIndex
        {
            Title = "Bulk Test Document 2",
            Content = "Content for bulk test 2",
            Price = 20.99f
        }
    }),
    new (new ModificationRequest<TestIndex>
    {
        Index = "test_index",
        Id = 1002,
        Document = new TestIndex
        {
            Title = "Bulk Test Document 3",
            Content = "Content for bulk test 3",
            Price = 30.99f
        }
    })
};

var result = await provider.BulkAsync(bulkRequests);
```

### Replacement of Documents

To replace documents, use the `ReplaceAsync` method:
```csharp
var replaceRequest = new ModificationRequest<TestIndex>
{
    Index = "test_index",
    Id = 3000,
    Document = new TestIndex
    {
        Title = "Updated Title",
        Content = "Updated Content",
        Price = 19.99f
    }
};

var result = await provider.ReplaceAsync(replaceRequest);
```

### Bulk Replacement of Documents

To perform a bulk document replacement, use the `BulkReplaceAsync` method:
```csharp
var bulkRequests = new List<BulkReplaceRequest<TestIndex>>
{
    new (new ModificationRequest<TestIndex>
    {
        Index = "test_index",
        Id = 1,
        Document = new TestIndex
        {
            Title = "Updated Document 1",
            Content = "Updated content for document 1",
            Price = 25.99f,
            Category = "updated"
        }
    }),
    new (new ModificationRequest<TestIndex>
    {
        Index = "test_index",
        Id = 2,
        Document = new TestIndex
        {
            Title = "Updated Document 2",
            Content = "Updated content for document 2",
            Price = 35.99f,
            Category = "updated"
        }
    })
};

var result = await provider.BulkReplaceAsync(bulkRequests);
```

### Updating Documents

To update documents, use the `UpdateAsync` method:
```csharp
var doc = new UpdateRequest<Products>
{
    Index = "products",
    Document = new Products
    {
        Title = "book"
    },
    Query = new Query
    {
        Equals = new Dictionary<string, object>
        {
            { "price", 25 }
        }
    }
};

var result = provider.Update(doc);
```

### Bulk Updating Documents

To update documents, use the `BulkUpdateAsync` method:
```csharp
var insertRequest = new ModificationRequest<TestIndex>
{
    Index = "test_index",
    Id = 13,
    Document = new TestIndex
    {
        Title = "Title",
        Content = "Content"
    }
};

await provider.InsertAsync(insertRequest);

var bulkRequests = new List<BulkUpdateRequest<TestIndex>>
{
    {
        new BulkUpdateRequest<TestIndex>
        {
            Update = new UpdateRequest<TestIndex>
            {
                Index = "test_index",
                Id = 13,
                Document = new TestIndex
                {
                    Title = "First Update",
                    Content = "First content"
                }
            }
        }
    }
};

var result = await provider.BulkUpdateAsync(bulkRequests);
```

### Search by Documents

To search documents and get full information about the search results, use the `SearchAsync` method:
```csharp
var searchRequest = new SearchRequest(
    index: "test_index",
    query: new Query
    {
        Match = new Dictionary<string, object>
        {
            { "content", new { query = "comprehensive test" } }
        }
    },
    source: new { includes = new List<string> { "id", "title", "content" } },
    profile: true,
    limit: 20,
    offset: 5,
    size: 15,
    from: 0,
    maxMatches: 100,
    sort: new List<object> { new { content = "desc" } },
    options: new OptionDetails
    {
        MaxMatches = 1000,
        Ranker = RankerOption.Bm25,
        Threads = 4
    },
    highlight: new HighlightOptions
    {
        BeforeMatch = "<b>",
        AfterMatch = "</b>",
        Limit = 3
    },
    trackScores: true
);

var result = await provider.SearchAsync(searchRequest);
```

To search documents and get information about hits based on specified documents, use the `SearchAsync<TDocument>` method:
```csharp
var searchRequest = new SearchRequest(
    index: "test_index",
    query: new Query
    {
        Range = new Dictionary<string, RangeFilter>
        {
            { "price", new RangeFilter() { Lte = 10 } }
        }
    }
);

var result = await apiInstance.SearchAsync<TestIndex>(searchRequest);
```

To search documents and get raw JSON data, use the `SearchRawAsync` method:
```csharp
var searchRequest = new SearchRequest(
    index: "test_index",
    query: new Query
    {
        Range = new Dictionary<string, RangeFilter>
        {
            { "price", new RangeFilter() { Lte = 10 } }
        }
    }
);

var result = await apiInstance.SearchRawAsync(searchRequest);
```

### Deleting Documents

To delete documents, use the `DeleteAsync` method:
```csharp
var request = new DeleteRequest
{
    Index = "products",
    Query = new Query
    {
        Equals = new Dictionary<string, object>
        {
            { "title", "book" }
        }
    }
};

var result = await provider.DeleteAsync(doc);
```

### Bulk Deletion of Documents

To perform bulk deletion of documents use the `BulkDeleteAsync` method:
```csharp
var request = new List<BulkDeleteRequest>
{
    {
        new BulkDeleteRequest
        {
            Delete = new DeleteRequest
            {
                Index = "test_index",
                Id = 1
            }
        }
    },
    {
        new BulkDeleteRequest
        {
            Delete = new DeleteRequest
            {
                Index = "test_index",
                Id = 2
            }
        }
    }
};

var result = await provider.BulkDeleteAsync(request);
```

### Working with Percolators

To index and search using percolators, use the `IndexPercolateAsync`, `PercolateAsync`, `GetPercolateAsync` and `UpdatePercolateAsync` methods:
```csharp
var percolate = new PercolateRequest<Products>
{
    Query = new PercolateRequestQuery<Products>
    {
        Percolate = new PercolateDocument<Products>
        {
            Documents =
            [
                new Products
                {
                    Title = "chocolate"
                },
                new Products
                {
                    Title = "banana"
                }
            ]
        }
    }
};

var result = await provider.PercolateAsync(percolate, "mypq");
```

### Autocomplete request

Retrieves autocomplete suggestions based on the provided autocomplete query. Use the `AutocompleteAsync` methods:
```csharp
var request = new AutocompleteRequest
{
    Index = "articles",
    Query = "Tr"
};

var result = await provider.AutocompleteAsync(request);
```

### Mapping request

 Defines a new table structure in Manticore search engine using the specified mapping properties, mimicking Elasticsearch-like table definitions. Use the `UseMappingAsync` methods:
```csharp
var request = new MappingRequest
{
    Properties = new Dictionary<string, MappingField>
    {
        { "exercise", new MappingField { Type = MappingFieldType.Keyword } },
        { "working_weight", new MappingField { Type = MappingFieldType.Integer } },
        { "weight_limit", new MappingField { Type = MappingFieldType.Integer } }
    }
};

var result = await provider.UseMappingAsync(request, "training");
```

## Usage In ASP.NET
To use `ManticoreProvider` in your ASP.NET project, register the provider in the DI container:
```csharp
builder.Services.AddManticoreSearchProvider();
```
Next, you can get the `IManticoreProvider` from the DI container. Example:
```csharp
[ApiController]
[Route("[controller]")]
public class ManticoreController(IManticoreProvider manticoreProvider) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] TestIndex testIndex)
    {
        await manticoreProvider.InsertAsync(new ModificationRequest<TestIndex>
        {
            Table = "test_index",
            Document = testIndex
        });

        return Ok();
    }
}
```

## Exceptions

- `AutocompleteException` — on Autocomplete request error.
- `BulkException` — on Bulk request error.
- `DeleteException` — on Delete request error.
- `MappingException` — on Mapping request error.
- `ModificationException` — on Modification request error.
- `PercolateException` — on Percolate request error.
- `SearchException` — on Search request error.
- `SqlException` — on SQL request error.
- `UpdateException` — on Update request error.