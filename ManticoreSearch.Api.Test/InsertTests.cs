using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class InsertTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void InsertDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertRequestWithNullIndexTest()
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
        public void InsertRequestWithEmptyIndexTest()
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
        public void InsertRequestWithWrongAttributesTest()
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
        public void InsertNullRequestTest()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(null));
        }

        [TestMethod]
        public void InsertRequestWithNullDocumentTest()
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
        public void InsertRequestWithEmptyDocumentTest()
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
