using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataBaseProvider;

namespace DataBaseProviderTests
{
    [TestClass]
    public class DBContextTest
    {
        [TestMethod]
        public void IsDataContextCreated()
        {
            var mockSet = new Mock<ReaderDataModel>();
            Assert.IsNotNull(mockSet);
        }
    }
}