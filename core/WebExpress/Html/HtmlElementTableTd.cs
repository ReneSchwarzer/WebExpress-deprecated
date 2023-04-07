﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eine einzelne Tabellenzelle.
    /// </summary>
    public class HtmlElementTableTd : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTableTd()
            : base("td")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="node">The content of the html element.</param>
        public HtmlElementTableTd(IHtmlNode node)
            : this()
        {
            Elements.Add(node);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTableTd(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTableTd(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
