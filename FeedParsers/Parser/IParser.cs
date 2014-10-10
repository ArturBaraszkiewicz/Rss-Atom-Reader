using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers.Parser
{
    public interface IParser
    {
        bool Check(ISource source);
        IOutput Process(ISource source);
    }
}
