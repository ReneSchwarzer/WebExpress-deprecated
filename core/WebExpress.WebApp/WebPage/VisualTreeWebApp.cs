using System.Collections.Generic;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.WebApp.WebControl;

namespace WebExpress.WebApp.WebPage
{
    public class VisualTreeWebApp : VisualTreeControl
    {
        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlWebAppHeader Header { get; protected set; } = new ControlWebAppHeader("webexpress.webapp.header");

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlWebAppContent Content { get; protected set; } = new ControlWebAppContent("webexpress.webapp.content");

        /// <summary>
        /// Liefert oder setzt den Fuß
        /// </summary>
        public ControlWebAppFooter Footer { get; protected set; } = new ControlWebAppFooter("webexpress.webapp.footer");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Meldungen
        /// </summary>
        public ControlPanelToast Toast { get; protected set; } = new ControlPanelToast("webexpress.webapp.toast");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Pfadangabe
        /// </summary>
        public ControlBreadcrumb Breadcrumb { get; protected set; } = new ControlBreadcrumb("webexpress.webapp.breadcrumb");

        /// <summary>
        /// Liefert oder setzt den Bereich für Prolog
        /// </summary>
        public ControlPanel Prologue { get; protected set; } = new ControlPanel("webexpress.webapp.prologue");

        /// <summary>
        /// Liefert oder setzt den den Bereich für die Suchoptionen
        /// </summary>
        public ControlPanel SearchOptions { get; protected set; } = new ControlPanel("webexpress.webapp.searchoptions");

        /// <summary>
        /// Liefert oder setzt die Sidebar
        /// </summary>
        public ControlWebAppSidebar Sidebar { get; protected set; } = new ControlWebAppSidebar("webexpress.webapp.sidebar");

        /// <summary>
        /// Constructor
        /// </summary>
        public VisualTreeWebApp()
        {
            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Breadcrumb.Margin = new PropertySpacingMargin(PropertySpacing.Space.Null);
            Breadcrumb.BackgroundColor = LayoutSchema.BreadcrumbBackground;
            Breadcrumb.Size = LayoutSchema.BreadcrumbSize;

            Toast.BackgroundColor = LayoutSchema.ValidationWarningBackground;

            Sidebar.BackgroundColor = LayoutSchema.SidebarBackground;

            Content.BackgroundColor = LayoutSchema.ContentBackground;
            Content.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None);
            //Content.Width = TypeWidth.OneHundred;

            Footer.BackgroundColor = LayoutSchema.FooterBackground;
        }

    }
}
