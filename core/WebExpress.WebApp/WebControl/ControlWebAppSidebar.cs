using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Sidebar für eine WenApp
    /// </summary>
    public class ControlWebAppSidebar : Control
    {
        /// <summary>
        /// Liefert oder setzt den Bereich für die Kopfzeile der Sidebar
        /// </summary>
        public List<IControl> Header { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für Präferenzen
        /// </summary>
        public List<IControl> Preferences { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den Primärbereich für die Steuerelemente
        /// </summary>
        public List<IControl> Primary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den sekundären Bereich für die Steuerelemente
        /// </summary>
        public List<IControl> Secondary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Bestimmt, ob Content vorhanden ist
        /// </summary>
        public bool HasContent => Header.Any() || Preferences.Any() || Primary.Any() || Secondary.Any();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppSidebar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.SidebarBackground;
            //Height = TypeHeight.OneHundred;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (!HasContent)
            {
                return null;
            }

            var elements = new List<IHtmlNode>();
            elements.AddRange(Header.Select(x => x.Render(context)));
            elements.AddRange(Preferences.Select(x => x.Render(context)));
            elements.AddRange(Primary.Select(x => x.Render(context)));
            elements.AddRange(Secondary.Select(x => x.Render(context)));

            return new HtmlElementTextContentDiv(elements)
            {
                ID = Id,
                Class = Css.Concatenate("sidebar", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
