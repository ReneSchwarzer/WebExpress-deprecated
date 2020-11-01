using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelMain : ControlPanel
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelMain(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementSectionMain()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };
            html.Elements.AddRange(from x in Content select x.Render(context));

            return html;
        }
    }
}
