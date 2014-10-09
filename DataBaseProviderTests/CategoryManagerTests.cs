using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataBaseProvider;
using Manager;
using DataBaseProvider.Models;

namespace DataBaseProviderTests
{
    [TestClass]
    public class DBContextTests
    {
        [TestMethod]
        public void IsDataContextCreated()
        {
            var mockSet = new Mock<ReaderDataModel>();
            Assert.IsNotNull(mockSet);
        }
    }

    [TestClass]
    public class CategoryManagerTest
    {
        [TestMethod]
        public void CategoryAddTest()
        {
            var categoryManager = new CategoryManager();
            var category = new Category { CategoryName = "Sport", IsActive = true, SyncPeriod = TimeSpan.FromHours(1), LastSync = DateTime.Now };
            categoryManager.Add(category);
            var categoryList = categoryManager.ToList();
            Assert.AreNotEqual(0, categoryList.Count);
        }

        [TestMethod]
        public void CategoryDeleteTest()
        {
            var categoryManager = new CategoryManager();
            var category = new Category { CategoryName = "ToDelete", IsActive = true, SyncPeriod = TimeSpan.FromHours(1), LastSync = DateTime.Now };
            categoryManager.Add(category);
            var isExist = categoryManager.ToList().Exists(x => x.CategoryName == "ToDelete");
            var categoryToDelete = categoryManager.ToList().Find(x => x.CategoryName == "ToDelete");
            categoryManager.Delete(categoryToDelete);
            var isNotExist = categoryManager.ToList().Exists(x => x.CategoryName == "ToDelete");
            Assert.AreNotEqual(isExist, isNotExist);
        }

        [TestMethod]
        public void CategoryUpdateTest() 
        { 
            
        }
    }
}
