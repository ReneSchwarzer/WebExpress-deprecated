using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementLink : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt die Url
        /// </summary>
        public string Href
        {
            get { return GetAttribute("href"); }
            set { SetAttribute("href", value); }
        }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public string Rel
        {
            get { return GetAttribute("rel"); }
            set { SetAttribute("rel", value); }
        }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementLink()
            : base("link", false)
        {
        }
    }
}
