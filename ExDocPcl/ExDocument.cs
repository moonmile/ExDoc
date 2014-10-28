using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Moonmile.ExDoc
{
    public class ExDocument
    {
        public XDocument _doc;
        public ExElement Root { get; internal set; }
        bool _deep = false;


        /// <summary>
        /// 空の要素（NULL 回避用に使う）
        /// </summary>
        protected static internal ExElement EmptyElement = new ExElement();
        protected static internal ExAttr EmptyAttr = new ExAttr();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExDocument()
        {
            this.Root = ExDocument.EmptyElement;
        }

        /// <summary>
        /// XML文字列でロードする
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static ExDocument LoadXml(string xml)
        {
            var sr = new StringReader(xml);
            var doc = XDocument.Load(sr);
            var edoc = new ExDocument();
            edoc._doc = doc;
            edoc.Root = new ExElement();
            edoc.Root._el = doc.Root;
            return edoc;
        }

        public static ExDocument Load(XDocument xdoc)
        {
            var edoc = new ExDocument();
            edoc._doc = xdoc;
            edoc.Root = new ExElement();
            edoc.Root._el = xdoc.Root;
            return edoc;
        }

        public static ExDocument Load(XElement xel)
        {
            var edoc = new ExDocument();
            edoc._doc = null;
            edoc.Root._el = xel;
            return edoc;
        }

        public ExElement Document
        {
            get { return this.Root; }
            internal set { this.Root = value; }
        }


        /// <summary>
        /// 子孫ノードから抽出する
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public ExElements SelectNodes(string tagName, bool deep = false)
        {
            // 初期化
            this.Root.Nodes = null; 
            return this.Root.SelectNodes(tagName, deep);
        }

        public static ExElements operator /(ExDocument doc, string tagName)
        {
            return doc.SelectNodes(tagName, false);
        }
        public static ExElements operator *(ExDocument doc, string tagName)
        {
            return doc.SelectNodes(tagName, true);
        }
        public ExElements this[string tagName]
        {
            get
            {
                if (_deep == false)
                {
                    if (this.Root.Name == tagName)
                    {
                        // 一度検索してリストを返す
                        this.Root.SelectNodes("x");
                        return this.Root.Nodes;
                    }
                    else
                    {
                        return new ExElements();
                    }
                }
                else
                {
                    var res = this.SelectNodes(tagName, true);
                    _deep = false;
                    return res;
                }
            }
            set
            {
                var res = this.SelectNodes(tagName, _deep);
                _deep = false;
                foreach (var it in res)
                {
                    it._el.Value = value;
                }
            }

        }
        public ExElements this[string name, string v]
        {
            get
            {
                var res = this.SelectNodes(name, _deep)
                    .Where(x => x.Value == v)
                    .ToList<ExElement>();
                _deep = false;
                var lst = new ExElements();
                lst.AddRange(res);
                return lst;
            }
            set
            {
                var res = this.SelectNodes(name, _deep)
                    .Where(x => x.Value == v)
                    .ToList<ExElement>();
                _deep = false;
                foreach (var it in res)
                {
                    it._el.Value = value;
                }
            }
        }
        public ExDocument _
        {
            get
            {
                _deep = true;
                return this;
            }
        }
    }
}
