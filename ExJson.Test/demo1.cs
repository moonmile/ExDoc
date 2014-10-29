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
    public class Demo1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string json = @"
{
 'CPU': 'Intel',
 'PSU': '500W',
 'Drives': [
   'DVD read/writer',
   '500 gigabyte hard drive',
   '200 gigabype hard drive'
  ]
}";

            string json2 = @"
{'drivers': [
   'DVD read/writer',
   '500 gigabyte hard drive',
   '200 gigabype hard drive'
]}";

            string json3 = @"
{
 'CPU': 'Intel',
 'PSU': '500W',
 'Drives': {
   set1: 'DVD read/writer',
   set2: '500 gigabyte hard drive',
   set3: '200 gigabype hard drive'
  },
 'Languages': [
   'C#',
   'F#',
   'Visual Basic'
  ]
}";

            string json4 = @"
[
   'DVD read/writer',
   '500 gigabyte hard drive',
   '200 gigabype hard drive'
]";

            string json5 = @"
{
 'CPU': 'Intel',
 'PSU': '500W',
 'Drives': {
   set1: 'DVD read/writer',
   set2: '500 gigabyte hard drive',
   set3: '200 gigabype hard drive'
  },
 'Languages': [
   'C#',
   {
        name: 'F#',
        spell: 'Fsharp'
    },
   'Visual Basic'
  ]
}";
            string json6 = @"
{
 'CPU': 'Intel',
 'PSU': '500W',
 'Drives': {
   set1: 'DVD read/writer',
   set2: '500 gigabyte hard drive',
   set3: '200 gigabype hard drive'
  },
 'Languages': [
   'C#',
   {
        name: 'F#',
        spell: 'Fsharp',
        num: [1,2,3]
    },
   'Visual Basic'
  ]
}";

            string xml = @"
<root CPU=""Intel"" PSU=""500W"">
  <Drivers>DVD read/writer</Drivers>
  <Drivers>500 gigabyte hard drive</Drivers>
  <Drivers>200 gigabype hard drive</Drivers>
</root>

".Trim();
            var jdoc = JToken.Parse(json6);
            var dump = jdoc.Dump();
            Debug.WriteLine(dump);

            var doc = jdoc.ToXDocument(XmlPriority.Element);
            Debug.WriteLine(doc.ToString());
            Assert.AreEqual(true, true);

        }

        [TestMethod]
        public void demo1()
        {
            string json = @"
{
'CPU': 'Intel',
'PSU': '500W',
'Drives': {
set1: 'DVD read/writer',
set2: '500 gigabyte hard drive',
set3: '200 gigabype hard drive'
},
'Languages': [
'C#',
'F#',
'Visual Basic'
]
}";

            var jdoc = JToken.Parse(json);
            var xdoc = jdoc.ToXDocument(XmlPriority.Element);
            Debug.WriteLine(xdoc.ToString());
            var doc = ExDocument.Load(xdoc);

            var lang = doc * "Languages";
            foreach (var it in lang)
            {
                Debug.WriteLine("lang" + (string)it);
            }
        }
    }
}
