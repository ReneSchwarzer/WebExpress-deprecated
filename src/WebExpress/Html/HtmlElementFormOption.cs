﻿using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Auswahloption innerhalb eines <select>-Elements, oder einen Vorschlag innerhalb eines <datalist>-Elements.
    /// <select name="top5" size="5">
    ///  <option>Michael Jackson</option>
    ///  <option selected>Tom Waits</option>
    /// </select>
    /// </summary>
    public class HtmlElementFormOption : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Returns or sets a value.
        /// </summary>
        public string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        /// <summary>
        /// Liefert oder setzt ob das Felf ausgewählt ist
        /// </summary>
        public bool Selected
        {
            get => HasAttribute("selected");
            set { if (value) { SetAttribute("selected"); } else { RemoveAttribute("selected"); } }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementFormOption()
            : base("option")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">The content of the html element.</param>
        public HtmlElementFormOption(params IHtmlNode[] nodes)
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
