namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet einenZeilenumbruch.
    /// </summary>
    public class HtmlElementTextSemanticsBr : HtmlElement,IHtmlElementTextSemantics
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsBr()
            : base("br", false)
        {
        }
    }
}
