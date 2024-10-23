using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class InsertTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void InsertDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 9,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertRequestTest_WithNullIndex()
        {
            var doc = new InsertRequest
            {
                Index = null,
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertRequestTest_WithEmptyIndex()
        {
            var doc = new InsertRequest
            {
                Index = "",
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertRequestTest_WithWrongAttributes()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "", "fanta" },
                    { "qwerty", 25.0f },
                    { "123", 1 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertRequestTest_Null()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(null));
        }

        [TestMethod]
        public void InsertRequestTest_WithNullDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = null
            };

            var result = apiInstance.Insert(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertRequestTest_WithEmptyDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = []
            };

            var result = apiInstance.Insert(doc);

            Assert.IsNotNull(result);
        }
    }
}
