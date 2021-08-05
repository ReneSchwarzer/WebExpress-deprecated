using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebResource;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeadline : Control
    {
        /// <summary>
        /// Liefert oder setzt den Vorwort-Bereich für die App-Navigation
        /// </summary>
        public List<IControl> Prologue { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControl> Preferences { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControl> Primary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControl> Secondary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeadline(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.HeadlineBackground;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var prologue = new ControlPanelFlexbox(Prologue) { Layout = TypeLayoutFlexbox.Default, Justify = TypeJustifiedFlexbox.Start };
            prologue.Content.Add(new ControlText()
            {
                Text = context.I18N(context.Page.Title),
                TextColor = LayoutSchema.HeadlineTitle,
                Format = TypeFormatText.H2,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
            });

            var content = new ControlPanelFlexbox
            (
                prologue,
                new ControlPanelFlexbox(Preferences) { Layout = TypeLayoutFlexbox.Default, Justify = TypeJustifiedFlexbox.End },
                new ControlPanelFlexbox(Primary) { Layout = TypeLayoutFlexbox.Default, Justify = TypeJustifiedFlexbox.End },
                new ControlPanelFlexbox(Secondary) { Layout = TypeLayoutFlexbox.Default, Justify = TypeJustifiedFlexbox.End }
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center,
                Justify = TypeJustifiedFlexbox.Between
            };

            return new HtmlElementSectionHeader(content.Render(context))
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
