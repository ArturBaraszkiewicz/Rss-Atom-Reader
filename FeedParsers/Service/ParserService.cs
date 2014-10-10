using Parsers.Model;
using Parsers.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers.Service
{
    public class ParserService: IParserService
    {

        protected IEnumerable<IParser> Parsers { get; set; }
        
        public ParserService(IEnumerable<IParser> parsers)
        {
            Parsers = parsers;
        }

        public IOutput Process(ISource source)
        {
            IOutput output = null;
            foreach (var parser in Parsers)
            {
                if (parser.Check(source))
                {
                    output = parser.Process(source);
                    break;
                }
            }
            return output;
        }

        public bool Check(ISource source)
        {
            foreach (var parser in Parsers)
            {
                if (parser.Check(source))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public interface IParserService:IParser
    {
    }
}
