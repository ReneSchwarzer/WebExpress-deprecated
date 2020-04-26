using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Tabellenspalte.
    /// </summary>
    public class HtmlElementTableCol : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableCol()
            : base("col")
        {

        }
    }
}
