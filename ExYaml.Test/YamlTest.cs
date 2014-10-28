using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpYaml.Serialization;

using Moonmile.ExDoc;
using Moonmile.ExDoc.Yaml;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace ExYaml.Test
{

    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestRootMap()
        {
            var yaml = @"
person: masuda
";
            var xml = @"<person>masuda</person>";

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootMap2()
        {
            var yaml = @"
name: masuda
id:   100
";
            var xml = @"<root name=""masuda"" id=""100"" />";

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootMap3()
        {
            var yaml = @"
name: masuda
id:   100
addr: tokyo
";
            var xml = @"<root name=""masuda"" id=""100"" addr=""tokyo"" />";

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootMap4()
        {
            var yaml = @"
root: 
  name: masuda
  id:   100
  addr: tokyo
";
            var xml = @"<root name=""masuda"" id=""100"" addr=""tokyo"" />";

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootMap5()
        {
            var yaml = @"
root: 
  person:
    name: masuda
    addr: tokyo
  id:   100
";
            var xml = @"
<root id=""100"">
  <person name=""masuda"" addr=""tokyo"" />
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }
        [TestMethod]
        public void TestRootMap6()
        {
            var yaml = @"
root: 
  id:   100
  person:
    name: masuda
    addr: tokyo
";
            var xml = @"
<root id=""100"">
  <person name=""masuda"" addr=""tokyo"" />
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }
    }

    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void TestRootList()
        {
            var yaml = @"
- person: masuda
";
            var xml = @"<person>masuda</person>";

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }
        [TestMethod]
        public void TestRootList2()
        {
            var yaml = @"
- person: masuda
- person: tomoaki
";
            var xml = @"
<root>
  <person>masuda</person>
  <person>tomoaki</person>
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootList3()
        {
            var yaml = @"
root:
 - person: masuda
 - person: tomoaki
";
            var xml = @"
<root>
  <person>masuda</person>
  <person>tomoaki</person>
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        [TestMethod]
        public void TestRootList4()
        {
            var yaml = @"
root:
 - person: masuda
   id: 100
   addr: tokyo
 - person: tomoaki
   id: 101
   addr: osaka
";
            var xml = @"
<root>
  <person id=""100"" addr=""tokyo"">masuda</person>
  <person id=""101"" addr=""osaka"">tomoaki</person>
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }
        [TestMethod]
        public void TestRootList5()
        {
            var yaml = @"
root:
 - person: 
    id: 100
    addr: tokyo
 - person: 
    id: 101
    addr: osaka
";
            var xml = @"
<root>
  <person id=""100"" addr=""tokyo"" />
  <person id=""101"" addr=""osaka"" />
</root>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }
    }


    [TestClass]
    public class MixTest
    {

        /// <summary>
        /// アプリケーション設定風
        /// </summary>
        [TestMethod]
        public void TestMix1()
        {
            var yaml = @"
app:
 - user: masuda
 - apikey:  key-xxxx
 - apipass: password
";
            var xml = @"
<app>
  <user>masuda</user>
  <apikey>key-xxxx</apikey>
  <apipass>password</apipass>
</app>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        /// <summary>
        /// アイテムデータ風
        /// </summary>
        [TestMethod]
        public void TestMix2()
        {
            var yaml = @"
items:
 - item: {id: 1, name: masuda }
 - item: {id: 2, name: tomoaki }
 - item: {id: 3, name: yumi }
";
            var xml = @"
<items>
  <item id=""1"" name=""masuda"" />
  <item id=""2"" name=""tomoaki"" />
  <item id=""3"" name=""yumi"" />
</items>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

        /// <summary>
        /// リストアイテム風
        /// </summary>
        [TestMethod]
        public void TestMix3()
        {
            var yaml = @"
items:
 - item: 
    id: 1
    text: item-1
 - item: 
    id: 2
    text: item-2
    memo: 
      author: masuda
      date:   2014/10/26
 - item: 
    id: 3
    text: item-2
";
            var xml = @"
<items>
  <item id=""1"" text=""item-1"" />
  <item id=""2"" text=""item-2"">
    <memo author=""masuda"" date=""2014/10/26"" />
  </item>
  <item id=""3"" text=""item-2"" />
</items>
".Trim();

            var ys = new YamlStream();
            ys.Load(new StringReader(yaml));
            YamlDocument ydoc = ys.Documents[0];
            var dump = ydoc.Dump();
            Debug.WriteLine(dump);

            var xdoc = ydoc.ToXDocument();
            var xml1 = xdoc.ToString(SaveOptions.None);
            Debug.WriteLine(xml1);
            Assert.AreEqual(xml, xml1);
        }

    }

}
