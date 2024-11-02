using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class SearchTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void SearchRequest()
        {
            var request = new SearchRequest
            {
                Index = "articles",
                Limit = 1000,
                Query = new Query
                {
                    Bool = new Bool
                    {
                        Must = new List<Must>
                        {
                            new Must
                            {
                                Match = new Dictionary<string, object>
                                {
                                    { "body", "Путин" }
                                }
                            }
                        },
                        MustNot = new List<Must>
                        {
                            new Must 
                            {
                                Match = new Dictionary<string, object>
                                {
                                    { "body", "Трамп" }
                                }
                            }
                        }
                    }
                }
            };

            var result = apiInstance.Search(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Response!.Hits.Total > 0);
        }

        [TestMethod]
        public void SearchRequest_Null()
        {
            var result = apiInstance.Search(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void SearchRequest_Empty()
        {
            var request = new SearchRequest();
            var result = apiInstance.Search(request);

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
