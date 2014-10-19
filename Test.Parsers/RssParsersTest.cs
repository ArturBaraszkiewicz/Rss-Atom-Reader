using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsers;
using System.Xml;
using System.Linq;
using Terradue.ServiceModel.Syndication;
using Parsers.Service;
using Parsers.Parser;
using Parsers.Model;

namespace Test.Parsers
{
    [TestClass]
    public class RssParsersTest
    {

        private ParserService Parser
        {
            get
            {
                return new ParserService(new IParser[]{
                        new RssAtomParser(
                            ()=>new Output(),
                            ()=>new OutputItem()
                        ),
                        new WebParserComposite(),
                    });
            }

        }

        [TestMethod]
        public void RssChannelTitleTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestRSS.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.ChannelTitle, "Gryzone");
        }
        [TestMethod]
        public void RssChannelDescriptionTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestRSS.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.ChannelDescription, "description");
        }
        [TestMethod]
        public void RssChannelFirstItemTitleTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestRSS.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().Title, "New Road The Best!");
        }
        [TestMethod]
        public void RssChannelFirstItemDescriptionTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestRSS.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().Description, "New Road okazał się najlepszą grą na Androida. Uwierzysz w to? bo ja NIE!");
        }
        [TestMethod]
        public void RssChannelFirstItemPubDateStringTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestRSS.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().PubDate,(DateTimeOffset) new DateTime(2014,10,10,10,55,09,DateTimeKind.Utc));
                //XmlConvert.ToDateTimeOffset("Mon, 22 Sep 2014 14:26:52 +0000"));
        }
       
    }
}
