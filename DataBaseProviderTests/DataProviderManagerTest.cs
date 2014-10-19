using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manager;
using DataBaseProvider.Models;

namespace DataBaseProviderTests
{
    [TestClass]
    public class DataProviderManagerTest
    {
        [TestMethod]
        public void Add_DataProvider_Test()
        {
            Category categoryFun;
            var categoryManager = new CategoryManager();
            if(!categoryManager.ToList().Exists(x=>x.CategoryName == "Fun"))
            {
                var tmpFun = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(tmpFun);
            }
            categoryFun = categoryManager.ToList().Find(x=>x.CategoryName == "Fun");

            var dataProvider = new DataProvider 
            {
                IsActive = true,
                ProviderName = "TestProvider",
                ProviderURI = "some://uri.test",
                LastSync = categoryFun.LastSync,
                SyncPeriod = categoryFun.SyncPeriod,
                ProviderType = DataProviderType.RSS,
                CategoryId = categoryFun.Id
            };

            var providerManager = new ProviderManager();
            var beaforeAdd = providerManager.ToList().Count;
            providerManager.Add(dataProvider);
            var providersList = providerManager.ToList().Count;
            Assert.AreNotEqual(beaforeAdd, providersList);
        }
       
        [TestMethod]
        public void Delete_Provider_Test_By_Provider_Name()
        {
            Category categoryFun;
            var categoryManager = new CategoryManager();
            if (!categoryManager.ToList().Exists(x => x.CategoryName == "Fun"))
            {
                var tmpFun = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(tmpFun);
            }
            categoryFun = categoryManager.ToList().Find(x => x.CategoryName == "Fun");

            var dataProvider = new DataProvider
            {
                IsActive = true,
                ProviderName = "ToDelete",
                ProviderURI = "some://uri.test",
                LastSync = categoryFun.LastSync,
                SyncPeriod = categoryFun.SyncPeriod,
                ProviderType = DataProviderType.RSS,
                CategoryId = categoryFun.Id
            };

            var providerManager = new ProviderManager();
            providerManager.Add(dataProvider);
            var delProvider = providerManager.ToList().Find(x => x.ProviderName == "ToDelete");
            providerManager.Delete(delProvider);
            var isDeleted = providerManager.ToList().Exists(x => x.ProviderName == "ToDelete");
            Assert.AreEqual(false, isDeleted);
        }

        [TestMethod]
        public void Update_Provider_Test()
        {
            Category categoryFun;
            var categoryManager = new CategoryManager();
            if (!categoryManager.ToList().Exists(x => x.CategoryName == "Fun"))
            {
                var tmpFun = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(tmpFun);
            }
            categoryFun = categoryManager.ToList().Find(x => x.CategoryName == "Fun");

            var dataProvider = new DataProvider
            {
                IsActive = true,
                ProviderName = "ToUpdate",
                ProviderURI = "some://uri.test",
                LastSync = categoryFun.LastSync,
                SyncPeriod = categoryFun.SyncPeriod,
                ProviderType = DataProviderType.RSS,
                CategoryId = categoryFun.Id
            };

            var providerManager = new ProviderManager();
            providerManager.Add(dataProvider);
            var updateingProvider = providerManager.ToList().Find(x => x.ProviderName == "ToUpdate");
            updateingProvider.ProviderName = "Updated";
            providerManager.Update(updateingProvider);
            Assert.AreEqual(true, providerManager.ToList().Exists(x => x.ProviderName == "Updated"));
        }
    }
}
