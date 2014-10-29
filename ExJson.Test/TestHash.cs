using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using Moonmile.ExDoc.Json;
using Moonmile.ExDoc;

namespace ExJson.Test
{
    [TestClass]
    public class TestHash
    {
        /// <summary>
        /// 無名のハッシュ
        /// </summary>
        [TestMethod]
        public void TestHash1()
        {
            string json = @"
{
 'CPU': 'Intel',
 'PSU': '500W',
}";
            string xml = @"
<root CPU=""Intel"" PSU=""500W"" />
".Trim();

            var jdoc = JToken.Parse(json);
            var dump = jdoc.Dump();
            Debug.WriteLine(dump);

            var doc = jdoc.ToXDocument();
            var oxml = doc.ToString();
            Debug.WriteLine( oxml );
            Assert.AreEqual(xml, oxml );
        }

        /// <summary>
        /// 名前付きハッシュ
        /// </summary>
        [TestMethod]
        public void TestHash2()
        {
            string json = @"
{
 PC: {
  'CPU': 'Intel',
  'PSU': '500W',
 }
}";
            string xml = @"
<root>
  <PC CPU=""Intel"" PSU=""500W"" />
</root>
".Trim();

            var jdoc = JToken.Parse(json);
            var dump = jdoc.Dump();
            Debug.WriteLine(dump);

            var doc = jdoc.ToXDocument();
            var oxml = doc.ToString();
            Debug.WriteLine(oxml);
            Assert.AreEqual(xml, oxml);
        }

        /// <summary>
        /// 複数の名前付きハッシュ
        /// </summary>
        [TestMethod]
        public void TestHash3()
        {
            string json = @"
{
 PC1: {
  'CPU': 'Intel',
  'PSU': '500W',
 },
 PC2: {
  'CPU': 'AMD',
  'PSU': '300W',
 }
}";
            string xml = @"
<root>
  <PC1 CPU=""Intel"" PSU=""500W"" />
  <PC2 CPU=""AMD"" PSU=""300W"" />
</root>
".Trim();

            var jdoc = JToken.Parse(json);
            var dump = jdoc.Dump();
            Debug.WriteLine(dump);

            var doc = jdoc.ToXDocument();
            var oxml = doc.ToString();
            Debug.WriteLine(oxml);
            Assert.AreEqual(xml, oxml);
        }
    }
}
