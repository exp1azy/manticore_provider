using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class ReplaceTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void ReplaceRequestTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 8217460083752177578,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 26.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReplaceRequestWithWrongIndexTest()
        {
            var doc = new InsertRequest
            {
                Index = "",
                Id = 8217460083752177578,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 26.0f },
                    { "count", 2 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceNullRequestTest()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Replace(null));
        }

        [TestMethod]
        public void ReplaceRequestWithWrongIdTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 10,
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReplaceRequestWithNegativeIdTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = -1,
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceRequestWithNullDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = -1,
                Document = null
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceRequestWithEmptyDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = -1,
                Document = []
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Replace(doc));
        }
    }
}
