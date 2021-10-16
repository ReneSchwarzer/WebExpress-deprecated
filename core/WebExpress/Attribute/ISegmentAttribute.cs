using WebExpress.WebResource;

namespace WebExpress.Attribute
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
