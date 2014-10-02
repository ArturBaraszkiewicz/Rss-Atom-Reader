namespace DataBaseProvider
{
    using DataBaseProvider.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ReaderDataModel : DbContext, IDisposable
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
}