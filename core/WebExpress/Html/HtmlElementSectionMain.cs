﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert den Hauptinhalt der Seite. Es ist nur ein <main> Element pro Seite zulässig.
    /// </summary>
    public class HtmlElementSectionMain : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementSectionMain()
            : base("main")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementSectionMain(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementSectionMain(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
