using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public interface IControl
    {
        /// <summary>
        /// Liefert oder setzt die zugehörige Seite
        /// </summary>
        IPage Page { get; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Liefert oder setzt die Css-Klasse
        /// </summary>
        List<string> Classes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Css-Style
        /// </summary>
        List<string> Styles { get; set; }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        IHtmlNode ToHtml();
    }
}
