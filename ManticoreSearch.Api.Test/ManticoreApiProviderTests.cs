using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class ManticoreApiProviderTests
    {
        private ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void CreateTableSqlTest()
        {
            string query = "create table products(title string, price float, count int)";
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        public void EmptyQuerySqlTest()
        {
            string query = string.Empty;
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        public void NullQuerySqlTest()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Sql(null));
        }

        [TestMethod]
        public void InsertDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "apple" },
                    { "price", 10.0f },
                    { "count", 5 }
                }
            };

            var result = apiInstance.Insert(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertNullRequestTest()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Insert(null));
        }

        [TestMethod]
        public void InsertRequestWithEmptyIndexTest()
        {
            var doc = new InsertRequest
            {
                Index = null,
                Document = new Dictionary<string, object>
                {
                    { "title", "apple" },
                    { "price", 10.0f },
                    { "count", 5 }
                }
            };

            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Insert(doc));
        }

        [TestMethod]
        public void InsertRequestWithEmptyDocumentTest()
        {
            var doc = new InsertRequest
            {
                Index = "products",
                Document = null
            };

            Assert.ThrowsException<InsertException>(() => apiInstance.Insert(doc));
        }
    }
}