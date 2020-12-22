using System.Collections.Generic;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public interface IControl
    {
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        IHtmlNode Render(RenderContext context);
    }
}
