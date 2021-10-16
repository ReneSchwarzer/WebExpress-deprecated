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
        public ControlWebAppHeader Header { get; protected set; } = new ControlWebAppHeader("header");

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlWebAppContent Content { get; protected set; } = new ControlWebAppContent("content");

        /// <summary>
        /// Liefert oder setzt den Fuß
        /// </summary>
        public ControlWebAppFooter Footer { get; protected set; } = new ControlWebAppFooter("footer");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Meldungen
        /// </summary>
        public ControlPanelToast Toast { get; protected set; } = new ControlPanelToast("toast");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Pfadangabe
        /// </summary>
        public ControlBreadcrumb Breadcrumb { get; protected set; } = new ControlBreadcrumb("breadcrumb");

        /// <summary>
        /// Liefert oder setzt den Bereich für Prolog
        /// </summary>
        public ControlPanel Prologue { get; protected set; } = new ControlPanel("prologue");

        /// <summary>
        /// Liefert oder setzt den den Bereich für die Suchoptionen
        /// </summary>
        public ControlPanel SearchOptions { get; protected set; } = new ControlPanel("searchoptions");

        /// <summary>
        /// Liefert oder setzt die Sidebar
        /// </summary>
        public ControlWebAppSidebar Sidebar { get; protected set; } = new ControlWebAppSidebar("sidebar");

        /// <summary>
        /// Konstruktor
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
            Content.Width = TypeWidth.OneHundred;

            Footer.BackgroundColor = LayoutSchema.FooterBackground;
        }

    }
}
