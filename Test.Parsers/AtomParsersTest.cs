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
    public class AtomParsersTest
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
        public void AtomChannelTitleTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestAtom.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.ChannelTitle, "Atomowy Gryzone");
        }
        [TestMethod]
        public void AtomChannelDescriptionTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestAtom.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.ChannelDescription, "atomowo");
        }
        [TestMethod]
        public void AtomChannelFirstItemTitleTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestAtom.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().Title, "Atomowy nowy road");
        }
        [TestMethod]
        public void AtomChannelFirstItemDescriptionTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestAtom.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().Description, "Kiedys cos tam zarobi");
        }
        [TestMethod]
        public void AtomChannelFirstItemPubDateStringTest()
        {
            var source = new LocalSource()
            {
                XmlReader = XmlReader.Create("Sources\\TestAtom.xml")
            };
            var processor = Parser;

            var output = processor.Process(source);
            Assert.AreEqual(output.Items.ToList().First().PubDate, (DateTimeOffset)new DateTime(2005, 6, 13, 16, 20, 02, DateTimeKind.Utc));
        }

    }
}
