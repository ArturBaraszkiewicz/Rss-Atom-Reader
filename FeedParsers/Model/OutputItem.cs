using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers.Model
{
    public interface IOutputItem
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTimeOffset PubDate { get; set; }
    }

    public class OutputItem : IOutputItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PubDate { get; set; }
    }
}
