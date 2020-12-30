using WebExpress.Uri;

namespace WebExpress.WebResource
{
    /// <summary>
    /// Statusseite
    /// </summary>
    public interface IPageStatus : IPage
    {
        /// <summary>
        /// Liefert oder setzt den Statuscode
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        string StatusTitle { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        string StatusMessage { get; set; }

        /// <summary>
        /// Liefert oder setzt das Statusicon
        /// </summary>
        IUri StatusIcon { get; set; }
    }
}
