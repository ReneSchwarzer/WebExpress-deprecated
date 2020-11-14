using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.Plugin;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Header für eine WenApp
    /// </summary>
    public class ControlHeaderWebApp : Control
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
        public List<IPluginComponentHamburgerPrimary> HamburgerPrimary { get; private set; } = new List<IPluginComponentHamburgerPrimary>();

        /// <summary>
        /// Liefert oder setzt die Elemente des Hamburgermenüs
        /// </summary>
        public List<IPluginComponentHamburgerSecondary> HamburgerSecondary { get; private set; } = new List<IPluginComponentHamburgerSecondary>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IPluginComponentAppNavigationPrimary> NavigationPrimary { get; protected set; } = new List<IPluginComponentAppNavigationPrimary>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IPluginComponentAppNavigationSecondary> NavigationSecondary { get; protected set; } = new List<IPluginComponentAppNavigationSecondary>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zum schnellen Erzeugen neuer Inhalte
        /// </summary>
        public List<IPluginComponentQuickCreatePrimary> QuickCreatePrimary { get; protected set; } = new List<IPluginComponentQuickCreatePrimary>();

        /// <summary>
        /// Liefert oder setzt die Schaltfläche zum schnellen Erzeugen neuer Inhalte
        /// </summary>
        public List<IPluginComponentQuickCreateSecondary> QuickCreateSecondary { get; protected set; } = new List<IPluginComponentQuickCreateSecondary>();

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
        public ControlHeaderWebApp(string id = null)
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
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Dark);

            TextColor = new PropertyColorNavbar(TypeColorNavbar.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var hamburger = new List<IControlDropdownItem>(HamburgerPrimary);
            if (HamburgerPrimary.Count > 0 && HamburgerSecondary.Count > 0)
            {
                hamburger.Add(new ControlDropdownDivider());
            }
            hamburger.AddRange(HamburgerSecondary);

            var navigation = new List<IControlNavigationItem>(NavigationPrimary);
            navigation.AddRange(NavigationSecondary);

            var quickcreate = new List<IControlSplitButtonItem>(QuickCreatePrimary);
            quickcreate.AddRange(QuickCreateSecondary);

            var content = new ControlPanelFlexbox
            (
                (hamburger.Count > 0) ? 
                (IControl)new ControlDropdown("logo", hamburger)
                {
                    Image = Logo,
                    Width = 70,
                    Height = 60,
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                } :
                new ControlImage("logo")
                {
                    Uri = Logo,
                    Width = 70,
                    Height = 60,
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                },
                new ControlLink("apptitle", new ControlText()
                {
                    Text = Title,
                    TextColor = new PropertyColorText(TypeColorText.White),
                    Format = TypeFormatText.H1,
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
                })
                {
                    Uri = context.Page.Uri.Root,
                    Decoration = TypeTextDecoration.None
                },
                new ControlNavigation("appnavigation", navigation)
                {
                    Layout = TypeLayoutTab.Default
                },
                (QuickCreateSecondary.Count > 0) ?
                (IControl)new ControlSplitButton("quickcreate", QuickCreateSecondary)
                {
                    Text = "Erstellen",
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Primary),
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None)
                } :
                (QuickCreatePrimary.Count > 0) ?
                new ControlButton("quickcreate")
                {
                    Text = "Erstellen",
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Primary),
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None)
                } :
                null
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center
            };

            return new HtmlElementSectionHeader(content.Render(context))
            {
                ID = ID,
                Class = Css.Concatenate("navbar", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
