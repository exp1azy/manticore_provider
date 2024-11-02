using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class AutocompleteTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void AutocompleteTest()
        {
            var request = new AutocompleteRequest
            {
                Index = "articles",
                Query = "ху"
            };

            var result = apiInstance.Autocomplete(request);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AutocompleteTest_EmptyQuery()
        {
            var request = new AutocompleteRequest
            {
                Index = "articles",
                Query = ""
            };

            var result = apiInstance.Autocomplete(request);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AutocompleteTest_NullQuery()
        {
            var request = new AutocompleteRequest
            {
                Index = "articles",
                Query = null
            };

            var result = apiInstance.Autocomplete(request);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void AutocompleteTest_EmptyIndex()
        {
            var request = new AutocompleteRequest
            {
                Index = "",
                Query = "при"
            };

            var result = apiInstance.Autocomplete(request);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void AutocompleteTest_NullIndex()
        {
            var request = new AutocompleteRequest
            {
                Index = null,
                Query = "при"
            };

            var result = apiInstance.Autocomplete(request);

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
