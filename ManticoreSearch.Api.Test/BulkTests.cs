using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;
using System;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class BulkTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void BulkRequestTest()
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
        public void BulkRequestWithNullIndexTest()
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
        public void BulkRequestWithEmptyIndexTest()
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
        public void BulkRequestWithWrongAttributesTest()
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
        public void BulkRequestWithEmptySetTest()
        {
            var docs = new List<BulkInsertRequest>();
            var result = apiInstance.Bulk(docs);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmptyBulkRequestTest()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Bulk(null));
        }
    }
}
