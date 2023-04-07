﻿using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Liste von HTML-Elementen
    /// </summary>
    public class HtmlList : IHtmlNode
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public List<IHtmlNode> Elements { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlList()
        {
            Elements = new List<IHtmlNode>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlList(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlList(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Convert to a string using a StringBuilder.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="deep">The call depth.</param>
        /// <param name="nl">Abschlustag auf neuer Zeile beginnen</param>
        public void ToString(StringBuilder builder, int deep)
        {
            foreach (var v in Elements)
            {
                v.ToString(builder, deep);
            }
        }
    }
}
