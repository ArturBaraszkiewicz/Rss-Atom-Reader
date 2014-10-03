using Autofac;
using Manager;
using System;
using Topshelf;
using Topshelf.Autofac;

namespace DownloadingService
{   
    interface IDownloadingService
	{
        void Start();
        void Stop();
	}
    
    class DownloadingService : IDownloadingService
    {
        private ICategoryManager _categoryManager;
        private IProviderManager _providerManager;
        private IContentManager _contentManager;
        //private IParseService _parseService;

        public DownloadingService(ICategoryManager categoryManager, IProviderManager providerManager, 
            IContentManager contentManager)//IParseService parseService)
        {
            _categoryManager = categoryManager;
            _providerManager = providerManager;
            _contentManager = contentManager;

        }


        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CategoryManager>().As<ICategoryManager>();
            builder.RegisterType<ProviderManager>().As<IProviderManager>();
            builder.RegisterType<ContentManager>().As<IContentManager>();
            builder.RegisterType<DownloadingService>();
            var container = builder.Build();

            HostFactory.Run(x => {
                x.UseAutofacContainer(container);
                x.Service<DownloadingService>(c => 
                {
                    c.ConstructUsingAutofacContainer();
                    c.WhenStarted(service => service.Start());
                    c.WhenStopped(service => service.Stop());                
                });

                x.RunAsLocalSystem();

                x.SetDescription("Rss reader data downloading service");
                x.SetDisplayName("Downloading Service for atom and rss");
                x.SetServiceName("rss-atom download");
            });
        }
    }
}
