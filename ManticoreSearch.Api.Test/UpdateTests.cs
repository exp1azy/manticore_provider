using ManticoreSearch.Api.Exceptions;
using ManticoreSearch.Api.Models.Requests;

namespace ManticoreSearch.Api.Test
{
    [TestClass]
    public class UpdateTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void UpdateRequestTest()
        {
            var doc = new UpdateRequest()
            {
                Index = "products",
                Id = 1,
                Document = new Dictionary<string, object>
                {
                    { "title", "cock" },
                    { "price", 50 },
                }
            };

            var result = apiInstance.Update(doc);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateRequestTest_Null()
        {
            Assert.ThrowsException<HttpRequestFailureException>(() => apiInstance.Update(null));  
        }
    }
}
