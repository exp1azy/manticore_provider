# ManticoreProvider

[Manticore Search](https://manticoresearch.com/) – easy-to-use open-source fast database for search 

Initial release of `ManticoreSearch.Provider` providing API integration with `ManticoreSearch` for .NET applications. This version includes features for full-text search, autocomplete, fuzzy search, basic CRUD operations, support for custom queries and filters, and flexible indexing and query expansion options. Designed to simplify `ManticoreSearch` integration in .NET projects.

**Note:** If you find errors or bugs in the library, please write in the `issues` section

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
var request = new ModificationRequest
    {
        Index = "products",
        Document = new Dictionary<string, object>
        {
            { "title", "jopa" },
            { "price", 19.0f },
            { "count", 3 }
        }
    }
};

var insertResponse = provider.Insert(request);
// or
var insertResponseAsync = await provider.InsertAsync(request);
```

### Bulking Documents

To perform bulk upload of documents use `Bulk` method or `BulkAsync`:
```csharp
for (int i = 0; i < 20; i++)
{
    request.Add(new()
    {
        Insert = new ModificationRequest
        {
            Index = "products",
            Id = i + 1,
            Document = new Dictionary<string, object>
            {
                { "title", "burger" },
                { "price", random.Next(1, 20) },
                { "count", random.Next(1, 5) }
            }
        }
    });
}

var result = provider.Bulk(request);
// or
var resultAsync = await provider.BulkAsync(request);
```

### Replacement of Documents

To replace documents, use `Replace` method or `ReplaceAsync`:
```csharp
var request = new ModificationRequest
{
    Index = "products",
    Id = 1,
    Document = new Dictionary<string, object>
    {
        { "title", "sosal" },
        { "price", 100.0f },
        { "count", 25 }
    }
};

var result = provider.Replace(request);
// or
var resultAsync = await provider.ReplaceAsync(request);
```

### Bulk Replacement of Documents

To perform a bulk document replacement, use `BulkReplace` method or `BulkReplaceAsync`:
```csharp
var request = new List<BulkReplaceRequest>()
{
    new()
    {
        Replace = new()
        {
            Index = "products",
            Id = 9,
            Document = new Dictionary<string, object>()
            {
                { "title", "goida" },
                { "price", 15 },
                { "count", 2 }
            }
        }
    },
    new()
    {
        Replace = new()
        {
            Index = "products",
            Id = 100,
            Document = new Dictionary<string, object>()
            {
                { "title", "svo" },
                { "price", 14 },
                { "count", 3 }
            }
        }
    },
};

var result = provider.BulkReplace(request);
// or
var resultAsync = await provider.BulkReplaceAsync(request);
```

### Updating Documents

To update documents, use `Update` method or `UpdateAsync`:
```csharp
var request = new UpdateRequest()
{
    Index = "products",
    Id = 1,
    Document = new Dictionary<string, object>
    {
        { "title", "tanki online" },
        { "price", 30.0f },
        { "count", 1 }
    }
};
```

### Bulk Updating Documents

To update documents, use `Update` method or `UpdateAsync`:
```csharp
var request = new List<BulkUpdateRequest>
{
    new()
    {
        Update = new()
        {
            Index = "products",
            Query = new Query
            {
                Equals = new Dictionary<string, object>
                {
                    { "title", "burger" }
                }
            },
            Document = new Dictionary<string, object>
            {
                { "title", "kit kat" }
            }
        }
    },
    new()
    {
        Update = new()
        {
            Index = "products",
            Query = new Query
            {
                Range = new Dictionary<string, QueryRange>
                {
                    { "price", new QueryRange { Lt = 10 } }
                }
            },
            Document = new Dictionary<string, object>
            {
                { "price", 10 }
            }
        }
    }
};

var result = provider.BulkUpdate(request);
// or
var resultAsync = await provider.BulkUpdateAsync(request);
```

### Search by Documents

To search documents, use `Search` method or `SearchAsync`:
```csharp
var request = new SearchRequest
{
    Index = "articles",
    Limit = 1000,
    Query = new Query
    {
        Bool = new QueryBool
        {
            Must = new List<BoolMust>
            {
                new BoolMust
                {
                    Match = new Dictionary<string, object>
                    {
                        { "body", "Putin" }
                    }
                }
            },
            MustNot = new List<BoolMust>
            {
                new BoolMust 
                {
                    Match = new Dictionary<string, object>
                    {
                        { "body", "Trump" }
                    }
                }
            }
        }
    }
};

var result = provider.Search(request);
// or
var resultAsync = await provider.SearchAsync(request);
```

### Deleting Documents

To delete documents, use `Delete` method or `DeleteAsync`:
```csharp
var request = new DeleteRequest
{
    Index = "products",
    Id = 2
};

var result = provider.Delete(request);
// or
var resultAsync = await provider.DeleteAsync(request);
```

### Bulk Deletion of Documents

To perform bulk deletion of documents use `BulkDelete` method or `BulkDeleteAsync`:
```csharp
var request = new List<BulkDeleteRequest>
{
    new()
    {
        Delete = new DeleteRequest
        {
            Index = "products",
            Query = new Query
            {
                Range = new Dictionary<string, QueryRange>
                {
                    { "id", new QueryRange { Lt = 5 } }
                }
            }
        }
    },
    new()
    {
        Delete = new DeleteRequest
        {
            Index = "products",
            Query = new Query
            {
                Range = new Dictionary<string, QueryRange>
                {
                    { "price", new QueryRange { Gt = 10 } }
                }
            }
        }
    }
};

var result = provider.BulkDelete(request);
// or
var resultAsync = await provider.BulkDeleteAsync(request);
```

### Working with Percolators

To index and search using percolators, use the `IndexPercolate`, `Percolate`, `GetPercolate` and `UpdatePercolate` methods and their asynchronous versions:
```csharp
var resuest = new IndexPercolateRequest
{
    Query = new Query
    {
        Match = new Dictionary<string, object>
        {
            { "title", "Nasral" }
        }
    }
};

var result = provider.Percolate(resuest, "your_index");
// or
var resultAsync = await provider.PercolateAsync(resuest, "your_index");
```

### Autocomplete request

Retrieves autocomplete suggestions based on the provided autocomplete query. Use the `Autocomplete` or `AutocompleteAsync` methods:
```csharp
var request = new IndexPercolateRequest
{
    Query = new Query
    {
        Match = new Dictionary<string, object>
        {
            { "title", "Pupkin" }
        }
    }
};

var result = apiInstance.IndexPercolate(request, "pqtable", 5);
// or
var resultAsync = await apiInstance.IndexPercolateAsync(request, "pqtable", 5);
```

### Mapping request

 Defines a new table structure in Manticore search engine using the specified mapping properties, mimicking Elasticsearch-like table definitions. Use the `UseMapping` or `UseMappingAsync` methods:
```csharp
var request = new MappingRequest
{
    Properties = new Dictionary<string, MappingField>
    {
        { "exersice", new MappingField { Type = FieldType.Keyword } },
        { "working_weight", new MappingField { Type = FieldType.Integer } },
        { "weight_limit", new MappingField { Type = FieldType.Integer } }
    }
};

var result = apiInstance.UseMapping(request, "training");
// or
var resultAsync = await apiInstance.UseMappingAsync(request, "training");
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

## Cleanup

Remember to release resources by calling the `Dispose` method when you are done using `ManticoreProvider`.
```csharp
provider.Dispose();
```