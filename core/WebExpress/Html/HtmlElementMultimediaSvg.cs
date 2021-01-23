using System;
using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert eine eingebettete Vektorgrafik.
    /// </summary>
    public class HtmlElementMultimediaSvg : HtmlElement, IHtmlElementMultimedia
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Width
        {
            get => Convert.ToInt32(GetAttribute("width"));
            set => SetAttribute("width", value.ToString());
        }

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Height
        {
            get => Convert.ToInt32(GetAttribute("height"));
            set => SetAttribute("height", value.ToString());
        }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public string Target
        {
            get => GetAttribute("target");
            set => SetAttribute("target", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMultimediaSvg()
            : base("svg")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementMultimediaSvg(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementMultimediaSvg(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
