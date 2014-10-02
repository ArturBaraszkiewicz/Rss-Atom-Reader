using DataBaseProvider;
using DataBaseProvider.Models;
using System.Collections.Generic;
using System.Linq;

namespace Manager
{
    class CategoryManager: IManager<Category>
    {
        public void Add(Category model)
        {
            using(var databaseCtx = new ReaderDataModel()) 
            {
                databaseCtx.Categories.Add(model);
                databaseCtx.SaveChanges();
            }
        }

        public void Update(Category model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                var updateCategory = databaseCtx.Categories.Single(x => x.Id == model.Id);
                updateCategory.CategoryName = model.CategoryName;
                updateCategory.IsActive = model.IsActive;
                databaseCtx.ChangeTracker.DetectChanges();
                databaseCtx.SaveChanges();
            }
        }

        public void Delete(Category model)
        {
            using (var databaseCtx = new ReaderDataModel())
            {
                databaseCtx.Categories.Remove(model);
                databaseCtx.SaveChanges();
            }
        }

        public List<Category> ToList()
        {
            List<Category> categories;
            using (var databaseCtx = new ReaderDataModel())
            {
                var categoryList = databaseCtx.Categories.Where(x=> x.IsActive);
                categories = categoryList.ToList();
            }
            return categories;
        }
    }
}
