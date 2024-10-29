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

            Assert.IsNotNull(result);
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

            Assert.IsTrue(result.ToString()!.Contains("error"));
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

            Assert.IsTrue(result.ToString()!.Contains("error"));
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

            Assert.IsTrue(result.ToString()!.Contains("error"));
        }

        [TestMethod]
        public void InsertRequestTest_Null()
        {
            var result = apiInstance.Insert(null);

            Assert.IsTrue(result.ToString()!.Contains("error"));
        }

        [TestMethod]
        public void InsertRequestTest_NullDocument()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = null
            };

            Assert.ThrowsException<NullException>(() => apiInstance.Insert(doc));
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
