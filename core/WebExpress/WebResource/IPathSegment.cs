using System.Globalization;

namespace WebExpress.WebResource
{
    public interface IPathSegment
    {
        /// <summary>
        /// Returns the display name.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="pluginID">The plugin id.</param>
        /// <param name="culture">The Culture.</param>
        /// <returns>The display string to the segment.</returns>
        string GetDisplay(string segment, string pluginID, CultureInfo culture);
    }
}
