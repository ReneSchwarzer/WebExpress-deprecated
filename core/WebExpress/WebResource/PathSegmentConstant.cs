using WebExpress.Module;
using WebExpress.Internationalization;
using System.Globalization;

namespace WebExpress.WebResource
{
    public class PathSegmentConstant : IPathSegment
    {
        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Segment { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring
        /// </summary>
        public string Display { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="segment">Der Wert</param>
        /// <param name="display">Der Anzeigestring</param>
        public PathSegmentConstant(string segment, string display)
        {
            Segment = segment;
            Display = display;
        }

        /// <summary>
        /// Liefert den Anzeigenamen
        /// </summary>
        /// <param name="segment">Das Segemnt</param>
        /// <param name="pluginID">Die PlugiinID</param>
        /// <param name="culture">Die Kultur</param>
        /// <returns>Der Anzeigestring zum Segment</returns>
        public string GetDisplay(string segment, string pluginID, CultureInfo culture)
        {
            return InternationalizationManager.I18N(culture, pluginID, Display);
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Das Pfadsegment in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return Segment;
        }
    }
}
