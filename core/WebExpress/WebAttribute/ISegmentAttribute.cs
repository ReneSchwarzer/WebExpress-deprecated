using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    public interface ISegmentAttribute
    {
        /// <summary>
        /// Conversion to a path segment.
        /// </summary>
        /// <returns>The path segment.</returns>
        IPathSegment ToPathSegment();
    }
}
