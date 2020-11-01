using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelNavbar : ControlPanel
    {
        /// <summary>
        /// Fixierungstypen
        /// </summary>
        public enum FixedTypes { None, Top, Bottom }

        /// <summary>
        /// Stacks the navbar vertically on extra large, large, medium or small screens
        /// </summary>
        public enum ExpandTypes { None, ExtraLarge, Large, Medium, Small }

        /// <summary>
        /// Liefert oder setzt das Control
        /// </summary>
        private ControlText ControlText { get; set; }

        /// <summary>
        /// Liefert oder setzt das Control
        /// </summary>
        public ControlHamburgerMenu HamburgerMenu { get; set; }

        /// <summary>
        /// Liefert oder setzt die ToolBar
        /// </summary>
        public ControlToolBar ToolBar { get; private set; }

        /// <summary>
        /// Liefert oder setzt die ToolBar
        /// </summary>
        public ControlToolBar NotificationBar { get; private set; }

        /// <summary>
        /// Liefert und setzt das Darkthema
        /// </summary>
        public bool Dark { get; set; }

        /// <summary>
        /// Liefert und setzt ob die Fixierung der Navbar
        /// </summary>
        public FixedTypes Fixed { get; set; }

        /// <summary>
        /// Liefert und setzt ob die Breite zur vertikalen und horizontalen Ausrichtung
        /// </summary>
        public ExpandTypes Expand { get; set; }

        /// <summary>
        /// Liefert und setzt ob die Navbar angedokt wird, wenn diese den Rand erreicht
        /// </summary>
        public bool Sticky { get; set; }

        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title
        {
            get => ControlText.Text;
            set => ControlText.Text = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelNavbar(string id = null)
            : base(id)
        {
            ControlText = new ControlText("headline") { Classes = new List<string>(new[] { "headline" }), Format = TypeFormatText.Paragraph };
            HamburgerMenu = new ControlHamburgerMenu("hamburger") { };

            ToolBar = new ControlToolBar("toolbar") { Classes = new List<string>(new[] { "toolbar" })};
            NotificationBar = new ControlToolBar("notificationbar") { Classes = new List<string>(new[] { "notificationbar" })};

            Expand = ExpandTypes.None;
            Fixed = FixedTypes.None;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="title">Die Überschrift</param>
        public ControlPanelNavbar(string id, string title)
            : this(id)
        {
            Title = title;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("navbar");

            if (Dark)
            {
                Classes.Add("navbar-dark");
            }
            else
            {
                Classes.Add("navbar-light");
            }

            switch (Fixed)
            {
                case FixedTypes.Top:
                    Classes.Add("fixed-top");
                    break;
                case FixedTypes.Bottom:
                    Classes.Add("fixed-bottom");
                    break;
            }

            switch (Expand)
            {
                case ExpandTypes.ExtraLarge:
                    Classes.Add("navbar-expand-xl");
                    break;
                case ExpandTypes.Large:
                    Classes.Add("navbar-expand-lg");
                    break;
                case ExpandTypes.Medium:
                    Classes.Add("navbar-expand-md");
                    break;
                case ExpandTypes.Small:
                    Classes.Add("navbar-expand-sm");
                    break;
            }

            if (Sticky)
            {
                Classes.Add("sticky-top");
            }

            var html = new HtmlElementSectionNav()
            {
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            html.Elements.Add(HamburgerMenu.Render(context));
            html.Elements.Add(ToolBar.Render(context));
            html.Elements.Add(new HtmlElementTextSemanticsSpan(new HtmlText(Title)) { Class = "navbar-text" });
            html.Elements.Add(NotificationBar.Render(context));

            return html;
        }
    }
}
