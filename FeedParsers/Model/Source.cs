using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Parsers.Model
{
    public interface ISource
    {
        XmlReader XmlReader { get; }

    }

}
