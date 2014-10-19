using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Moonmile.ExHtml
{
    public static class HtmlDocumentExtenstions
    {
        public static XDocument ToXDocument(this HtmlAgilityPack.HtmlDocument doc)
        {
            try
            {
                var xdoc = new XDocument();
                if (doc.DocumentNode.ChildNodes.Count == 1)
                {
                    xdoc.Add(doc.DocumentNode.FirstChild.ToXElement());
                } else
                {
                    foreach ( var it in doc.DocumentNode.ChildNodes) {
                        if (it.ChildNodes.Count > 0)
                        {
                            xdoc.Document.Add(it.ToXElement());
                        }
                    }
                }
                return xdoc;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        public static XElement ToXElement(this HtmlAgilityPack.HtmlNode node)
        {
            if (node.Name.StartsWith("#"))
                return new XElement(node.Name.Replace("#", "_"));

            var el = new XElement(node.Name);
            foreach (var it in node.Attributes)
            {
                el.SetAttributeValue(
                    it.Name, it.Value);
            }
            foreach (var it in node.ChildNodes)
            {
                if (it.Name == "#text")
                {
                    el.Add(new XText(it.InnerText));
                }
                else
                {
                    el.Add(it.ToXElement());
                }
            }
            return el;
        }
    }
}
