using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.IO;
using Moonmile.ExDoc;

namespace ExDoc.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSelectAttr()
        {
            var xml = "<html><body><a id='a1' href='link001.html'>title</a>" +
                "<a id='a2' href='link002.html'>title</a>" + 
                "</body></html>";

            var st = new StringReader(xml);
            var doc = XDocument.Load(st);
            var edoc = ExDocument.Load(doc);

            ExElement el = edoc * "a" % "id" == "a2";
            Assert.AreEqual("title", el.Value);
            Assert.AreEqual("link002.html", el % "href");

            xml = "<html><body><a id='a1' href='link011.html'>title</a>" +
                "<a id='a2' href='link012.html'>title</a>" +
                "</body></html>";

            st = new StringReader(xml);
            doc = XDocument.Load(st);
            edoc = ExDocument.Load(doc);

            // el = edoc * "a" % "id" == "a2";
            var els = edoc * "a";
            var attrs = els % "id";
            el = attrs == "a2";
            Assert.AreEqual("title", el.Value);
            Assert.AreEqual("link012.html", el % "href");
        }
    }
}
