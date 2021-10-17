using System.Globalization;

namespace WebExpress.Internationalization
{
    public interface II18N
    {
        /// <summary>
        /// Liefert oder setzt die Kultur
        /// </summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Liefert den I18N-Key
        /// </summary>
        string I18N_PluginID { get; }
    }
}
