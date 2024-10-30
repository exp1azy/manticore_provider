using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class ReplaceTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void ReplaceRequestTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 100.0f },
                    { "count", 25 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceRequestTest_WrongIndex()
        {
            var doc = new InsertRequest
            {
                Index = "",
                Id = 105,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 26.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceRequestTest_Null()
        {
            Assert.ThrowsException<ReplaceException>(() => apiInstance.Replace(null));
        }

        [TestMethod]
        public void ReplaceRequestTest_WrongId()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 101,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceRequestTest_NegativeId()
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

            var result = apiInstance.Replace(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceRequestTest_NullDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 1,
                Document = null
            };

            Assert.ThrowsException<ReplaceException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceRequestTest_EmptyDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 3,
                Document = []
            };

            Assert.ThrowsException<ReplaceException>(() => apiInstance.Replace(doc));
        }
    }
}
