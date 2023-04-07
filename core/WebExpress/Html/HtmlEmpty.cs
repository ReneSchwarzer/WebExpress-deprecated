﻿using System.Text;

namespace WebExpress.Html
{
    public class HtmlEmpty : IHtmlNode
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlEmpty()
        {

        }

        /// <summary>
        /// Convert to a string using a StringBuilder.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="deep">The call depth.</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Value);
        }
    }
}
