using Autofac;
using Manager;
using Parsers;
using Parsers.Service;
using Parsers.Model;
using System;
using System.Linq;
using System.Timers;
using Topshelf;
using Topshelf.Autofac;
using DataBaseProvider;
using System.Xml;
using Terradue.ServiceModel.Syndication;
using DataBaseProvider.Models;

namespace DownloadingService
{
    public interface ISyncDataService
    {
        bool Locked { get; set; }
        void Sync();
    }

    public class XMLSource : ISource
    {

        public System.Xml.XmlReader XmlReader { set; get; }
    }

    public class SyncDataService : ISyncDataService
    {
        //remove in production
        public bool test = true;
        public void TestPrepare() {
            if (test)
            {
                var now = DateTime.Now;
                var testCat = new Category
                {
                    CategoryName = "News",
                    IsActive = true,
                    LastSync = now,
                    SyncPeriod = TimeSpan.FromMinutes(2)
                };
                _categoryManager.Add(testCat);
                var tmpCat = _categoryManager.ToList().First();
                var testProvider = new DataProvider
                {
                    CategoryId = tmpCat.Id,
                    IsActive = true,
                    LastSync = now,
                    ProviderURI = "http://wiadomosci.wp.pl/ver,rss,rss.xml",
                    ProviderName = "Polish News",
                    ProviderType = DataProviderType.ATOM,
                    SyncPeriod = TimeSpan.FromMinutes(2)
                };

                _providerManager.Add(testProvider);
            }
            test = false;
        }
        //end remove
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
            Locked = false;
        }

        public void Sync()
        {
            Locked = true;
            var categories = _categoryManager.ToList();
            Console.WriteLine("SyncStart"); // ToDO remove in production
            categories.ForEach(c => 
            {
                var now = DateTime.Now;
                
                if (TimeToUpdate(c.LastSync, c.SyncPeriod))
                {
                    Console.WriteLine("Update Category: {0}", c.CategoryName);
                    var provider = _providerManager.ToFilteredList(c);
                    provider.ForEach(p =>
                    {
                        var source = new XMLSource()
                        { 
                            XmlReader = XmlReader.Create(p.ProviderURI)
                        };
                        var data  = _parserService.Process(source);
                        Console.WriteLine("Update Provider: {0}", p.ProviderName);
                        var contentFromProvider = _contentManager.ToFilteredList(p);
                        Console.WriteLine("IsNull {0}", contentFromProvider.Count > 0);
                        DateTimeOffset lastItemPubDate;
                        if (contentFromProvider.Count > 0)
                            lastItemPubDate = contentFromProvider.Last().PublicationDate;
                        else
                            lastItemPubDate = DateTimeOffset.MinValue;
                        Console.WriteLine("Time Assign {0}", lastItemPubDate);
                        data.Items.ToList().ForEach(x =>
                        {
                            Console.WriteLine("ChekingParsedData");
                            if ( x.PubDate > lastItemPubDate )
                            {
                                Console.WriteLine("Add {0}", x.Title);
                                var tmpCtn = new ProviderContent
                                {
                                    ProviderId = p.Id,
                                    Title = x.Title,
                                    Content = x.Description,
                                    PublicationDate = new DateTime(x.PubDate.Year, x.PubDate.Month, x.PubDate.Day, x.PubDate.Hour, x.PubDate.Minute, x.PubDate.Second),
                                    Author = ""
                                };
                                Console.WriteLine("model created");
                                _contentManager.Add(tmpCtn);
                                Console.WriteLine("Model added");
                            }
                        });
                        p.LastSync = now;
                    });
                    c.LastSync = now;
                }
            });
            Locked = false;
            Console.WriteLine("SyncEnd");// ToDO remove in production
        }

        private bool TimeToUpdate(DateTime lastUpdate, TimeSpan syncPeriod) 
        {
            var now = DateTime.Now;
            var timeToSync = lastUpdate.Add(syncPeriod);
            var compo = DateTime.Compare(now, timeToSync);
            if (compo >= 0) return true;
            return false;
        }

        private bool TimeToUpdate(long lastUpdate, long syncPeriod)
        {
            var now = DateTime.Now.ToBinary();
            var fromLast = lastUpdate + syncPeriod;
            if (now > fromLast) return true;
            return false;
        }

        public bool Locked { get; set; }
    }

    
    
    class DownloadingService
    {

        private Timer _timer;
        private ISyncDataService _syncDataService;
        
        public DownloadingService(ISyncDataService syncDataService)
        {
            _syncDataService = syncDataService;
            var invokeInterval = TimeSpan.FromMinutes(3).TotalMilliseconds;
            _timer = new Timer(invokeInterval) {AutoReset = true};
            _timer.Elapsed += (sender, eventArgs) =>
            {
                if (((SyncDataService)_syncDataService).test)
                    ((SyncDataService)_syncDataService).TestPrepare();
                if(!_syncDataService.Locked)
                    _syncDataService.Sync();
            };
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
