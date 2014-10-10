using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers.Model
{
    public class Output : IOutput
    {
        public string ChannelTitle
        {
            get;
            set;
        }
        public string ChannelDescription
        {
            get;
            set;
        }

        public IEnumerable<IOutputItem> Items
        {
            get;
            set;
        }
    }

    public interface IOutput
    {
        string ChannelTitle { get; set; }
        string ChannelDescription { get; set; }
        IEnumerable<IOutputItem> Items { get; set; }
    }
}
