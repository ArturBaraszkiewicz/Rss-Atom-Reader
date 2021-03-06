﻿using DataBaseProvider.Models;
using DataBaseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public interface IContentManager : IManager<ProviderContent>
    {
        List<ProviderContent> ToFilteredList(DataProvider provider);
    }

    public class ContentManager: IContentManager
    {
        public void Add(ProviderContent model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.ProvidersContent.Add(model);
                databaseCtx.SaveChanges();
            }
        }

        public void Update(ProviderContent model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.Entry(model).State = System.Data.Entity.EntityState.Modified;
                databaseCtx.SaveChanges();
            }
        }

        public void Delete(ProviderContent model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                databaseCtx.SaveChanges();
            }
        }

        public List<ProviderContent> ToFilteredList(DataProvider provider)
        {
            List<ProviderContent> filteredContent = null;
            using (var databaseCtx = new ReaderDataModel())
            {
                var contentFilter = databaseCtx.ProvidersContent
                                    .Where(x => x.Provider.Id == provider.Id);
                filteredContent = contentFilter.ToList();
            }

            return filteredContent == null ? null : filteredContent;
        }

        public List<ProviderContent> ToList()
        {
            List<ProviderContent> contentList = null;
            using (var databaseCtx = new ReaderDataModel())
            {
                contentList = databaseCtx.ProvidersContent.ToList();
            }
            return contentList == null ? null : contentList;
        }
    }
}
