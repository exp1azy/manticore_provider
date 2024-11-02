using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class MappingTests
    {
        private readonly ManticoreProvider apiInstance = new("http://194.168.0.126:9308");

        [TestMethod]
        public void MappingTest()
        {
            var request = new MappingRequest
            {
                Properties = new Dictionary<string, MappingField>
                {
                    { "exersice", new MappingField { Type = FieldType.Keyword } },
                    { "working_weight", new MappingField { Type = FieldType.Integer } },
                    { "weight_limit", new MappingField { Type = FieldType.Integer } }
                }
            };

            var result = apiInstance.UseMapping(request, "training");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MappingTest_WrongFieldTypes()
        {
            var request = new MappingRequest
            {
                Properties = new Dictionary<string, MappingField>
                {
                    { "exersice", new MappingField { Type = "string" } },
                    { "working_weight", new MappingField { Type = "int" } },
                    { "weight_limit", new MappingField { Type = "int" } }
                }
            };

            var result = apiInstance.UseMapping(request, "training");

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void MappingTest_EmptyProperties()
        {
            var request = new MappingRequest
            {
                Properties = []
            };

            var result = apiInstance.UseMapping(request, "training");

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void MappingTest_EmptyRequest()
        {
            var request = new MappingRequest();

            var result = apiInstance.UseMapping(request, "training");

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void MappingTest_Null()
        {
            var result = apiInstance.UseMapping(null, "training");

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
