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
                Id = 8217476891905359885,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 26.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsNotNull(result);
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

            Assert.IsTrue(result.ToString()!.Contains("error"));
        }

        [TestMethod]
        public void ReplaceRequestTest_Null()
        {
            var result = apiInstance.Replace(null);

            Assert.IsTrue(result.ToString()!.Contains("error"));
        }

        [TestMethod]
        public void ReplaceRequestTest_WrongId()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Id = 105,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsNotNull(result);
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

            Assert.IsTrue(result.ToString()!.Contains("error"));
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

            Assert.ThrowsException<NullException>(() => apiInstance.Replace(doc));
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
