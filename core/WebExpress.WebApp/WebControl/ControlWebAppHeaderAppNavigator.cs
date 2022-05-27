using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeaderAppNavigator : Control
    {
        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> Preferences { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> Primary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> Secondary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeaderAppNavigator(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var hamburger = new List<IControlDropdownItem>
            {
                new ControlDropdownItemHeader() { Text = context.I18N(context.Application, context.Application.ApplicationName) }
            };

            hamburger.AddRange(Primary);

            if (Primary.Count > 0 && Secondary.Count > 0)
            {
                hamburger.Add(new ControlDropdownItemDivider());
            }

            hamburger.AddRange(Secondary);

            var logo = (hamburger.Count > 1) ?
            (IControl)new ControlDropdown("webexpress.webapp.header.icon", hamburger)
            {
                Image = context.Application.Icon,
                Height = 50,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None),
                Styles = new List<string>() { "padding: 0.5em;" }
            } :
            new ControlImage("webexpress.webapp.header.icon")
            {
                Uri = context.Application.Icon,
                Height = 50,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
            };

            return logo?.Render(context);
        }
    }
}
