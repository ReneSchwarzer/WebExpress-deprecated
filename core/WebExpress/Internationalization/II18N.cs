using System.Globalization;

namespace WebExpress.Internationalization
{
    public interface II18N
    {
        /// <summary>
        /// Liefert oder setzt die Kultur
        /// </summary>
        CultureInfo Culture { get; set; }
    }
}
