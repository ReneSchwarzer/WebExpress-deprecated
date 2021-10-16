using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Footer für eine WebApp
    /// </summary>
    public class ControlWebAppFooter : Control
    {
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppFooter(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.FooterBackground;
            TextColor = LayoutSchema.FooterText;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var elements = new List<IHtmlNode>
            {
                new HtmlElementTextContentDiv(Preferences.Select(x => x.Render(context))),
                new HtmlElementTextContentDiv(Primary.Select(x => x.Render(context))) { Class = "justify-content-center" },
                new HtmlElementTextContentDiv(Secondary.Select(x => x.Render(context)))
            };

            return new HtmlElementTextContentDiv(elements)
            {
                ID = ID,
                Class = Css.Concatenate("footer d-flex justify-content-between align-items-stretch", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
