using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public interface IControl
    {
        /// <summary>
        /// Liefert Returns or sets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Liefert oder setzt die Css-Klasse
        /// </summary>
        List<string> Classes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Css-Style
        /// </summary>
        List<string> Styles { get; set; }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        IHtmlNode Render(RenderContext context);
    }
}
