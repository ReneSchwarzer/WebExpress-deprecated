using System;
using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    public class SegmentAttribute : Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// Returns or set the segment of the uri path.
        /// </summary>
        private string Segment { get; set; }

        /// <summary>
        /// Returns or sets the display string.
        /// </summary>
        private string Display { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="segment">The segment of the uri path.</param>
        /// <param name="display">The display string.</param>
        public SegmentAttribute(string segment, string display = null)
        {
            Segment = segment;
            Display = display;
        }

        /// <summary>
        /// Conversion to a path segment.
        /// </summary>
        /// <returns>The path segment.</returns>
        public IPathSegment ToPathSegment()
        {
            return new PathSegmentConstant(Segment, Display) { };
        }
    }
}
