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
                Table = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_NegativeId()
        {
            var doc = new UpdateRequest()
            {
                Table = "products",
                Id = -1,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_WrongId()
        {
            var doc = new UpdateRequest()
            {
                Table = "products",
                Id = 10000,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_EmptyIndex()
        {
            var doc = new UpdateRequest()
            {
                Table = "",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_NullIndex()
        {
            var doc = new UpdateRequest()
            {
                Table = null,
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_EmptyDocument()
        {
            var doc = new UpdateRequest()
            {
                Table = "products",
                Id = 1,
                Document = []
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_NullDocument()
        {
            var doc = new UpdateRequest()
            {
                Table = "products",
                Id = 1,
                Document = null
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_Null()
        {
            var result = apiInstance.Update(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateRequestTest_ErrorTable()
        {
            var doc = new UpdateRequest()
            {
                Table = "error",
                Id = 8217476891905359912,
                Document = new Dictionary<string, object>
                {
                    { "price", 20.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
