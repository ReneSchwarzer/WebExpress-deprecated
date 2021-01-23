using System;
using WebExpress.Internationalization;
using WebExpress.WebResource;

namespace WebExpress.Attribute
{
    public class SegmentAttribute : System.Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// Liefert oder setzt das Segment
        /// </summary>
        private string Segment { get; set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring
        /// </summary>
        private string Display { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="segment">Das Segment des Uri-Pfades</param>
        /// <param name="display">Der Anzeigestring</param>
        public SegmentAttribute(string segment, string display = null)
        {
            Segment = segment;
            Display = display;
        }

        /// <summary>
        /// Umwandlung in ein Pfadsegment
        /// </summary>
        /// <returns>Das Pfadsegment</returns>
        public IPathSegment ToPathSegment()
        {
            return new PathSegmentConstant(Segment, Display) {  };
        }
    }
}
