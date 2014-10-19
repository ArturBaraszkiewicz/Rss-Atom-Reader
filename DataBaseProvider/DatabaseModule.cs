using Autofac;
using DataBaseProvider.Models;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContentManager>()
                .As<IContentManager>()
                .As<IManager<ProviderContent>>()
                .As<IManager>();

            builder.RegisterType<ProviderManager>()
                .As<IProviderManager>()
                .As<IManager<DataProvider>>()
                .As<IManager>();

            builder.RegisterType<CategoryManager>()
                .As<ICategoryManager>()
                .As<IManager<Category>>()
                .As<IManager>();
        }
    }
}
