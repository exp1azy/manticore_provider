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
                Id = 3
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteRequestTest_WrongId()
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
        public void DeleteRequestTest_NegativeId()
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
        public void DeleteRequestTest_EmptyIndex()
        {
            var doc = new DeleteRequest
            {
                Index = "",
                Id = 2
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteRequestTest_NullIndex()
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
        public void DeleteRequestTest_EmptyBody()
        {
            var doc = new DeleteRequest();
            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteRequestTest_Null()
        {
            var result = apiIstance.Delete(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DeleteRequestTest_Query()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new Query
                {
                    Equals = new
                    {
                        title = "cock cola"
                    }
                }
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        //[TestMethod]
        //public void DeleteRequestTest_WrongQuery()
        //{
        //    var query = new
        //    {
        //        qwerty = new Dictionary<string, object>
        //        {
        //            ["column"] = "cock cola"
        //        }
        //    };

        //    var doc = new DeleteRequest
        //    {
        //        Index = "products",
        //        Query = new Query(query)
        //    };

        //    var result = apiIstance.Delete(doc);

        //    Assert.IsTrue(result.IsSuccess);
        //}

        [TestMethod]
        public void DeleteRequestTest_EmptyQuery()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new()
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
