﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet einen Textteil mit Ruby-Annotationen. Dies sind kurze Aussprachetipps und andere Hinweise, die hauptsächlich für ostasiatische Typografie verwendet werden.
    /// </summary>
    public class HtmlElementTextSemanticsRuby : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Returns the elements.
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTextSemanticsRuby()
            : base("ruby")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTextSemanticsRuby(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementTextSemanticsRuby(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
