using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class DeleteTests
    {
        private readonly ManticoreProvider apiIstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void DeleteTest()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = 2
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_WrongId()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = 12345
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_NegativeId()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = -1
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_EmptyIndex()
        {
            var doc = new DeleteRequest
            {
                Index = "",
                Id = 3
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_NullIndex()
        {
            var doc = new DeleteRequest
            {
                Index = null,
                Id = 1
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_Empty()
        {
            var doc = new DeleteRequest();
            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_Null()
        {
            var result = apiIstance.Delete(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteTest_Query()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new Query
                {
                    Equals = new Dictionary<string, object>
                    {
                        { "title", "fanta" }
                    }
                }
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.ResponseIfQuery);
        }

        [TestMethod]
        public void DeleteTest_EmptyQuery()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new()
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.ResponseIfQuery);
        }
    }
}
