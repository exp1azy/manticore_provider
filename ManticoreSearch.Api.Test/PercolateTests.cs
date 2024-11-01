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
            var percolate = new IndexPercolateRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "Трамп" }
                    }
                }
            };

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateRequestTest_NullFilter()
        {
            var percolate = new IndexPercolateRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "Трамп" }
                    }
                },
                Filters = null
            };

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 2);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
