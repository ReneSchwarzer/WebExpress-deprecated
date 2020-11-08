using System.Collections.Generic;
using System.Security.Principal;
using WebExpress.Html;
using WebExpress.UI.Controls;

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
        public ControlPanelHeader Header { get; protected set; } = new ControlPanelHeader("header");

        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlDropdown Hamburger { get; protected set; } = new ControlDropdown("hamburger");

        /// <summary>
        /// Liefert oder setzt den Anwendungsnamen
        /// </summary>
        public ControlText AppTitle { get; protected set; } = new ControlText("apptitle");

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Funktionen
        /// </summary>
        public ControlPanel AppFunctions { get; protected set; } = new ControlPanel("appfunctions");
        
        /// <summary>
        /// Liefert oder setzt die Schaltfläche zum schnellen Erzeugen neuer Inhalte
        /// </summary>
        public ControlSplitButton QuickCreate { get; protected set; } = new ControlSplitButton("quickcreate");

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

            Hamburger.Width = 70;
            Hamburger.Heigt = 60;
            Hamburger.Image = Uri.Root.Append(Context.IconUrl);
            Hamburger.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);

            AppTitle.Text = Context.Name;
            AppTitle.TextColor = new PropertyColorText(TypeColorText.White);
            AppTitle.Format = TypeFormatText.H1;
            AppTitle.Padding = new PropertySpacingPadding(PropertySpacing.Space.One);
            AppTitle.Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null);

            QuickCreate.Text = "Erstellen";
            QuickCreate.BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            QuickCreate.Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None);

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
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
            Header.Content.Add(new ControlPanelFlexbox
            (
                Hamburger,
                AppTitle,
                AppFunctions.Content.Count > 0 ? AppFunctions : null,
                QuickCreate
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center
            });

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
