using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace Moonmile.ExDoc.Json
{
    public enum XmlPriority
    {
        Attribute,
        Element
    }
    public static class YamlDocumentExtensions
    {
        public static XDocument ToXDocument(this JToken jdoc, XmlPriority pri = XmlPriority.Attribute)
        {
            var doc = new XDocument();
            doc.Document.Add(new XElement("root"));
            doc.Root.AddJsonNode(jdoc, pri);
            return doc;
        }

        /// <summary>
        /// 子ノードを追加する
        /// </summary>
        /// <param name="pa"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static XElement AddJsonNode(this XElement pa, JToken token,
            XmlPriority pri = XmlPriority.Attribute)
        {
            if (token is JProperty)
            {
                // to XElement
                var prop = token as JProperty;
                if (prop.Value is JValue)
                {
                    // 親要素に属性として追加する
                    var v = prop.Value as JValue;
                    if (pri == XmlPriority.Attribute || prop.Name.StartsWith("@"))
                    {
                        // 属性優先の場合
                        pa.SetAttributeValue(
                            prop.Name.Replace("@", ""),
                            v.Value.ToString());
                    }
                    else
                    {
                        // 要素優先の場合
                        var el = new XElement(prop.Name);
                        el.Value = v.Value.ToString();
                        pa.Add(el);
                    }
                }
                else
                {
                    // 子要素として追加する
                    var el = new XElement(prop.Name);
                    pa.Add(el);
                    el.AddJsonNode(prop.Value, pri);
                }
            }
            else
            {
                bool d = false;
                // 親要素に追加する
                foreach (var it in token.Children())
                {
                    if (it is JValue)
                    {
                        var el = new XElement(pa.Name);
                        d = true;
                        pa.Parent.Add(el);
                        el.Value = (it as JValue).Value.ToString();
                    }
                    else
                    {
                        if (token is JObject)
                        {
                            pa.AddJsonNode(it, pri);
                        }
                        else
                        {
                            var el = new XElement(pa.Name);
                            d = true;
                            pa.Parent.Add(el);
                            el.AddJsonNode(it, pri);
                        }
                    }
                }
                if (d == true)
                {
                    // 重複を消す
                    pa.Remove();
                }
            }
            return pa;
        }

        /// <summary>
        /// 内部調査のためのダンプ
        /// </summary>
        /// <param name="ydoc"></param>
        /// <returns></returns>
        public static string Dump(this JToken jdoc)
        {
            string s = "";
            s += string.Format("root: {0}\n", jdoc.Path);
            s += jdoc.Dump(1);
            return s;
        }
        public static string Dump(this JToken token, int indent)
        {
            string s = "";
            if (token is JProperty)
            {
                // to XElement
                var prop = token as JProperty;
                if (prop.Value is JValue)
                {
                    // to Attribute
                    var v = prop.Value as JValue;
                    s += string.Format("{0}prop: {1} value:{2}\n",
                         new string(' ', indent), prop.Name, v.Value);
                }
                else
                {
                    // to Children
                    s += string.Format("{0}prop: {1}\n",
                         new string(' ', indent), prop.Name);
                    s += prop.Value.Dump(indent + 1);
                }
            }
            else
            {
                // to List
                foreach (var it in token.Children())
                {
                    if (it is JValue)
                    {
                        s += string.Format("{0}value:{2}\n",
                            new string(' ', indent),
                            it.Path,
                            (it as JValue).Value);
                    }
                    else
                    {
                        s += it.Dump(indent + 1);
                    }
                }
            }
            return s;
        }
    }
}
