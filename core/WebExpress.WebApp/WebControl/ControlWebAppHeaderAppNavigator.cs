using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApplication;
using WebExpress.WebComponent;
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
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeaderAppNavigator(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var application = ComponentManager.ApplicationManager.GetApplcation(context.Page.ApplicationContext?.ApplicationID);

            var hamburger = new List<IControlDropdownItem>
            {
                new ControlDropdownItemHeader() { Text = context.I18N(context.Page.ApplicationContext, context.Page.ApplicationContext?.ApplicationName) }
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
                Image = application?.Icon,
                Height = 50,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None),
                Styles = new List<string>() { "padding: 0.5em;" }
            } :
            new ControlImage("webexpress.webapp.header.icon")
            {
                Uri = application?.Icon,
                Height = 50,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
            };

            return logo?.Render(context);
        }
    }
}
