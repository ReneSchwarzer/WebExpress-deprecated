using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

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
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> MorePreferences { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> MorePrimary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die App-Navigation
        /// </summary>
        public List<IControlDropdownItem> MoreSecondary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den Bereich für die Metadaten
        /// </summary>
        public List<IControl> Metadata { get; protected set; } = new List<IControl>();

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
            var prologue = new ControlPanelFlexbox(Prologue) { Layout = TypeLayoutFlexbox.Default, Align = TypeAlignFlexbox.Center, Justify = TypeJustifiedFlexbox.Start };
            prologue.Content.Add(new ControlText()
            {
                Text = context.I18N(context.Page.Title),
                TextColor = LayoutSchema.HeadlineTitle,
                Format = TypeFormatText.H2,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
            });
            prologue.Content.AddRange(Preferences);

            var epilog = new ControlPanelFlexbox(Secondary) { Layout = TypeLayoutFlexbox.Default, Align = TypeAlignFlexbox.Center, Justify = TypeJustifiedFlexbox.End };
            if (MorePreferences.Count() > 0 || MorePrimary.Count() > 0 || MoreSecondary.Count() > 0)
            {
                var more = new ControlDropdown("more")
                {
                    Title = context.I18N("webexpress.webapp", "headline.more.title"),
                    Icon = new PropertyIcon(TypeIcon.EllipsisHorizontal),
                    TextColor = LayoutSchema.HeadlineTitle,
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
                };

                foreach (var v in MorePreferences)
                {
                    more.Add(v);
                }

                if (MorePreferences.Count > 0 && (MorePrimary.Count > 0 || MoreSecondary.Count > 0))
                {
                    more.AddSeperator();
                }

                foreach (var v in MorePrimary)
                {
                    more.Add(v);
                }

                if (MorePrimary.Count() > 0 && MoreSecondary.Count > 0)
                {
                    more.AddSeperator();
                }

                foreach (var v in MoreSecondary)
                {
                    more.Add(v);
                }

                epilog.Content.Add(more);
            }

            var content = new ControlPanelFlexbox
            (
                prologue,
                new ControlPanelFlexbox(Primary) { Layout = TypeLayoutFlexbox.Default, Align = TypeAlignFlexbox.Center, Justify = TypeJustifiedFlexbox.End },
                epilog
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center,
                Justify = TypeJustifiedFlexbox.Between
            };

            var metadata = new HtmlElementTextContentDiv
            (
                Metadata.Select(x => x.Render(context))
            )
            {
                Class = Css.Concatenate("ml-2 mr-2 mb-3 text-secondary"),
                Style = Style.Concatenate("font-size:0.75rem;")
            };

            return new HtmlElementSectionHeader
            (
                content.Render(context),
                Metadata.Count > 0 ? metadata : null
            )
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
