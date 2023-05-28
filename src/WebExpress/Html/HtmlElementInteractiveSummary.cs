﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eineZusammenfassung oder eineLegende für ein bestimmte <details>-Element.
    /// </summary>
    public class HtmlElementInteractiveSummary : HtmlElement, IHtmlElementInteractive
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementInteractiveSummary()
            : base("summary")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementInteractiveSummary(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementInteractiveSummary(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
