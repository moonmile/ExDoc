using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moonmile.ExDoc
{
    public class ExElement
    {
        public ExElements Nodes { get; internal set; }
        internal XElement _el = null;

        public XElement XElement { get { return _el; } }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExElement()
        {
        }

        public string Name
        {
            get { return _el == null ? "" : _el.Name.ToString(); }
        }
        /// <summary>
        /// 最初のテキストノードの値を返す。それ以外は空白
        /// </summary>
        public string Value
        {

            get
            {
                if (_el == null)
                    return "";
                if (_el.FirstNode is XText)
                    return ((XText)_el.FirstNode).Value;
                return "";
            }
        }

        /// <summary>
        /// マッチする子ノードを取得する
        /// </summary>
        /// <param name="el"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public ExElements SelectNodes(string tagName, bool deep = false)
        {
            // 最初は子ノードを追加しておく
            if (((object)this.Nodes) == null)
            {
                this.Nodes = new ExElements();
                if (this._el != null)
                {
                    foreach (var nd in this._el.Nodes())
                    {
                        var e = new ExElement();
                        if (nd is XElement)
                        {
                            e._el = (XElement)nd;
                            this.Nodes.Add(e);
                        }
                    }
                }
            }
            return this.Nodes.SelectNodes(tagName, deep);
        }

        public static ExElements operator /(ExElement el, string tagName)
        {
            return el.SelectNodes(tagName);
        }
        public static ExElements operator *(ExElement el, string tagName)
        {
            return el.SelectNodes(tagName, true);
        }

        /// <summary>
        /// 値でマッチする子ノードを取得する
        /// </summary>
        /// <param name="el"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public ExElements SelectNodesByValue(string value, bool deep = false, bool reverse = false)
        {
            // 最初は子ノードを追加しておく
            if (((object)this.Nodes) == null)
            {
                this.Nodes = new ExElements();
                foreach (var nd in this._el.Nodes())
                {
                    var e = new ExElement();
                    if (nd is XElement)
                    {
                        e._el = (XElement)nd;
                        this.Nodes.Add(e);
                    }
                }
            }
            return this.Nodes.SelectNodesByValue(value, deep, reverse);
        }

        /// <summary>
        /// 単一の要素にキャストする
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static implicit operator ExElement(ExAttrs attrs)
        {
            if (attrs.Count == 0)
            {
                return ExDocument.EmptyElement;
            }
            else
            {
                return attrs.First().Parent;
            }
        }

        /// <summary>
        /// 空要素のチェック
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
        {
            get
            {
                return this.Equals(ExDocument.EmptyElement);
            }
        }

        /// <summary>
        /// 親ノードを取得
        /// </summary>
        public ExElement Parent
        {
            get
            {
                if (this.IsEmpty)
                {
                    return ExDocument.EmptyElement;
                }
                else
                {
                    if (_el.Parent == null)
                    {
                        return ExDocument.EmptyElement;
                    }
                    else
                    {
                        var el = new ExElement() { _el = this._el.Parent };
                        return el;
                    }
                }
            }
        }

        public static ExAttr operator %(ExElement el, string name)
        {
            if (el._el.Attribute(name) == null)
            {
                return ExDocument.EmptyAttr;
            }
            else
            {
                var attr = new ExAttr()
                {
                    Name = name,
                    Value = el._el.Attribute(name).Value,
                    Parent = el
                };
                return attr;
            }
        }
    }

    public class ExElements : List<ExElement>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExElements()
        {
        }

        /// <summary>
        /// 子ノードよりタグ名で抽出する
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public ExElements SelectNodes(string tagName, bool deep = false)
        {
            var lst = new ExElements();
            foreach (var el in this)
            {
                if (el.Name == tagName || el._el.Name.LocalName == tagName )
                // if ( el.Name == tagName )
                {
                    lst.Add(el);
                }
                if (deep)
                    lst.AddRange(el.SelectNodes(tagName, true));
            }
            return lst;
        }

        /// <summary>
        /// 子ノードより値で抽出する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="deep"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public ExElements SelectNodesByValue(string value, bool deep = false, bool reverse = false)
        {
            var lst = new ExElements();
            foreach (var el in this)
            {
                if (reverse == false)
                {
                    if (el.Value == value)
                    {
                        lst.Add(el);
                    }
                }
                else
                {
                    if (el.Value != value)
                    {
                        lst.Add(el);
                    }
                }
                if (deep)
                    lst.AddRange(el.SelectNodesByValue(value, true, reverse));
            }
            return lst;
        }

        /// <summary>
        /// 子ノードから抽出
        /// </summary>
        /// <param name="els"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static ExElements operator /(ExElements els, string tagName)
        {
            return els.SelectNodes(tagName, false);
        }

        /// <summary>
        /// 子孫ノードから抽出
        /// </summary>
        /// <param name="els"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static ExElements operator *(ExElements els, string tagName)
        {
            return els.SelectNodes(tagName, true);
        }

        /// <summary>
        /// 単一要素のキャスト（最初の要素を取得）
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static implicit operator ExElement(ExElements lst)
        {
            if (lst.Count == 0)
            {
                return ExDocument.EmptyElement;
            }
            else
            {
                return lst.First();
            }
        }

        /// <summary>
        /// マッチする属性を取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ExAttrs SelectAttrs(string name)
        {
            var lst = new ExAttrs();
            foreach (var el in this)
            {
                if (el._el.HasAttributes == true)
                {
                    var xa = el._el.Attribute(name);
                    if (xa != null)
                    {
                        var attr = new ExAttr() { Name = name, Value = xa.Value, Parent = el };
                        lst.Add(attr);
                    }
                }
            }
            return lst;
        }


        public static ExAttrs operator %(ExElements els, string name)
        {
            return els.SelectAttrs(name);
        }

        public static ExElements operator ==(ExElements els, string value)
        {
            return els.SelectNodesByValue(value, true, false);
        }
        public static ExElements operator !=(ExElements els, string value)
        {
            return els.SelectNodesByValue(value, true, true);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// 複数の属性にキャストする
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static implicit operator ExElements(ExAttrs attrs)
        {
            var lst = new ExElements();
            foreach (var it in attrs)
            {
                lst.Add(it.Parent);
            }
            return lst;
        }

        /// <summary>
        /// 文字列にキャスト
        /// </summary>
        /// <param name="els"></param>
        /// <returns></returns>
        public static implicit operator string(ExElements els)
        {
            if (els.Count == 0)
            {
                return "";
            }
            else
            {
                return els[0].Value;
            }
        }
        /// <summary>
        /// int型へキャスト
        /// </summary>
        /// <param name="els"></param>
        /// <returns></returns>
        public static implicit operator int(ExElements els)
        {
            if (els.Count == 0)
            {
                return 0;
            }
            else
            {
                int ret = 0;
                if (int.TryParse(els[0].Value, out ret) == true)
                {
                    return ret;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// bool型へキャスト
        /// </summary>
        /// <param name="els"></param>
        /// <returns></returns>
        public static implicit operator bool(ExElements els)
        {
            if (els.Count == 0)
            {
                return false;
            }
            else
            {
                if (els[0].Value == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// DateTime型へキャスト
        /// </summary>
        /// <param name="els"></param>
        /// <returns></returns>
        public static implicit operator DateTime(ExElements els)
        {
            DateTime ret = new DateTime(0);
            if (els.Count > 0)
            {
                DateTime.TryParse(els[0].Value, out ret);
            }
            return ret;
        }   
    }
}
