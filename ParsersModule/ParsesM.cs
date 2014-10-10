using Autofac;
using Parsers.Parser;
using Parsers.Service;
using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsersModule
{
    public class ParsesM : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //service
            builder.RegisterType<ParserService>().As<IParserService>().SingleInstance();

            //parsers
            builder.RegisterType<RssAtomParser>().As<IParser>().SingleInstance();
            builder.RegisterType<WebParserComposite>().As<IParser>().SingleInstance();

            //models
            builder.RegisterType<Output>().As<IOutput>();
            builder.RegisterType<OutputItem>().As<IOutputItem>();


        }
    }
}
