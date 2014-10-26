using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpYaml;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using SharpYaml.Serialization;


namespace Moonmile.ExYaml
{
    public static class YamlDocumentExtensions
    {
        public static XDocument ToXDocument(this YamlDocument ydoc)
        {
            var doc = new XDocument();

            if (ydoc.RootNode is YamlMappingNode)
            {
                var map = ydoc.RootNode as YamlMappingNode;
                if (map.Children.Count == 1)
                {
                    // ルートがひとつの場合
                    var node = map.GetFirst();
                    var name = (node.Item1 as YamlScalarNode).Value;
                    doc.Document.Add(new XElement(name));
                    doc.Root.AddYamlNode(node.Item2);
                }
                else
                {
                    // map が複数の場合は root 要素を追加する
                    doc.Document.Add(new XElement("root"));
                    doc.Root.AddYamlNode(map);
                }
            }
            else if (ydoc.RootNode is YamlSequenceNode)
            {
                var seq = ydoc.RootNode as YamlSequenceNode;
                if (seq.Children.Count == 1)
                {
                    // ルートがひとつの場合
                    var node = seq.Children[0];
                    if (node is YamlMappingNode)
                    {
                        var map = node as YamlMappingNode;
                        var name = (map.GetFirst().Item1 as YamlScalarNode).Value;
                        doc.Document.Add(new XElement(name));
                        doc.Root.AddYamlNode(node);
                        // 属性値を要素の値に入れ替え
                        var attr = doc.Root.Attribute(name);
                        if (attr != null)
                        {
                            doc.Root.Value = attr.Value;
                            doc.Root.SetAttributeValue(name, null);
                        }
                    }
                }
                else
                {
                    // seq が複数の場合は root 要素を追加する
                    doc.Document.Add(new XElement("root"));
                    doc.Root.AddYamlNode(seq);
                }
            }
            return doc;
        }

        /// <summary>
        /// 子ノードを追加する
        /// </summary>
        /// <param name="pa"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static XElement AddYamlNode(this XElement pa, YamlNode node)
        {
            if (node is YamlScalarNode)
            {
                pa.Value = (node as YamlScalarNode).Value;
            }
            else if (node is YamlMappingNode)
            {
                var map = node as YamlMappingNode;
                // 親ノードに属性として追加する
                foreach (var it in map)
                {
                    var name = (it.Key as YamlScalarNode).Value;
                    if (it.Value is YamlScalarNode)
                    {
                        var value = (it.Value as YamlScalarNode).Value;
                        pa.SetAttributeValue(name, value);
                    }
                    else
                    {
                        // 要素に昇格させる
                        var el = new XElement(name);
                        pa.Add(el);
                        el.AddYamlNode(it.Value);
                    }
                }
            }
            else if (node is YamlSequenceNode)
            {
                var seq = node as YamlSequenceNode;
                // 親ノードに要素として追加する
                foreach (var it in seq)
                {
                    var head = (it as YamlMappingNode).GetFirst();
                    var name = (head.Item1 as YamlScalarNode).Value;
                    var el = new XElement(name);
                    pa.Add(el);
                    el.AddYamlNode(it);
                    // 属性値を要素の値に入れ替え
                    var attr = el.Attribute(name);
                    if (attr != null)
                    {
                        el.Value = attr.Value;
                        el.SetAttributeValue(name, null);
                    }
                    // 要素のネストを浅くする
                    if (el.FirstNode is XElement)
                    {
                        if (name == ((XElement)el.FirstNode).Name)
                        {
                            el.Remove();
                            pa.Add(el.FirstNode);
                        }
                    }
                }
            }
            return pa;
        }

        static Tuple<YamlNode, YamlNode> GetFirst(this YamlMappingNode node)
        {
            var it = node.Children.GetEnumerator();
            it.MoveNext();
            return new Tuple<YamlNode, YamlNode>(it.Current.Key, it.Current.Value);

        }

        /// <summary>
        /// 内部調査のためのダンプ
        /// </summary>
        /// <param name="ydoc"></param>
        /// <returns></returns>
        public static string Dump(this YamlDocument ydoc)
        {
            return ydoc.RootNode.Dump();
        }
        public static string Dump(this YamlNode ynode, int indent = 0)
        {
            if (ynode is YamlScalarNode)
            {
                var node = ynode as YamlScalarNode;
                return string.Format("{0}value: {1}\n", new string(' ', indent), node.Value);
            }
            else if (ynode is YamlSequenceNode)
            {
                var seq = ynode as YamlSequenceNode;
                var s = string.Format("{0}seq: \n", new string(' ', indent));
                foreach (var it in seq.Children)
                {
                    s += it.Dump(indent + 1);
                }
                return s;
            }
            else if (ynode is YamlMappingNode)
            {
                var map = ynode as YamlMappingNode;
                var s = string.Format("{0}map: \n", new string(' ', indent));
                foreach (var it in map.Children)
                {
                    s += string.Format("{0}key: {1} \n", new string(' ', indent), it.Key);
                    s += it.Value.Dump(indent + 1);
                }
                return s;
            }
            return "error\n";
        }
    }
}
