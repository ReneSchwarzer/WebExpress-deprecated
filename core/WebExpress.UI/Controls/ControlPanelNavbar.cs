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
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelNavbar(IPage page, string id = null)
            : base(page, id)
        {
            ControlText = new ControlText(Page, "headline") { Class = "headline", Format = TypesTextFormat.Paragraph };
            HamburgerMenu = new ControlHamburgerMenu(Page, "hamburger") { };

            ToolBar = new ControlToolBar(Page, "toolbar") { Class = "toolbar" };
            NotificationBar = new ControlToolBar(Page, "notificationbar") { Class = "notificationbar" };

            //Content.Add(HamburgerMenu);
            //Content.Add(ToolBar);
            //Content.Add(ControlText);
            //Content.Add(NotificationBar);
            Expand = ExpandTypes.None;
            Fixed = FixedTypes.None;

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="title">Die Überschrift</param>
        public ControlPanelNavbar(IPage page, string id, string title)
            : this(page, id)
        {
            Title = title;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "navbar"
            };
            //classes.Add("navbar-expand-xl");

            if (Dark)
            {
                classes.Add("navbar-dark");
            }
            else
            {
                classes.Add("navbar-light");
            }

            switch (Fixed)
            {
                case FixedTypes.Top:
                    classes.Add("fixed-top");
                    break;
                case FixedTypes.Bottom:
                    classes.Add("fixed-bottom");
                    break;
            }

            switch (Expand)
            {
                case ExpandTypes.ExtraLarge:
                    classes.Add("navbar-expand-xl");
                    break;
                case ExpandTypes.Large:
                    classes.Add("navbar-expand-lg");
                    break;
                case ExpandTypes.Medium:
                    classes.Add("navbar-expand-md");
                    break;
                case ExpandTypes.Small:
                    classes.Add("navbar-expand-sm");
                    break;
            }

            if (Sticky)
            {
                classes.Add("sticky-top");
            }

            var html = new HtmlElementNav()
            {
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x)))
            };

            html.Elements.Add(HamburgerMenu.ToHtml());
            html.Elements.Add(ToolBar.ToHtml());
            html.Elements.Add(new HtmlElementSpan(new HtmlText(Title)) { Class = "navbar-text" });
            html.Elements.Add(NotificationBar.ToHtml());

            return html;
        }
    }
}
