using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    public interface ISegmentAttribute
    {
        /// <summary>
        /// Umwandlung in einem Pfadsegment
        /// </summary>
        /// <returns>Das Pfadsegment</returns>
        IPathSegment ToPathSegment();
    }
}
