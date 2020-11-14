using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace WebExpress.UI.Pages
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// siehe doc/PageTemplateWebApp.vsdx
    /// </summary>
    public abstract class PageTemplateWebApp : PageTemplate
    {
        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlHeaderWebApp Header { get; protected set; } = new ControlHeaderWebApp("header");

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlPanelMain Content { get; protected set; } = new ControlPanelMain("content");

        /// <summary>
        /// Liefert oder setzt den Fuß
        /// </summary>
        public ControlPanelFooter Footer { get; protected set; } = new ControlPanelFooter("footer");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Meldungen
        /// </summary>
        public ControlPanelToast Toast { get; protected set; } = new ControlPanelToast("toast");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Pfadangabe
        /// </summary>
        public ControlBreadcrumb Breadcrumb { get; protected set; } = new ControlBreadcrumb("breadcrumb");

        /// <summary>
        /// Liefert oder setzt den den Bereich für die Suchoptionen
        /// </summary>
        public ControlPanel SearchOptions { get; protected set; } = new ControlPanel("searchoptions");

        /// <summary>
        /// Liefert oder setzt die Sidebar
        /// </summary>
        public ControlPanel Sidebar { get; protected set; } = new ControlPanel("sidebar");

        /// <summary>
        /// Liefert oder setzt den Bereich in der Sidebar für erweiterte Inhalte
        /// </summary>
        public ControlPanel SidebarContent { get; protected set; } = new ControlPanel("sidebarcontent");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Kopfzeile der Sidebar
        /// </summary>
        public ControlPanel SidebarHeader { get; protected set; } = new ControlPanel("sidebarheader");

        /// <summary>
        /// Liefert oder setzt den Navigationsbereich
        /// </summary>
        public ControlPanelNavbar SidebarNavigation { get; protected set; } = new ControlPanelNavbar("sidebarnavigation");

        /// <summary>
        /// Liefert oder setzt den Navigationsbereich
        /// </summary>
        public ControlText PageTitle { get; protected set; } = new ControlText("pagetitle");

        /// <summary>
        /// Liefert oder setzt die Seitenfunktionen
        /// </summary>
        public ControlPanel PageFunctions { get; protected set; } = new ControlPanel("pagefunctions");

        /// <summary>
        /// Liefert oder setzt den Seiteneigenschaften
        /// </summary>
        public ControlPanel PageProperty { get; protected set; } = new ControlPanel("pageproperty");

        /// <summary>
        /// Liefert oder setzt den Navigationsbereich
        /// </summary>
        public ControlToolBar Toolbar { get; protected set; } = new ControlToolBar("toolbar");

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebApp()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            Header.BackgroundColor = new PropertyColorBackground(TypeColorBackground.Dark);
            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Header.Logo = Uri.Root.Append(Context.IconUrl);
            Header.Title = Context.Name;

            Breadcrumb.Uri = Uri;
            Breadcrumb.Margin = new PropertySpacingMargin(PropertySpacing.Space.Null);

            Toast.BackgroundColor = new PropertyColorBackgroundAlert(TypeColorBackground.Warning);

            Sidebar.BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);

            PageTitle.Text = Title;
            PageTitle.Format = TypeFormatText.H2;
            PageTitle.TextColor = new PropertyColorText(TypeColorText.Dark);
            PageTitle.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);

            Content.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var hamburgerPrimary = Context.Host.CreatePluginComponet<IPluginComponentHamburgerPrimary>();
            Header.HamburgerPrimary.AddRange(hamburgerPrimary.Where(x => x != null));
            
            var hamburgerSecondary = Context.Host.CreatePluginComponet<IPluginComponentHamburgerSecondary>();
            Header.HamburgerSecondary.AddRange(hamburgerSecondary.Where(x => x != null));

            var appNavigationPrimary = Context.Host.CreatePluginComponet<IPluginComponentAppNavigationPrimary>();
            Header.NavigationPrimary.AddRange(appNavigationPrimary.Where(x => x != null));

            var appNavigationSecondary = Context.Host.CreatePluginComponet<IPluginComponentAppNavigationSecondary>();
            Header.NavigationSecondary.AddRange(appNavigationSecondary.Where(x => x != null));

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {


            Header.Classes.Add("navbar");

            Sidebar.Content.Add(SidebarHeader);
            Sidebar.Content.Add(SidebarNavigation);
            Sidebar.Content.Add(SidebarContent);

            var flexbox = new ControlPanelFlexbox
            (
                Sidebar,
                new ControlPanel
                (
                    Toolbar,
                    new ControlPanelFlexbox
                    (
                        PageTitle,
                        PageFunctions
                    )
                    {
                        Layout = TypeLayoutFlexbox.Default,
                        Align = TypeAlignFlexbox.Start
                    },
                    new ControlPanelFlexbox
                    (
                        Content,
                        PageProperty
                    )
                    {
                        Layout = TypeLayoutFlexbox.Default,
                        Align = TypeAlignFlexbox.Stretch
                    }
                )
                {

                }
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Stretch
            };


            base.Content.Add(Header);
            base.Content.Add(Toast);
            base.Content.Add(Breadcrumb);
            base.Content.Add(SearchOptions);
            base.Content.Add(flexbox);
            base.Content.Add(Footer);

            return base.Render();
        }
    }
}
