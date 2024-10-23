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
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "cockey coola" },
                    { "price", 30 },
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
