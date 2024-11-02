using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class UpdateTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void UpdateTest()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "goida" },
                    { "price", 30.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_NegativeId()
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_WrongId()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 50,
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
        public void UpdateTest_EmptyIndex()
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_NullIndex()
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

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_EmptyDocument()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = []
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_NullDocument()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = null
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_Null()
        {
            var result = apiInstance.Update(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_ErrorTable()
        {
            var doc = new UpdateRequest()
            {
                Index = "error",
                Id = 8217476891905359912,
                Document = new Dictionary<string, object>
                {
                    { "count", 20 }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UpdateTest_WrongAttributes()
        {
            var doc = new UpdateRequest()
            {
                Index = "error",
                Id = 8217476891905359912,
                Document = new Dictionary<string, object>
                {
                    { "123", 20 },
                    { "456", "" }
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
