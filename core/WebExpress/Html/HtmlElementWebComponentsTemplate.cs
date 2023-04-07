﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet Html, welches nicht gerendert wird
    /// </summary>
    public class HtmlElementWebFragmentsTemplate : HtmlElement, IHtmlElementWebFragments
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementWebFragmentsTemplate()
            : base("template")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementWebFragmentsTemplate(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementWebFragmentsTemplate(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
