using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlSplitButtonItemDivider : Control, IControlSplitButtonItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlSplitButtonItemDivider(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = Css.Concatenate("dropdown-divider", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
