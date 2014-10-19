using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService
{
    public class SyncDataServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SyncDataService>().As<ISyncDataService>();
            builder.RegisterType<XMLSource>();
        }
    }
}
