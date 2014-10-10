using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terradue.ServiceModel.Syndication;

namespace Parsers.Parser
{
    public class RssAtomParser : IRssAtomParser
    {
        public RssAtomParser(Func<IOutput> outputBuilder, Func<IOutputItem> itemBuilder)
        {
            OutputBuilder = outputBuilder;
            ItemBuilder = itemBuilder;
        }
        public bool Check(ISource source)
        {
            return true;
        }

        public IOutput Process(ISource source)
        {
            var syndication = SyndicationFeed.Load(source.XmlReader);
            var output = OutputBuilder();

            output.ChannelTitle = syndication.Title.Text;
            output.ChannelDescription = syndication.Description.Text;
            output.Items = syndication.Items.Select((item) =>
            {
                var outputItem = ItemBuilder();

                outputItem.Title = item.Title.Text;
                outputItem.Description = item.Summary.Text;
                outputItem.PubDate = item.PublishDate > item.LastUpdatedTime ? item.PublishDate : item.LastUpdatedTime;
                return outputItem;
            });

            return output;
        }


        public Func<IOutput> OutputBuilder { get; set; }

        public Func<IOutputItem> ItemBuilder { get; set; }
    }

    public interface IRssAtomParser : IParser
    {
    }
}
