using ManticoreSearch.Api.Exceptions;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class SqlTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void SqlTest_CreateTable()
        {
            string query = "create table products(title string, price float, count int)";
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SqlTest_EmptyQuery()
        {
            string query = string.Empty;
            var result = apiInstance.Sql(query);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SqlTest_NullQuery()
        {
            Assert.ThrowsException<NullException>(() => apiInstance.Sql(null));
        }
    }
}