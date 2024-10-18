using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class DeleteTests
    {
        private readonly ManticoreProvider apiIstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void DeleteRequestTest()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = 1
            };

            var result = apiIstance.Delete(doc);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Found);
        }

        [TestMethod]
        public void DeleteRequestWithWrongIdTest()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = 100
            };

            var result = apiIstance.Delete(doc);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Found);
        }

        [TestMethod]
        public void DeleteRequestWithNegativeIdTest()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = -1
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiIstance.Delete(doc));
        }

        [TestMethod]
        public void DeleteRequestWithWrongIndexTest()
        {
            var doc = new DeleteRequest
            {
                Index = "",
                Id = 1
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiIstance.Delete(doc));
        }

        [TestMethod]
        public void DeleteRequestWithNullIndexTest()
        {
            var doc = new DeleteRequest
            {
                Index = null,
                Id = 1
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiIstance.Delete(doc));
        }

        [TestMethod]
        public void DeleteRequestWithEmptyBodyTest()
        {
            var doc = new DeleteRequest();

            Assert.ThrowsException<HttpRequestFailureException>(() => apiIstance.Delete(doc));
        }

        [TestMethod]
        public void DeleteNullRequestTest()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiIstance.Delete(null));
        }

        [TestMethod]
        public void DeleteRequestWithQueryTest()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new
                {
                    equals = new Dictionary<string, object>
                    {
                        ["title"] = "pepsi"
                    }
                }
            };

            var result = apiIstance.Delete(doc);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Found);
        }
    }
}
