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
                Id = 232232332,
                Index = "products",
                Document = new Dictionary<string, object>
                {
                    { "title", "cockey coola" },
                    { "price", 30 },
                }
            };

            var result = apiInstance.Update(doc);
        }

        [TestMethod]
        public void UpdateNullRequestTest()
        {
            var result = apiInstance.Update(null);
        }
    }
}
