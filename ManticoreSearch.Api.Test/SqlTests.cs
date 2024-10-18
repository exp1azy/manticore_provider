using ManticoreSearch.Api.Exceptions;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class SqlTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void CreateTableSqlTest()
        {
            string query = "create table products(title string, price float, count int)";
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmptyQuerySqlTest()
        {
            string query = string.Empty;
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NullQuerySqlTest()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Sql(null));
        }
    }
}