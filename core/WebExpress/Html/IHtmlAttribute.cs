namespace WebExpress.Html
{
    public interface IHtmlAttribute : IHtml
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Attributes
        /// </summary>
        string Name { get; set; }

    }
}
