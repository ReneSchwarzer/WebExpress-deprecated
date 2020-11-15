namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet einen Zeilenumbruch.
    /// </summary>
    public class HtmlElementTextSemanticsBr : HtmlElement, IHtmlElementTextSemantics
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
