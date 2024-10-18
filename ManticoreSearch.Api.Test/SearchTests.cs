using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class SearchTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void SearchRequest()
        {
            var request = new SearchRequest()
            {
                Index = "articles",
                Limit = 1000,
                Query = new
                {
                    @bool = new
                    {
                        must = new[]
                        {
                            new
                            {
                                match = new
                                {
                                    body = "Путин"
                                }
                            }
                        },
                        must_not = new[]
                        {
                            new
                            {
                                match = new
                                {
                                    body = "Трамп"
                                }
                            }
                        }
                    }
                }
            };

            var result = apiInstance.Search(request);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchNullRequest()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Search(null));
        }

        [TestMethod]
        public void SearchEmptyRequest()
        {
            var request = new SearchRequest();

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Search(request));
        }
    }
}
