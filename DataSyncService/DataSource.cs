using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService
{
    public class XMLSource : ISource
    {
        public System.Xml.XmlReader XmlReader { set; get; }
    }
}
