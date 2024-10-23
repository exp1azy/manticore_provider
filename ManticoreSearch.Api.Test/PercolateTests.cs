using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class PercolateTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void IndexPercolateRequestTest()
        {
            var request = new IndexPercolateRequest()
            {
                Query = new
                {
                    match = new
                    {
                        title = "Путин"
                    }
                },
                Filters = ""
            };

            var result = apiInstance.IndexPercolate(request, "articles");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexPercolateRequestTest_Empty()
        {
            var request = new IndexPercolateRequest();

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.IndexPercolate(request, "articles"));
        }

        [TestMethod]
        public void IndexPercolateRequestTest_Null()
        {
            var request = apiInstance.IndexPercolate(null, "articles");

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void IndexPercolateRequestTest_EmptyIndex()
        {
            var request = new IndexPercolateRequest()
            {
                Query = new
                {
                    match = new
                    {
                        title = "Путин"
                    }
                },
                Filters = ""
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.IndexPercolate(request, ""));
        }

        [TestMethod]
        public void PercolateRequestTest()
        {
            var request = new PercolateRequest()
            {
                Query = new PercolateRequestQuery
                {
                    Percolate = new
                    {
                        document = new
                        {
                            title = "Путин"
                        }
                    }
                }
            };

            var result = apiInstance.Percolate(request, "articles");

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void PercolateRequestTest_Empty()
        {
            var request = new PercolateRequest();

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Percolate(request, "articles"));
        }

        [TestMethod]
        public void PercolateRequestTest_Null()
        {
            var result = apiInstance.Percolate(null, "articles");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PercolateRequestTest_EmptyIndex()
        {
            var request = new PercolateRequest()
            {
                Query = new PercolateRequestQuery
                {
                    Percolate = new
                    {
                        document = new
                        {
                            title = "Путин"
                        }
                    }
                }
            };

            var result = apiInstance.Percolate(request, "");

            Assert.IsNotNull(result);
        }
    }
}
