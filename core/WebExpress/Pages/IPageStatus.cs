namespace WebExpress.Pages
{
    /// <summary>
    /// Statusseite
    /// </summary>
    public interface IPageStatus : IPage
    {
        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        string StatusMessage { get; set; }
    }
}
