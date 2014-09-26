namespace DataBaseProvider
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ReaderDataModel : DbContext
    {
        // Your context has been configured to use a 'ReaderDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataBaseProvider.ReaderDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ReaderDataModel' 
        // connection string in the application configuration file.
        public ReaderDataModel()
            : base("name=ReaderDataModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DataProvider> DataProviders { get; set; }
        public virtual DbSet<ProviderContent> ProvidersContent { get; set; }
    }

    public enum DataProviderType {
        RSS, ATOM, WEB_SITE
    }

    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }


    public class DataProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderURI { get; set; }
        public DataProviderType ProviderType { get; set; }
        public Category ProviderCategory { get; set; }
    }

    public class ProviderContent 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public DataProvider Provider { get; set; }
    }
}