using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für ein Kontrollelement zur Erzeugung einesPaares aus öffentlichem und privaten Schlüssel und zum Versenden des öffentlichen Schlüssels.
    /// </summary>
    public class HtmlElementFormKeygen : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFormKeygen()
            : base("keygen")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFormKeygen(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
