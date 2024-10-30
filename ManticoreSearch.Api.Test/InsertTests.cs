using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class InsertTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void InsertDocumentTest()
        {
            var doc = new InsertRequest
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
        public void InsertDocumentTest_ErrorTableName()
        {
            var doc = new InsertRequest
            {
                Index = "error",
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
        public void InsertDocumentTest_ManyElementsInDocument()
        {
            var doc = new InsertRequest
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
        public void InsertRequestTest_NullIndex()
        {
            var doc = new InsertRequest
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
        public void InsertRequestTest_EmptyIndex()
        {
            var doc = new InsertRequest
            {
                Index = "",
                Document = new Dictionary<string, object>
                {
                    { "title", "fanta" },
                    { "price", 25.0f },
                    { "count", 1 }
                }
            };

            var result =  apiInstance.Insert(doc);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InsertRequestTest_WrongAttributes()
        {
            var doc = new InsertRequest
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
        public void InsertRequestTest_Null()
        {
            Assert.ThrowsException<InsertException>(() => apiInstance.Insert(null));
        }

        [TestMethod]
        public void InsertRequestTest_NullDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = null
            };

            Assert.ThrowsException<InsertException>(() =>  apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertRequestTest_EmptyDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = []
            };

            Assert.ThrowsException<InsertException>(() => apiInstance.Insert(doc));
        }
    }
}
