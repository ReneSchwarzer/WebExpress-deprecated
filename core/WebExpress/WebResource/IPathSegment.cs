using System.Globalization;

namespace WebExpress.WebResource
{
    public interface IPathSegment
    {
        /// <summary>
        /// Liefert den Anzeigenamen
        /// </summary>
        /// <param name="segment">Das Segemnt</param>
        /// <param name="pluginID">Die PlugiinID</param>
        /// <param name="culture">Die Kultur</param>
        /// <returns>Der Anzeigestring zum Segment</returns>
        string GetDisplay(string segment, string pluginID, CultureInfo culture);
    }
}
