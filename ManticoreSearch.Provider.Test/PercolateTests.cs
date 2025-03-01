﻿using ManticoreSearch.Provider.Exceptions;
using ManticoreSearch.Provider.Models.Requests;

namespace ManticoreSearch.Provider.Test
{
    [TestClass]
    public class PercolateTests
    {
        private readonly ManticoreProvider apiInstance = new();

        [TestMethod]
        public void PercolateTest()
        {
            var percolate = new PercolateRequest
            {
                Query = new PercolateRequestQuery
                {
                    Percolate = new PercolateDocument
                    {
                        Documents = new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                { "title", "water" }
                            },
                            new Dictionary<string, object>
                            {
                                { "title", "soda" }
                            }
                        }
                    }
                }
            };

            var result = apiInstance.Percolate(percolate, "mypq");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateTest()
        {
            var percolate = new PercolationActionRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "soda" }
                    }
                }
            };

            var result = apiInstance.IndexPercolate(percolate, "mypq", 2);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateTest_Empty()
        {
            var percolate = new PercolationActionRequest();

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 6);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void IndexPercolateTest_WrongAttributes()
        {
            var percolate = new PercolationActionRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "1234", "1234" }
                    }
                }
            };

            var result = apiInstance.IndexPercolate(percolate, "pqtable", 6);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void GetPeroclateTest()
        {
            var result = apiInstance.GetPercolate("pqtable", 1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GetPercolateTest_NegativeId()
        {
            var result = apiInstance.GetPercolate("pqtable", -1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GetPercolateTest_EmptyIndex()
        {
            var result = apiInstance.GetPercolate("", 1);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UpdatePercolateTest()
        {
            var request = new PercolationActionRequest
            {
                Query = new Query
                {
                    Match = new Dictionary<string, object>
                    {
                        { "title", "Кимерсен" }
                    }
                }
            };

            var result = apiInstance.UpdatePercolate(request, "pqtable", 1);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
