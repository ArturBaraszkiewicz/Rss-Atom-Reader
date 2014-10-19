using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manager;
using DataBaseProvider.Models;

namespace DataBaseProviderTests
{
    [TestClass]
    public class ProviderContentManagerTests
    {
        [TestMethod]
        public void Add_Content_Test()
        {
            var categoryManager = new CategoryManager();
            if (!categoryManager.ToList().Exists(x => x.CategoryName == "Fun"))
            {
                var category = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(category);
            }
            var testCategory = categoryManager.ToList().Find(x=>x.CategoryName == "Fun");
            var dataProviderManager = new ProviderManager();
            if (!dataProviderManager.ToList().Exists(x => x.ProviderName == "TestProvider"))
            {
                var provider = new DataProvider
                {
                    ProviderName = "TestProvider",
                    IsActive = true,
                    CategoryId = testCategory.Id,
                    ProviderType = DataProviderType.RSS,
                    SyncPeriod = testCategory.SyncPeriod,
                    LastSync = testCategory.LastSync,
                    ProviderURI = "Test://uri"
                };
                dataProviderManager.Add(provider);
            }
            var testProvider = dataProviderManager.ToList().Find(x => x.ProviderName == "TestProvider");

            var contentToAdd = new ProviderContent 
            { 
                Title = "SomeTitle",
                PublicationDate = DateTime.Now,
                Content = "Lorem ipsum solor delet i cos tam",
                Author = "Gal Anonim",
                ProviderId = testProvider.Id,
            };

            var contentManager = new ContentManager();
            var beaforeAdd = contentManager.ToList().Count;
            contentManager.Add(contentToAdd);
            var afterAdd = contentManager.ToList().Count;
            Assert.AreNotEqual(beaforeAdd, afterAdd);
        }

        [TestMethod]
        public void Delete_Content_Test()
        {
            var categoryManager = new CategoryManager();
            if (!categoryManager.ToList().Exists(x => x.CategoryName == "Fun"))
            {
                var category = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(category);
            }
            var testCategory = categoryManager.ToList().Find(x => x.CategoryName == "Fun");
            var dataProviderManager = new ProviderManager();
            if (!dataProviderManager.ToList().Exists(x => x.ProviderName == "TestProvider"))
            {
                var provider = new DataProvider
                {
                    ProviderName = "TestProvider",
                    IsActive = true,
                    CategoryId = testCategory.Id,
                    ProviderType = DataProviderType.RSS,
                    SyncPeriod = testCategory.SyncPeriod,
                    LastSync = testCategory.LastSync,
                    ProviderURI = "Test://uri"
                };
                dataProviderManager.Add(provider);
            }
            var testProvider = dataProviderManager.ToList().Find(x => x.ProviderName == "TestProvider");

            var contentToDelete = new ProviderContent
            {
                Title = "ToDelete",
                PublicationDate = DateTime.Now,
                Content = "Lorem ipsum solor delet i cos tam",
                Author = "Gal Anonim",
                ProviderId = testProvider.Id,
            };

            var contentManager = new ContentManager();
            contentManager.Add(contentToDelete);
            contentManager.Delete(contentManager.ToList().Find(x => x.Title == "ToDelete"));
            Assert.AreEqual(false, contentManager.ToList().Exists(x => x.Title == "ToDelete"));
        }

        [TestMethod]
        public void Update_Content_Test()
        {
            var categoryManager = new CategoryManager();
            if (!categoryManager.ToList().Exists(x => x.CategoryName == "Fun"))
            {
                var category = new Category
                {
                    CategoryName = "Fun",
                    IsActive = true,
                    LastSync = DateTime.Now,
                    SyncPeriod = TimeSpan.FromHours(1)
                };
                categoryManager.Add(category);
            }
            var testCategory = categoryManager.ToList().Find(x => x.CategoryName == "Fun");
            var dataProviderManager = new ProviderManager();
            if (!dataProviderManager.ToList().Exists(x => x.ProviderName == "TestProvider"))
            {
                var provider = new DataProvider
                {
                    ProviderName = "TestProvider",
                    IsActive = true,
                    CategoryId = testCategory.Id,
                    ProviderType = DataProviderType.RSS,
                    SyncPeriod = testCategory.SyncPeriod,
                    LastSync = testCategory.LastSync,
                    ProviderURI = "Test://uri"
                };
                dataProviderManager.Add(provider);
            }
            var testProvider = dataProviderManager.ToList().Find(x => x.ProviderName == "TestProvider");

            var contentToUpdate = new ProviderContent
            {
                Title = "ToUpdate",
                PublicationDate = DateTime.Now,
                Content = "Lorem ipsum solor delet i cos tam",
                Author = "Gal Anonim",
                ProviderId = testProvider.Id,
            };

            var contentManager = new ContentManager();
            contentManager.Add(contentToUpdate);
            var updatetingContent = contentManager.ToList().Find(x => x.Title == "ToUpdate");
            updatetingContent.Title = "Updated";
            contentManager.Update(updatetingContent);
            Assert.AreEqual(false, contentManager.ToList().Exists(x => x.Title == "ToUpdate"));
        }
    }
}
