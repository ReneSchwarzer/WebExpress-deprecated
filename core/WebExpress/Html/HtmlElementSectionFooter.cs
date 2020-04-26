using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert den Fußteil einer Seite oder eines Abschnitts. Er enthält oft Copyright-Hinweise, einen Link auf das Impressum oder Kontaktadressen.
    /// </summary>
    public class HtmlElementSectionFooter : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSectionFooter()
            : base("footer")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementSectionFooter(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionFooter(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionFooter(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
