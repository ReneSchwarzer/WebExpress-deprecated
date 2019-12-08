using System.Collections.Generic;
using System.Text;

namespace WebServer.Html
{
    public class HtmlElementForm : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        public string Name
        {
            get => GetAttribute("name");
            set => SetAttribute("name", value);
        }

        /// <summary>
        /// Liefert oder setzt die Zeichenkodierung
        /// </summary>
        public string AcceptCharset
        {
            get => GetAttribute("accept-charset");
            set => SetAttribute("accept-charset", value);
        }

        /// <summary>
        /// Liefert oder setzt den Methode Post oder get
        /// </summary>
        public string Method
        {
            get => GetAttribute("method");
            set => SetAttribute("method", value);
        }

        /// <summary>
        /// Liefert oder setzt die URL 
        /// </summary>
        public string Action
        {
            get => GetAttribute("action");
            set => SetAttribute("action", value);
        }

        /// <summary>
        /// Liefert oder setzt das Zielfenster
        /// </summary>
        public string Target
        {
            get => GetAttribute("target");
            set => SetAttribute("target", value);
        }

        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

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
