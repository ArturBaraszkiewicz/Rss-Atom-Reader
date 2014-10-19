using Autofac;
using DataBaseProvider;
using DataSyncService;
using Parsers;
using Topshelf;
using Topshelf.Autofac;

namespace DownloadingService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new ParsesModule());
            builder.RegisterModule(new SyncDataServiceModule());
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
