using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Moonmile.ExDoc;

namespace ExDoc.Test
{
    [TestClass]
    public class TestExElement
    {
        [TestMethod]
        public void TestQuery1()
        {
            var xml = @"
<root> 
    <person id='1'>masuda</person>
    <person id='2'>
        <child>kaho</child>
        <child>kazuhisa</child>
    </person> 
    <person id='3'>yumi</person>
</root>";

            var doc = ExDocument.LoadXml(xml);

            ExElement el = doc * "person";
            Assert.AreEqual("masuda", el.Value );

            ExElement el2 = doc / "person";
            Assert.AreEqual("masuda", el2.Value);
        }

        [TestMethod]
        public void TestQuery2()
        {
            var xml = @"
<root> 
    <person id='1'>masuda</person>
    <person id='2'>
        <child>kaho</child>
        <child>kazuhisa</child>
    </person> 
    <person id='3'>yumi</person>
</root>";

            var doc = ExDocument.LoadXml(xml);

            /// 子孫を調べるとある
            ExElement el = doc * "child";
            Assert.AreEqual("kaho", el.Value);
            /// 子要素だけだと無い
            ExElement el2 = doc / "child";
            Assert.AreEqual("", el2.Value);
        }
    }
}
