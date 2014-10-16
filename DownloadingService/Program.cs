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
        private IParserService _parserService;

        public SyncDataService(ICategoryManager categoryManager, IProviderManager providerManager,
            IContentManager contentManager, IParserService parserService)
        {
            _categoryManager = categoryManager;
            _providerManager = providerManager;
            _contentManager = contentManager;
            _parserService = parserService;
        }

        public void Sync()
        {
            var categories = _categoryManager.ToList();
            Console.WriteLine("SyncStart"); // ToDO remowe in production
            categories.ForEach(c => 
            {
                var now = DateTime.Now;
                var updated = false;
                if (TimeToUpdate(c.LastSync, c.SyncPeriod))
                {
                    var provider = _providerManager.ToFilteredList(c);
                    provider.ForEach(p =>
                    {
                        var content = _contentManager.ToFilteredList(p);

                    });
                    updated = true;
                }
                if (updated)
                {
                    c.LastSync = now;
                    updated = false;
                }

            });
            Console.WriteLine("SyncEnd");
        }

        private bool TimeToUpdate(DateTime lastUpdate, TimeSpan syncPeriod) 
        {
            var timeToSync = lastUpdate.AddHours(syncPeriod.Hours);
            var diff = timeToSync - lastUpdate;
            if (diff.Duration() > syncPeriod.Duration()) return true;
            return false;
        }
        
    }

    
    
    class DownloadingService
    {

        private Timer _timer;
        private DateTime nowTime;
        private ISyncDataService _syncDataService;
        
        public DownloadingService(ISyncDataService syncDataService)
        {
            _syncDataService = syncDataService;
            var invokeInterval = TimeSpan.FromMinutes(3).TotalMilliseconds;
            _timer = new Timer(invokeInterval) {AutoReset = true};
            _timer.Elapsed += (sender, eventArgs) => _syncDataService.Sync();
        }


        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new ParsesModule());
            builder.RegisterType<SyncDataService>().As<ISyncDataService>();
            builder.RegisterType<DownloadingService>();
            var container = builder.Build();

            HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);
                x.Service<DownloadingService>(c =>
                {
                    c.ConstructUsingAutofacContainer();
                    c.WhenStarted(service => service.Start());
                    c.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Rss reader data downloading service");
                x.SetDisplayName("DownloadingServiceForAtomAndRss");
                x.SetServiceName("rss-atom-download");
            });
           
        }
    }
}
