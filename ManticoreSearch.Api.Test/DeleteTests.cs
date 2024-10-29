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
                Id = 8217476891905359873
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.ToString()!.Contains("deleted"));
        }

        [TestMethod]
        public void DeleteRequestTest_WrongId()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Id = 100
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.ToString()!.Contains("not found"));
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

            Assert.IsTrue(result.ToString()!.Contains("Negative document ids are not allowed", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void DeleteRequestTest_EmptyIndex()
        {
            var doc = new DeleteRequest
            {
                Index = "",
                Id = 8217476891905359887
            };

            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.ToString()!.Contains("no such table"));
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

            Assert.IsTrue(result.ToString()!.Contains("property value should be a string"));
        }

        [TestMethod]
        public void DeleteRequestTest_EmptyBody()
        {
            var doc = new DeleteRequest();
            var result = apiIstance.Delete(doc);

            Assert.IsTrue(result.ToString()!.Contains("property value should be a string"));
        }

        [TestMethod]
        public void DeleteRequestTest_Null()
        {
            var result = apiIstance.Delete(null);

            Assert.IsTrue(result.ToString()!.Contains("property missing"));
        }

        [TestMethod]
        public void DeleteRequestTest_Query()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new
                {
                    equals = new Dictionary<string, object>
                    {
                        ["title"] = "cock cola"
                    }
                }
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.ToString()!.Contains("error"));
        }

        [TestMethod]
        public void DeleteRequestTest_WrongQuery()
        {
            var doc = new DeleteRequest
            {
                Index = "products",
                Query = new
                {
                    qwerty = new Dictionary<string, object>
                    {
                        ["title"] = "cock cola"
                    }
                }
            };

            var result = apiIstance.Delete(doc);

            Assert.IsFalse(result.ToString()!.Contains("error"));
        }
    }
}
