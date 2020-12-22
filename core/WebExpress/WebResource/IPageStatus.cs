namespace WebExpress.WebResource
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
