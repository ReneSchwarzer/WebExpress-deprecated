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
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelMain(IPage page, string id = null)
            : base(page, id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var html = new HtmlElementSectionMain()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
            html.Elements.AddRange(from x in Content select x.ToHtml());

            return html;
        }
    }
}
