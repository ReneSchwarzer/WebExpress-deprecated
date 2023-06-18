using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Grid aus 12-Zellen pro Zeile
    /// </summary>
    public class ControlPanelGrid : ControlPanel
    {


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelGrid(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            html.Elements.Add(new HtmlElementTextContentDiv(Content.Select(x => x.Render(context))) { Class = "row" });

            return html;
        }
    }
}
