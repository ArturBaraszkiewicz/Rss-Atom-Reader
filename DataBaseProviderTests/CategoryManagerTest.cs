using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataBaseProvider;
using Manager;
using DataBaseProvider.Models;

namespace Test.DataBaseProvider
{
    [TestClass]
    public class CategoryManagerTest
    {
        [TestMethod]
        public void Add_Category_Test()
        {
            var categoryManager = new CategoryManager();
            var category = new Category { CategoryName = "Sport", IsActive = true, SyncPeriod = TimeSpan.FromHours(1), LastSync = DateTime.Now };
            categoryManager.Add(category);
            var categoryList = categoryManager.ToList();
            Assert.AreNotEqual(0, categoryList.Count);
        }

        [TestMethod]
        public void Delete_Category_Test()
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
        public void Update_Category_Test() 
        {
            var categoryToUpdate = new Category
            {
                CategoryName = "ToUpdate",
                IsActive = true,
                LastSync = DateTime.Now,
                SyncPeriod = TimeSpan.FromHours(1),
            };
            var categoryManager = new CategoryManager();

            categoryManager.Add(categoryToUpdate);

            var updateingCategory = categoryManager.ToList().Find(x => x.CategoryName == "ToUpdate");
            updateingCategory.CategoryName = "Updated";
            categoryManager.Update(updateingCategory);
            var updatedCategory = categoryManager.ToList().Find(x => x.CategoryName == "Updated");
            Assert.AreNotEqual(categoryToUpdate.CategoryName, updatedCategory.CategoryName);

        }
    }
}
