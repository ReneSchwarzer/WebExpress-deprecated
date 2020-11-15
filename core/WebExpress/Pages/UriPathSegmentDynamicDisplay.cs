using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebExpress.Pages
{
    /// <summary>
    /// Anzeigeklasse für dynamische Pfadseperatoren
    /// </summary>
    public class UriPathSegmentDynamicDisplay
    {
        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public List<IUriPathSegmentDynamicDisplayItem> Items { get; protected set; } = new List<IUriPathSegmentDynamicDisplayItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public UriPathSegmentDynamicDisplay()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="display">Das zu kopierende Objekt</param>
        public UriPathSegmentDynamicDisplay(UriPathSegmentDynamicDisplay display)
        {
            Items = display.Items.ToList();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Items</param>
        public UriPathSegmentDynamicDisplay(params IUriPathSegmentDynamicDisplayItem[] items)
        {
            Items = items.ToList();
        }

        /// <summary>
        /// Konvertierung in String
        /// </summary>
        /// <param name="parameter">Die Parameter</param>
        /// <returns>Der String</returns>
        public virtual string ToString(Dictionary<string, string> parameter)
        {
            var builder = new StringBuilder();

            foreach (var item in Items)
            {
                if (item is UriPathSegmentDynamicDisplayText text)
                {
                    builder.Append(text);
                }
                else if (item is UriPathSegmentDynamicDisplayReference reference)
                {
                    if (parameter.ContainsKey(reference.Name.ToLower()))
                    {
                        var value = parameter[reference.Name.ToLower()];
                        builder.Append(value);
                    }
                }
            }

            return builder.ToString();
        }
    }
}