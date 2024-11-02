using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class ReplaceTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void ReplaceTest()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 100.0f },
                    { "count", 25 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceTest_WrongIndex()
        {
            var doc = new ModificationRequest
            {
                Index = "",
                Id = 2,
                Document = new Dictionary<string, object>
                {
                    { "title", "coca cola" },
                    { "price", 26.0f },
                    { "count", 2 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceTest_Null()
        {
            Assert.ThrowsException<ModificationException>(() => apiInstance.Replace(null));
        }

        [TestMethod]
        public void ReplaceTest_WrongId()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Id = 1000,
                Document = new Dictionary<string, object>
                {
                    { "title", "pepsi" },
                    { "price", 20.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceTest_NegativeId()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Id = -1,
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ReplaceTest_NullDocument()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Id = 1,
                Document = null
            };

            Assert.ThrowsException<ModificationException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceTest_EmptyDocument()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Id = 3,
                Document = []
            };

            Assert.ThrowsException<ModificationException>(() => apiInstance.Replace(doc));
        }

        [TestMethod]
        public void ReplaceTest_WithoutId()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Replace(doc);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
