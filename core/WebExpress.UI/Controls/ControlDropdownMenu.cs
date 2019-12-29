using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlDropdownMenu : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutButton Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypesSize Size { get; set; }

        /// <summary>
        /// Liefert oder setzt doe Outline-Eigenschaft
        /// </summary>
        public bool Outline { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche die volle Breite einnehmen soll
        /// </summary>
        public bool Block { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche deaktiviert ist
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Items { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Klasse der Schaltfläche
        /// </summary>
        public string ClassButton { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Style der Schaltfläche
        /// </summary>
        public string StyleButton { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlDropdownMenu(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdownMenu(IPage page, string id, params Control[] content)
            : this(page, id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdownMenu(IPage page, string id, IEnumerable<Control> content)
            : this(page, id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Fügt ein neues Item hinzu
        /// </summary>
        /// <param name="item"></param>
        public void Add(Control item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Fügt ein neuen Seterator hinzu
        /// </summary>
        public void AddSeperator()
        {
            Items.Add(null);
        }

        /// <summary>
        /// Fügt ein neuen Kopf hinzu
        /// </summary>
        /// <param name="text">Der Überschriftstext</param>
        public void AddHeader(string text)
        {
            Items.Add(new ControlDropdownMenuHeader(Page) { Text = text });
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypesSize.Default;
            Class = "";
            ClassButton = "";
            Items = new List<Control>();
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">Die Einträge welcher hinzugefügt werden sollen</param>
        public void Add(params Control[] item)
        {
            Items.AddRange(item);
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
                "dropdown"
            };

            var buttonClasses = new List<string>
            {
                ClassButton,
                "btn"
            };

            if (Outline)
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        buttonClasses.Add("btn-outline-primary");
                        break;
                    case TypesLayoutButton.Success:
                        buttonClasses.Add("btn-outline-success");
                        break;
                    case TypesLayoutButton.Info:
                        buttonClasses.Add("btn-outline-info");
                        break;
                    case TypesLayoutButton.Warning:
                        buttonClasses.Add("btn-outline-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        buttonClasses.Add("btn-outline-danger");
                        break;
                    case TypesLayoutButton.Light:
                        buttonClasses.Add("btn-outline-light");
                        break;
                    case TypesLayoutButton.Dark:
                        buttonClasses.Add("btn-outline-dark");
                        break;
                }
            }
            else
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        buttonClasses.Add("btn-primary");
                        break;
                    case TypesLayoutButton.Success:
                        buttonClasses.Add("btn-success");
                        break;
                    case TypesLayoutButton.Info:
                        buttonClasses.Add("btn-info");
                        break;
                    case TypesLayoutButton.Warning:
                        buttonClasses.Add("btn-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        buttonClasses.Add("btn-danger");
                        break;
                    case TypesLayoutButton.Light:
                        buttonClasses.Add("btn-light");
                        break;
                    case TypesLayoutButton.Dark:
                        buttonClasses.Add("btn-dark");
                        break;
                }
            }

            switch (Size)
            {
                case TypesSize.Large:
                    buttonClasses.Add("btn-lg");
                    break;
                case TypesSize.Small:
                    buttonClasses.Add("btn-sm");
                    break;
            }

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            if (Block)
            {
                buttonClasses.Add("btn-block");
            }

            var html = new HtmlElementDiv()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style
            };

            var button = new HtmlElementButton()
            {
                ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                Class = string.Join(" ", buttonClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = StyleButton,
                DataToggle = "dropdown"
            };

            if (Icon != Icon.None && !string.IsNullOrWhiteSpace(Text))
            {
                button.Elements.Add(new HtmlElementSpan() { Class = Icon.ToClass() });

                button.Elements.Add(new HtmlNbsp());
                button.Elements.Add(new HtmlNbsp());
                button.Elements.Add(new HtmlNbsp());
            }
            else if (Icon != Icon.None && string.IsNullOrWhiteSpace(Text))
            {
                button.AddClass(Icon.ToClass());
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                button.Elements.Add(new HtmlText(Text));
            }

            html.Elements.Add(button);
            html.Elements.Add
            (
                new HtmlElementUl
                (
                    Items.Select
                    (
                        x =>
                        x == null || x is ControlDropdownMenuDivider || x is ControlLine ?
                        new HtmlElementLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownMenuHeader ?
                        x.ToHtml() :
                        new HtmlElementLi(x.ToHtml()) { Class = "dropdown-item" }
                    )
                )
                {
                    Class = HorizontalAlignment == TypesHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
                }
            );

            return html;
        }
    }
}
