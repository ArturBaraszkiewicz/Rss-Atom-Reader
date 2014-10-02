using DataBaseProvider;
using DataBaseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    class CategoryManager: IManager<Category>
    {
        public void Add(Category model)
        {
            using(var databaseCtx = new ReaderDataModel()) 
            { 
                
            
            }
        }

        public void Update(Category model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category model)
        {
            throw new NotImplementedException();
        }

        public List<Category> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
