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
    public class TestList
    {
        /// <summary>
        /// 名前付きのリスト
        /// </summary>
        [TestMethod]
        public void TestList1()
        {
            string json = @"
{
 data: [ ""F#"", ""C#"", ""Visual Basic"" ]
}
";
            string xml = @"
<root>
  <data>
    <item>F#</item>
    <item>C#</item>
    <item>Visual Basic</item>
  </data>
</root>
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
        /// 無名のリスト
        /// </summary>
        [TestMethod]
        public void TestList2()
        {
            string json = @"
 [ ""F#"", ""C#"", ""Visual Basic"" ]
";
            string xml = @"
<root>
  <item>F#</item>
  <item>C#</item>
  <item>Visual Basic</item>
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
        /// 複数のリスト
        /// </summary>
        [TestMethod]
        public void TestList3()
        {
            string json = @"
{
 lang: [ ""F#"", ""C#"", ""Visual Basic"" ],
 nums: [ 1,2,3 ]
}
";
            string xml = @"
<root>
  <lang>
    <item>F#</item>
    <item>C#</item>
    <item>Visual Basic</item>
  </lang>
  <nums>
    <item>1</item>
    <item>2</item>
    <item>3</item>
  </nums>
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
