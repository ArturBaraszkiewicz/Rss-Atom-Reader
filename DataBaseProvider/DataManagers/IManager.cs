using DataBaseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    interface IManager{}

    interface ICategoryManager
    {
        void Add(Category model);
        void Update(Category model);
        void Delete(Category model);
        List<Category> ToList();
    }
    
    interface IProviderManager
    {
        void Add(DataProvider model);
        void Update(DataProvider model);
        void Delete(DataProvider model);
        List<DataProvider> ToFilteredList(Category category);
        List<DataProvider> ToList();
    }
    
    interface IContentManager
    {
        void Add(ProviderContent model);
        void Update(ProviderContent model);
        void Delete(ProviderContent model);
        List<ProviderContent> ToFilteredList(DataProvider provider);
        List<ProviderContent> ToList();
    }
}
