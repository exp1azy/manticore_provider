using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class InsertTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void InsertTest()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 19.0f },
                    { "count", 3 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void InsertTest_ManyElementsInDocument()
        {
            var doc = new ModificationRequest
            {
                Index = "error",
                Document = new Dictionary<string, object>
                {
                    { "title", "cock cola" },
                    { "price", 19.0f },
                    { "count", 3 },
                    { "123", "cock cola" },
                    { "456", 19.0f },
                    { "789", 3 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InsertTest_NullIndex()
        {
            var doc = new ModificationRequest
            {
                Index = null,
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InsertTest_EmptyIndex()
        {
            var doc = new ModificationRequest
            {
                Index = "",
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InsertTest_WrongAttributes()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "", "fanta" },
                    { "qwerty", 25.0f },
                    { "123", 1 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InsertTest_Null()
        {
            Assert.ThrowsException<ModificationException>(() => apiInstance.Insert(null));
        }

        [TestMethod]
        public void InsertRequestTest_NullDocument()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Document = null
            };

            Assert.ThrowsException<ModificationException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertTest_EmptyDocument()
        {
            var doc = new ModificationRequest
            {
                Index = "products",
                Document = []
            };

            Assert.ThrowsException<ModificationException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertTest_Empty()
        {
            var doc = new ModificationRequest();

            Assert.ThrowsException<ModificationException>(() => apiInstance.Insert(doc));
        }
    }
}
