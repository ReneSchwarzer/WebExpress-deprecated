using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Properties für eine WebApp
    /// </summary>
    public class ControlWebAppProperty : Control
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
        public ControlWebAppProperty(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.PropertyBackground;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Preferences.Count == 0 && Primary.Count == 0 && Secondary.Count == 0)
            {
                return null;
            }

            var preferences = new HtmlElementTextContentDiv(Preferences.Select(x => x.Render(context)));
            var primary = new HtmlElementTextContentDiv(Primary.Select(x => x.Render(context)));
            var secondary = new HtmlElementTextContentDiv(Secondary.Select(x => x.Render(context)));

            return new HtmlElementTextContentDiv(preferences, primary, secondary)
            {
                ID = ID,
                Class = Css.Concatenate("proterty", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
