﻿using DataBaseProvider;
using DataBaseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public interface IProviderManager : IManager<DataProvider>
    {
        List<DataProvider> ToFilteredList(Category category);
    }

    public class ProviderManager: IProviderManager
    {
        public void Add(DataProvider model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.DataProviders.Add(model);
                databaseCtx.SaveChanges();
            }
        }

        public void Update(DataProvider model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.Entry(model).State = System.Data.Entity.EntityState.Modified;
                databaseCtx.SaveChanges();
            }
        }

        public void Delete(DataProvider model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                databaseCtx.SaveChanges();
            }
        }

        public List<DataProvider> ToFilteredList(Category category)
        {
            List<DataProvider> filteredDataProviders = null;
            using (var databaseCtx = new ReaderDataModel())
            {
                var filter = databaseCtx.DataProviders.Where(x => x.ProviderCategory.Id == category.Id && x.IsActive);
                filteredDataProviders = filter.ToList();
            }
            return filteredDataProviders == null ? null : filteredDataProviders;
        }

        public List<DataProvider> ToList()
        {
            List<DataProvider> dataProviders = null;
            using (var databaseCtx = new ReaderDataModel())
            {
                var providersList = databaseCtx.DataProviders.Where(x => x.IsActive);
                dataProviders = providersList.ToList();
            }
            return dataProviders == null ? null : dataProviders;
        }
    }
}
