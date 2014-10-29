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
    public class TestComplex
    {
        /// <summary>
        /// ハッシュの中にリスト
        /// </summary>
        [TestMethod]
        public void TestComplex1()
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
   'Visual Basic' ]
}";
            string xml = @"
<root CPU=""Intel"" PSU=""500W"">
  <Drives set1=""DVD read/writer"" set2=""500 gigabyte hard drive"" set3=""200 gigabype hard drive"" />
  <Languages>
    <item>C#</item>
    <item>F#</item>
    <item>Visual Basic</item>
  </Languages>
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
        /// ハッシュの中にリスト
        /// </summary>
        [TestMethod]
        public void TestComplex2()
        {
            string json = @"
{
 num:  [1,2,3],
 word: [ ""one"", ""two"", ""three"" ],
 lang: [ ""F#"", ""C#"", ""Visual Basic"" ]
}";
            string xml = @"
<root>
  <num>
    <item>1</item>
    <item>2</item>
    <item>3</item>
  </num>
  <word>
    <item>one</item>
    <item>two</item>
    <item>three</item>
  </word>
  <lang>
    <item>F#</item>
    <item>C#</item>
    <item>Visual Basic</item>
  </lang>
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
        /// リストの中にハッシュ
        /// </summary>
        [TestMethod]
        public void TestComplex3()
        {
            string json = @"
[
 { name: ""masuda"",  id: 10 },
 { name: ""tomoaki"", id: 20 },
 { id: -1 },
]
";
            string xml = @"
<root>
  <item name=""masuda"" id=""10"" />
  <item name=""tomoaki"" id=""20"" />
  <item id=""-1"" />
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
        /// 艦これのデータを試しにパース
        /// </summary>
        [TestMethod]
        public void TestComplex4()
        {
            string json = @"
{
  ""api_result"":1,
  ""api_result_msg"":""成功"",
  ""api_data"":{
    ""api_ship_id"":[-1,509,509,502,502,502,-1],
    ""api_win_rank"":""S"",
    ""api_get_exp"":60,
    ""api_mvp"":1,
    ""api_member_lv"":24,
    ""api_member_exp"":29431,
    ""api_get_base_exp"":120,
    ""api_get_ship_exp"":[-1,432,144,144,144,144,144],
    ""api_get_exp_lvup"":[[69933,70300,74100],[2471,2800],[2574,2800],[8773,9100],[9330,10500],[12372,13600]],
    ""api_dests"":5,
    ""api_destsf"":1,
    ""api_lost_flag"":[-1,0,0,0,0,0,0],
    ""api_quest_name"":""カムラン半島"",
    ""api_quest_level"":3,
    ""api_enemy_info"":{
      ""api_level"":"""",
      ""api_rank"":"""",
      ""api_deck_name"":""敵前衛部隊""
    },
    ""api_first_clear"":0,
    ""api_get_flag"":[0,1,0],
    ""api_get_ship"":{
      ""api_ship_id"":32,
      ""api_ship_type"":""駆逐艦"",
      ""api_ship_name"":""初雪"",
      ""api_ship_getmes"":""初雪……です……よろしく。""
    },
    ""api_get_eventflag"":0
  }
}";
            string xml = @"
<root api_result=""1"" api_result_msg=""成功"">
  <api_data api_win_rank=""S"" api_get_exp=""60"" api_mvp=""1"" api_member_lv=""24"" api_member_exp=""29431"" api_get_base_exp=""120"" api_dests=""5"" api_destsf=""1"" api_quest_name=""カムラン半島"" api_quest_level=""3"" api_first_clear=""0"" api_get_eventflag=""0"">
    <api_ship_id>
      <item>-1</item>
      <item>509</item>
      <item>509</item>
      <item>502</item>
      <item>502</item>
      <item>502</item>
      <item>-1</item>
    </api_ship_id>
    <api_get_ship_exp>
      <item>-1</item>
      <item>432</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
    </api_get_ship_exp>
    <api_get_exp_lvup>
      <item>
        <item>69933</item>
        <item>70300</item>
        <item>74100</item>
      </item>
      <item>
        <item>2471</item>
        <item>2800</item>
      </item>
      <item>
        <item>2574</item>
        <item>2800</item>
      </item>
      <item>
        <item>8773</item>
        <item>9100</item>
      </item>
      <item>
        <item>9330</item>
        <item>10500</item>
      </item>
      <item>
        <item>12372</item>
        <item>13600</item>
      </item>
    </api_get_exp_lvup>
    <api_lost_flag>
      <item>-1</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
    </api_lost_flag>
    <api_enemy_info api_level="""" api_rank="""" api_deck_name=""敵前衛部隊"" />
    <api_get_flag>
      <item>0</item>
      <item>1</item>
      <item>0</item>
    </api_get_flag>
    <api_get_ship api_ship_id=""32"" api_ship_type=""駆逐艦"" api_ship_name=""初雪"" api_ship_getmes=""初雪……です……よろしく。"" />
  </api_data>
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
        /// 艦これのデータを試しにパース（要素展開モード）
        /// </summary>
        [TestMethod]
        public void TestComplex5()
        {
            string json = @"
{
  ""api_result"":1,
  ""api_result_msg"":""成功"",
  ""api_data"":{
    ""api_ship_id"":[-1,509,509,502,502,502,-1],
    ""api_win_rank"":""S"",
    ""api_get_exp"":60,
    ""api_mvp"":1,
    ""api_member_lv"":24,
    ""api_member_exp"":29431,
    ""api_get_base_exp"":120,
    ""api_get_ship_exp"":[-1,432,144,144,144,144,144],
    ""api_get_exp_lvup"":[[69933,70300,74100],[2471,2800],[2574,2800],[8773,9100],[9330,10500],[12372,13600]],
    ""api_dests"":5,
    ""api_destsf"":1,
    ""api_lost_flag"":[-1,0,0,0,0,0,0],
    ""api_quest_name"":""カムラン半島"",
    ""api_quest_level"":3,
    ""api_enemy_info"":{
      ""api_level"":"""",
      ""api_rank"":"""",
      ""api_deck_name"":""敵前衛部隊""
    },
    ""api_first_clear"":0,
    ""api_get_flag"":[0,1,0],
    ""api_get_ship"":{
      ""api_ship_id"":32,
      ""api_ship_type"":""駆逐艦"",
      ""api_ship_name"":""初雪"",
      ""api_ship_getmes"":""初雪……です……よろしく。""
    },
    ""api_get_eventflag"":0
  }
}";
            string xml = @"
<root>
  <api_result>1</api_result>
  <api_result_msg>成功</api_result_msg>
  <api_data>
    <api_ship_id>
      <item>-1</item>
      <item>509</item>
      <item>509</item>
      <item>502</item>
      <item>502</item>
      <item>502</item>
      <item>-1</item>
    </api_ship_id>
    <api_win_rank>S</api_win_rank>
    <api_get_exp>60</api_get_exp>
    <api_mvp>1</api_mvp>
    <api_member_lv>24</api_member_lv>
    <api_member_exp>29431</api_member_exp>
    <api_get_base_exp>120</api_get_base_exp>
    <api_get_ship_exp>
      <item>-1</item>
      <item>432</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
      <item>144</item>
    </api_get_ship_exp>
    <api_get_exp_lvup>
      <item>
        <item>69933</item>
        <item>70300</item>
        <item>74100</item>
      </item>
      <item>
        <item>2471</item>
        <item>2800</item>
      </item>
      <item>
        <item>2574</item>
        <item>2800</item>
      </item>
      <item>
        <item>8773</item>
        <item>9100</item>
      </item>
      <item>
        <item>9330</item>
        <item>10500</item>
      </item>
      <item>
        <item>12372</item>
        <item>13600</item>
      </item>
    </api_get_exp_lvup>
    <api_dests>5</api_dests>
    <api_destsf>1</api_destsf>
    <api_lost_flag>
      <item>-1</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
      <item>0</item>
    </api_lost_flag>
    <api_quest_name>カムラン半島</api_quest_name>
    <api_quest_level>3</api_quest_level>
    <api_enemy_info>
      <api_level></api_level>
      <api_rank></api_rank>
      <api_deck_name>敵前衛部隊</api_deck_name>
    </api_enemy_info>
    <api_first_clear>0</api_first_clear>
    <api_get_flag>
      <item>0</item>
      <item>1</item>
      <item>0</item>
    </api_get_flag>
    <api_get_ship>
      <api_ship_id>32</api_ship_id>
      <api_ship_type>駆逐艦</api_ship_type>
      <api_ship_name>初雪</api_ship_name>
      <api_ship_getmes>初雪……です……よろしく。</api_ship_getmes>
    </api_get_ship>
    <api_get_eventflag>0</api_get_eventflag>
  </api_data>
</root>
".Trim();

            var jdoc = JToken.Parse(json);
            var dump = jdoc.Dump();
            Debug.WriteLine(dump);

            var doc = jdoc.ToXDocument(XmlPriority.Element);
            var oxml = doc.ToString();
            Debug.WriteLine(oxml);
            Assert.AreEqual(xml, oxml);
        }
    
    }
}
