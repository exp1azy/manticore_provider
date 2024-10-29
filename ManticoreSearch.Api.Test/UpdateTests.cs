using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class UpdateTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void UpdateRequestTest()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("updated"));
        }

        [TestMethod]
        public void UpdateRequestTest_NegativeId()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = -1,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("negative document ids are not allowed", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void UpdateRequestTest_WrongId()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 5,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("noop"));
        }

        [TestMethod]
        public void UpdateRequestTest_EmptyIndex()
        {
            var doc = new UpdateRequest()
            {
                Index = "",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("no such table"));
        }

        [TestMethod]
        public void UpdateRequestTest_NullIndex()
        {
            var doc = new UpdateRequest()
            {
                Index = null,
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("property value should be a string"));
        }

        [TestMethod]
        public void UpdateRequestTest_EmptyDocument()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = []
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("noop"));
        }

        [TestMethod]
        public void UpdateRequestTest_NullDocument()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = null
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.ToString()!.Contains("property value should be an object"));
        }

        [TestMethod]
        public void UpdateRequestTest_Null()
        {
            var result = apiInstance.Update(null);

            Assert.IsTrue(result.ToString()!.Contains("property missing"));
        }
    }
}
