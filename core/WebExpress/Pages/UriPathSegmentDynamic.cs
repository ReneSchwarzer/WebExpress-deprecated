using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Pages
{
    /// <summary>
    /// Variables Pfadsegment
    /// </summary>
    public class UriPathSegmentDynamic
    {
        /// <summary>
        /// Liefert oder setzt den Anzeigetext
        /// </summary>
        public UriPathSegmentDynamicDisplay Display { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Trenner
        /// </summary>
        public string Seperator { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Items
        /// </summary>
        public List<UriPathSegmentVariable> Items { get; protected set; } = new List<UriPathSegmentVariable>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="display">Der Anzeigetext</param>
        /// <param name="items">Die Items</param>
        public UriPathSegmentDynamic(UriPathSegmentDynamicDisplay display, params UriPathSegmentVariable[] items)
            : this(display, null, items)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="display">Der Anzeigetext</param>
        /// <param name="seperator">Der Trenner</param>
        /// <param name="items">Die Items</param>
        public UriPathSegmentDynamic(UriPathSegmentDynamicDisplay display, string seperator, params UriPathSegmentVariable[] items)
        {
            Display = display;
            Seperator = seperator;
            Items = items.ToList();
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="segment">Das zu kopierende Segment</param>
        public UriPathSegmentDynamic(UriPathSegmentDynamic segment)
        {
            Display = new UriPathSegmentDynamicDisplay(segment.Display);
            Seperator = segment.Seperator;
            Items = segment.Items.ToList();
        }
    }
}