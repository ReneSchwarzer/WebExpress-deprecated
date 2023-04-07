﻿using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet die Beschriftung für ein Formular-Kontrollelement (z.B. Texteingabefelder).
    /// <label for="vorname">Vorname:</label> 
    /// <input type="text" name="vorname" id="vorname" maxlength="30">
    /// </summary>
    public class HtmlElementFieldLabel : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Returns or sets the name of the input field.
        /// </summary>
        public string For
        {
            get => GetAttribute("for");
            set => SetAttribute("for", value);
        }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text
        {
            get => string.Join(string.Empty, Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.RemoveAll(x => x is HtmlText); Elements.Insert(0, new HtmlText(value)); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementFieldLabel()
            : base("label")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">The content of the html element.</param>
        public HtmlElementFieldLabel(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementFieldLabel(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Convert to a string using a StringBuilder.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="deep">The call depth.</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
