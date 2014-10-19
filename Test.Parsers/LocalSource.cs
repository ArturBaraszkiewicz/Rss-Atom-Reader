using Parsers;
using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Test.Parsers
{
    public class LocalSource : ISource
    {
        public XmlReader XmlReader { get; set; }
    }
}
