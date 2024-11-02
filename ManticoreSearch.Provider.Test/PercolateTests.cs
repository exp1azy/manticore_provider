using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class PercolateTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void IndexPercolateTest()
        {
            var percolate = new IndexPercolateRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "Ким Чен Ын" }
                    }
                }
            };

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 5);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateTest_Empty()
        {
            var percolate = new IndexPercolateRequest();

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 6);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateTest_WrongAttributes()
        {
            var percolate = new IndexPercolateRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "1234", "1234" }
                    }
                }
            };

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 6);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void GetPeroclateTest()
        {
            var result = apiInstance.GetPercolate("pqtable", 1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GetPercolateTest_NegativeId()
        {
            var result = apiInstance.GetPercolate("pqtable", -1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GetPercolateTest_EmptyIndex()
        {
            var result = apiInstance.GetPercolate("", 1);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdatePercolateTest()
        {
            var request = new IndexPercolateRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "Кимерсен" }
                    }
                }
            };

            var result = apiInstance.UpdatePercolate(request, "pqtable", 1);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
