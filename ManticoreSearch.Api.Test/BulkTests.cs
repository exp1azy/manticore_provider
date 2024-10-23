using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class BulkTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void BulkRequestTest()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 15; i++)
            {
                docs.Add(new()
                {
                    Insert = new InsertRequest
                    {
                        Index = "products",
                        Document = new Dictionary<string, object>
                        {
                            { "title", "some food" },
                            { "price", random.Next(1, 20) },
                            { "count", random.Next(1, 5) }
                        }
                    }
                });
            }

            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkRequestTest_WithNullIndex()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new InsertRequest
                    {
                        Index = null,
                        Document = new Dictionary<string, object>
                        {
                            { "title", "some food" },
                            { "price", random.Next(1, 20) },
                            { "count", random.Next(1, 5) }
                        }
                    }
                });
            }

            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkRequestTest_WithEmptyIndex()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new InsertRequest
                    {
                        Index = "",
                        Document = new Dictionary<string, object>
                        {
                            { "title", "some food" },
                            { "price", random.Next(1, 20) },
                            { "count", random.Next(1, 5) }
                        }
                    }
                });
            }

            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkRequestTest_WithWrongAttributes()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new InsertRequest
                    {
                        Index = "products",
                        Document = new Dictionary<string, object>
                        {
                            { "", "some food" },
                            { "qwerty", random.Next(1, 20) },
                            { "123", random.Next(1, 5) }
                        }
                    }
                });
            }

            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkRequestTest_WithEmptySet()
        {
            var docs = new List<BulkInsertRequest>();
            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkRequestTest_Null()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Bulk(null));
        }

        [TestMethod]
        public void BulkReplaceRequestTest()
        {
            var docs = new List<BulkReplaceRequest>()
            {
                new()
                {
                    Replace = new()
                    {
                        Index = "products",
                        Id = 8217456661804089403,
                        Document = new Dictionary<string, object>()
                        {
                            { "title", "pineapple" },
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
                        Document = new Dictionary<string, object>()
                        {
                            { "title", "mango" },
                            { "price", 14 },
                            { "count", 3 }
                        }
                    }
                },
            };

            var result = apiInstance.BulkReplace(docs); 
            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkReplaceRequestTest_Null()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.BulkReplace(null));
        }

        [TestMethod]
        public void BulkReplaceRequestTest_Empty()
        {
            var docs = new List<BulkReplaceRequest>();

            var result = apiInstance.BulkReplace(docs);
        }

        [TestMethod]
        public void BulkReplaceRequestTest_WithWrongAttributes()
        {
            var random = new Random();
            var docs = new List<BulkReplaceRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Replace = new InsertRequest
                    {
                        Index = "products",
                        Document = new Dictionary<string, object>
                        {
                            { "", "some food" },
                            { "qwerty", random.Next(1, 20) },
                            { "123", random.Next(1, 5) }
                        }
                    }
                });
            }

            var result = apiInstance.BulkReplace(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkDeleteRequestTest()
        {
            var docs = new List<BulkDeleteRequest>
            {
                new() {
                    Delete = new DeleteRequest
                    {
                        Index = "products",
                        Query = new
                        {
                            equals = new
                            {
                                title = "pepsi"
                            }
                        }
                    }
                }
            };

            var result = apiInstance.BulkDelete(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkDeleteRequestTest_Empty()
        {
            var docs = new List<BulkDeleteRequest>();
            var result = apiInstance.BulkDelete(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BulkDeleteRequestTest_WithEmptyIndex()
        {
            var docs = new List<BulkDeleteRequest>
            {
                new() {
                    Delete = new DeleteRequest
                    {
                        Index = "",
                        Query = new
                        {
                            equals = new
                            {
                                title = "pepsi"
                            }
                        }
                    }
                }
            };

            var result = apiInstance.BulkDelete(docs);

            Assert.IsNotNull(result);
        }
    }
}
