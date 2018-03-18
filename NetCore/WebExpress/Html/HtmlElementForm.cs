using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementForm : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        public string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
        
        /// <summary>
        /// Liefert oder setzt die Zeichenkodierung
        /// </summary>
        public string AcceptCharset
        {
            get { return GetAttribute("accept-charset"); }
            set { SetAttribute("accept-charset", value); }
        }

        /// <summary>
        /// Liefert oder setzt den Methode Post oder get
        /// </summary>
        public string Method
        {
            get { return GetAttribute("method"); }
            set { SetAttribute("method", value); }
        }

        /// <summary>
        /// Liefert oder setzt die URL 
        /// </summary>
        public string Action
        {
            get { return GetAttribute("action"); }
            set { SetAttribute("action", value); }
        }
        
        /// <summary>
        /// Liefert oder setzt das Zielfenster
        /// </summary>
        public string Target
        {
            get { return GetAttribute("target"); }
            set { SetAttribute("target", value); }
        }

        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements { get { return base.Elements; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementForm()
            : base("form")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementForm(string text)
            : this()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementForm(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
