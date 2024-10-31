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
            object query = new
            {
                index = "articles",
                limit = 1000,
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
            };

            var request = new SearchRequest(query);

            var result = apiInstance.Search(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Response!.Hits.Total > 0);
        }

        [TestMethod]
        public void SearchRequest_Null()
        {
            var result = apiInstance.Search(null);
        }

        [TestMethod]
        public void SearchRequest_Empty()
        {
            var request = new SearchRequest();
            var result = apiInstance.Search(request);
        }
    }
}
