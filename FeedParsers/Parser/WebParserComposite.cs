using Parsers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers.Parser
{
    public class WebParserComposite:IWebParserComposite
    {
        public bool Check(ISource source)
        {
            return false;
        }

        public IOutput Process(ISource source)
        {
            throw new NotImplementedException();
        }
    }

    public interface IWebParserComposite:IParser
    {
    }
}
