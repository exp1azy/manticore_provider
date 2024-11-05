using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class BulkTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void BulkTest()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 20; i++)
            {
                docs.Add(new()
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

            var result = apiInstance.Bulk(docs);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BulkTest_NullIndex()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new ModificationRequest
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkTest_EmptyIndex()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new ModificationRequest
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkTest_WrongAttributes()
        {
            var random = new Random();
            var docs = new List<BulkInsertRequest>();

            for (int i = 0; i < 1000; i++)
            {
                docs.Add(new()
                {
                    Insert = new ModificationRequest
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkTest_Empty()
        {
            var docs = new List<BulkInsertRequest>();
            var result = apiInstance.Bulk(docs);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkTest_Null()
        {
            Assert.ThrowsException<BulkException>(() => apiInstance.Bulk(null));
        }

        [TestMethod]
        public void BulkReplaceTest()
        {
            var docs = new List<BulkReplaceRequest>()
            {
                new()
                {
                    Replace = new()
                    {
                        Index = "products",
                        Id = 9,
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
                        Id = 100,
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
            
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BulkReplaceTest_Null()
        {
            Assert.ThrowsException<BulkException>(() => apiInstance.BulkReplace(null));
        }

        [TestMethod]
        public void BulkReplaceTest_Empty()
        {
            var docs = new List<BulkReplaceRequest>();

            var result = apiInstance.BulkReplace(docs);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkReplaceTest_WrongAttributes()
        {
            var random = new Random();
            var docs = new List<BulkReplaceRequest>();

            for (int i = 0; i < 20; i++)
            {
                docs.Add(new()
                {
                    Replace = new ModificationRequest
                    {
                        Index = "products",
                        Id = i + 1,
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkDeleteTest()
        {
            var docs = new List<BulkDeleteRequest>
            {
                new()
                {
                    Delete = new DeleteRequest
                    {
                        Index = "products",
                        Query = new Query
                        {
                            Range = new Dictionary<string, Models.Requests.RangeFilter>
                            {
                                { "id", new Models.Requests.RangeFilter { Lt = 5 } }
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
                            Range = new Dictionary<string, Models.Requests.RangeFilter>
                            {
                                { "price", new Models.Requests.RangeFilter { Gt = 10 } }
                            }
                        }
                    }
                }
            };

            var result = apiInstance.BulkDelete(docs);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BulkDeleteTest_Empty()
        {
            var docs = new List<BulkDeleteRequest>();
            var result = apiInstance.BulkDelete(docs);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BulkDeleteTest_WrongAttributes()
        {
            var docs = new List<BulkDeleteRequest>
            {
                new()
                {
                    Delete = new DeleteRequest
                    {
                        Index = "products",
                        Query = new Query
                        {
                            Equals = new Dictionary<string, object>
                            {
                                { "123", "burger" }
                            }
                        }
                    }
                }
            };

            var result = apiInstance.BulkDelete(docs);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BulkUpdateTest()
        {
            var docs = new List<BulkUpdateRequest>
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
                            Range = new Dictionary<string, Models.Requests.RangeFilter>
                            {
                                { "price", new Models.Requests.RangeFilter { Lt = 10 } }
                            }
                        },
                        Document = new Dictionary<string, object>
                        {
                            { "price", 10 }
                        }
                    }
                }
            };

            var result = apiInstance.BulkUpdate(docs);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
