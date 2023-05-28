﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert einen hochgestellten Text.
    /// </summary>
    public class HtmlElementTextSemanticsSup : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTextSemanticsSup()
            : base("sup")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTextSemanticsSup(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTextSemanticsSup(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
