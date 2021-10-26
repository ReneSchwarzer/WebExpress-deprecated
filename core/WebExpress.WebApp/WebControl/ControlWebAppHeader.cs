using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeader : Control
    {
        /// <summary>
        /// Liefert oder setzt das Logo
        /// </summary>
        public IUri Logo { get; set; }

        /// <summary>
        /// Liefert oder setzt den Anwendungsnamen
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die Elemente des Hamburgermenüs
        /// </summary>
        public List<IControlDropdownItem> HamburgerPrimary { get; private set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Elemente des Hamburgermenüs
        /// </summary>
        public List<IControlDropdownItem> HamburgerSecondary { get; private set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> NavigationPreferences { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> NavigationPrimary { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> NavigationSecondary { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zum schnellen Erzeugen neuer Inhalte
        /// </summary>
        public List<IControlSplitButtonItem> QuickCreatePrimary { get; protected set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zum schnellen Erzeugen neuer Inhalte
        /// </summary>
        public List<IControlSplitButtonItem> QuickCreateSecondary { get; protected set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zur Hilfe
        /// </summary>
        public List<IControlDropdownItem> HelpPreferences { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zur Hilfe
        /// </summary>
        public List<IControlDropdownItem> HelpPrimary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zur Hilfe
        /// </summary>
        public List<IControlDropdownItem> HelpSecondary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Einstellungen
        /// </summary>
        public List<IControlDropdownItem> SettingsPrimary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Einstellungen
        /// </summary>
        public List<IControlDropdownItem> SettingsSecondary { get; protected set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new virtual PropertyColorNavbar TextColor
        {
            get => (PropertyColorNavbar)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Die fixierte Anordnung
        /// </summary>
        public virtual TypeFixed Fixed
        {
            get => (TypeFixed)GetProperty(TypeFixed.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Fixiert die Anordnung, wenn sich die Toolbar am oberen Rand befindet
        /// </summary>
        public virtual TypeSticky Sticky
        {
            get => (TypeSticky)GetProperty(TypeSticky.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeader(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Fixed = TypeFixed.Top;
            Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
            BackgroundColor = LayoutSchema.HeaderBackground;
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
                new ControlDropdownItemHeader() { Text = Title }
            };

            hamburger.AddRange(HamburgerPrimary);

            if (HamburgerPrimary.Count > 0 && HamburgerSecondary.Count > 0)
            {
                hamburger.Add(new ControlDropdownItemDivider());
            }

            hamburger.AddRange(HamburgerSecondary);

            var navigation = new List<IControlNavigationItem>(NavigationPreferences);
            navigation.AddRange(NavigationPrimary);
            navigation.AddRange(NavigationSecondary);

            var quickcreateList = new List<IControlSplitButtonItem>(QuickCreatePrimary);
            quickcreateList.AddRange(QuickCreateSecondary);

            var firstQuickcreate = (quickcreateList.FirstOrDefault() as ControlLink);
            firstQuickcreate?.Render(context);

            var helpList = new List<IControlDropdownItem>
            {
                new ControlDropdownItemHeader() { Text = context.I18N("webexpress.webapp", "header.help.label") }
            };
            helpList.AddRange(HelpPreferences);
            if (HelpPreferences.Count > 0 && HelpPrimary.Count > 0)
            {
                helpList.Add(new ControlDropdownItemDivider());
            }
            helpList.AddRange(HelpPrimary);
            if (HelpPrimary.Count > 0 && HelpSecondary.Count > 0)
            {
                helpList.Add(new ControlDropdownItemDivider());
            }
            helpList.AddRange(HelpSecondary);

            var settingsList = new List<IControlDropdownItem>
            {
                new ControlDropdownItemHeader() { Text = context.I18N("webexpress.webapp", "header.setting.label") }
            };
            settingsList.AddRange(SettingsPrimary);
            if (SettingsPrimary.Count > 0 && SettingsSecondary.Count > 0)
            {
                settingsList.Add(new ControlDropdownItemDivider());
            }
            settingsList.AddRange(SettingsSecondary);

            var logo = (hamburger.Count > 1) ?
            (IControl)new ControlDropdown("logo", hamburger)
            {
                Image = Logo,
                Height = 50,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None),
                Styles = new List<string>() { "padding: 0.5em;" }
            } :
            new ControlImage("logo")
            {
                Uri = Logo,
                Height = 50,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
            };

            var apptitle = new ControlLink("apptitle", new ControlText()
            {
                Text = Title,
                TextColor = LayoutSchema.HeaderTitle,
                Format = TypeFormatText.H1,
                Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
            })
            {
                Uri = context?.Page?.Context?.Application?.ContextPath,
                Decoration = TypeTextDecoration.None
            };

            var appnavigation = new ControlNavigation("appnavigation", navigation)
            {
                Layout = TypeLayoutTab.Default,
                ActiveColor = LayoutSchema.HeaderNavigationActiveBackground,
                ActiveTextColor = LayoutSchema.HeaderNavigationActive,
                LinkColor = LayoutSchema.HeaderNavigationLink
            };

            var quickcreate = (quickcreateList.Count > 1) ?
            (IControl)new ControlSplitButtonLink("quickcreate", quickcreateList.Skip(1))
            {
                Text = context.I18N("webexpress.webapp", "header.quickcreate.label"),
                Uri = firstQuickcreate?.Uri,
                BackgroundColor = LayoutSchema.HeaderQuickCreateButtonBackground,
                Size = LayoutSchema.HeaderQuickCreateButtonSize,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None)
            } :
            (QuickCreatePrimary.Count > 0) ?
            new ControlButtonLink("quickcreate")
            {
                Text = context.I18N("webexpress.webapp", "header.quickcreate.label"),
                Uri = firstQuickcreate?.Uri,
                BackgroundColor = LayoutSchema.HeaderQuickCreateButtonBackground,
                Size = LayoutSchema.HeaderQuickCreateButtonSize,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None)
            } :
            null;

            var help = (helpList.Count > 1) ?
            new ControlDropdown("help", helpList)
            {
                Icon = new PropertyIcon(TypeIcon.InfoCircle),
                AlighmentMenu = TypeAlighmentDropdownMenu.Right,
                BackgroundColor = new PropertyColorButton(TypeColorButton.Dark),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None)
            } :
            null;

            var settings = (settingsList.Count > 1) ?
            new ControlDropdown("settings", settingsList)
            {
                Icon = new PropertyIcon(TypeIcon.Cog),
                AlighmentMenu = TypeAlighmentDropdownMenu.Right,
                BackgroundColor = new PropertyColorButton(TypeColorButton.Dark),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None)
            } :
            null;

            var content = new ControlPanelFlexbox
            (
                logo,
                apptitle,
                appnavigation,
                quickcreate,
                new ControlPanel() { Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None) },
                help,
                settings
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center
            };

            return new HtmlElementSectionHeader(content.Render(context))
            {
                ID = ID,
                Class = Css.Concatenate("navbar", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
