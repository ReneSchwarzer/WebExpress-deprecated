using WebExpress.Html;

namespace WebExpress.Pages
{
    public class UriPathSegmentPage : UriPathSegment
    {
        /// <summary>
        /// Die ID des Segmentese
        /// </summary>
        public UriSegmentID SegmentID { get; set; }

        /// <summary>
        /// Der Anzeigetext
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert</param>
        /// <param name="segmentID">Die SegmentID</param>
        /// <param name="tag">Der Tag</param>
        public UriPathSegmentPage(string value, UriSegmentID segmentID, object tag = null)
            : base(value, tag)
        {
            SegmentID = segmentID;
            Display = Display;
        }
    }
}
