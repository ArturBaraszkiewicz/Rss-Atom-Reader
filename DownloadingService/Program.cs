using Autofac;
using Manager;
using Parsers;
using Parsers.Service;
using System;
using System.Timers;
using Topshelf;
using Topshelf.Autofac;
using DataBaseProvider;

namespace DownloadingService
{
    public interface ISyncDataService
    {
        void Sync();
    }

    public class SyncDataService : ISyncDataService
    {
        private ICategoryManager _categoryManager;
        private IProviderManager _providerManager;
        private IContentManager _contentManager;
        //private IParseService _parseService;

        public SyncDataService(ICategoryManager categoryManager, IProviderManager providerManager,
            IContentManager contentManager)
        {
            _categoryManager = categoryManager;
            _providerManager = providerManager;
            _contentManager = contentManager;
        }

        public void Sync()
        {
            var categories = _categoryManager.ToList();
            categories.ForEach(c => 
            {
                var provider = _providerManager.ToFilteredList(c);
                provider.ForEach(p =>
                {
                    var content = _contentManager.ToFilteredList(p);
                    //TODO after parsers inserting a content
                });
            });
        }

        
    }

    interface IDownloadingService
	{
        void Start();
        void Stop();
	}
    
    class DownloadingService : IDownloadingService
    {

        private Timer _timer;
        private DateTime nowTime;
        private ISyncDataService _syncDataService;
        
        public DownloadingService(ISyncDataService syncDataService, IParserService parserService)
        {
            _syncDataService = syncDataService;
            _timer = new Timer(1000) {AutoReset = true};
            _timer.Elapsed += (sender, eventArgs) => nowTime = DateTime.Now;
        }


        public void Start()
        {
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private bool IsTimeToUpdate()
        {

            return false;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new ParsesModule());
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
