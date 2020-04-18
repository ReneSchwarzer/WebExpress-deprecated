using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlSplitButtonLink : ControlSplitButton
    {
        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Klasse der Schaltfläche
        /// </summary>
        public string ClassButton { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Style der Schaltfläche
        /// </summary>
        public string StyleButton { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlSplitButtonLink(IPage page, string id)
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
        public ControlSplitButtonLink(IPage page, string id, params Control[] content)
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
        public ControlSplitButtonLink(IPage page, string id, IEnumerable<Control> content)
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
                "btn"
            };

            var containerClasses = new List<string>
            {
                ClassContainer,
                "btn-group",
                "btn-group-toggle"
            };

            var buttonClasses = new List<string>
            {
                ClassDropDown,
                "btn",
                "dropdown-toggle"
            };

            if (Outline)
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-outline-primary");
                        buttonClasses.Add("btn-outline-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-outline-success");
                        buttonClasses.Add("btn-outline-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-outline-info");
                        buttonClasses.Add("btn-outline-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-outline-warning");
                        buttonClasses.Add("btn-outline-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-outline-danger");
                        buttonClasses.Add("btn-outline-danger");
                        break;
                    case TypesLayoutButton.Light:
                        classes.Add("btn-outline-light");
                        buttonClasses.Add("btn-outline-light");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-outline-dark");
                        buttonClasses.Add("btn-outline-dark");
                        break;
                }
            }
            else
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-primary");
                        buttonClasses.Add("btn-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-success");
                        buttonClasses.Add("btn-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-info");
                        buttonClasses.Add("btn-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-warning");
                        buttonClasses.Add("btn-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-danger");
                        buttonClasses.Add("btn-danger");
                        break;
                    case TypesLayoutButton.Light:
                        classes.Add("btn-light");
                        buttonClasses.Add("btn-light");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-dark");
                        buttonClasses.Add("btn-dark");
                        break;
                }
            }

            switch (Size)
            {
                case TypesSize.Large:
                    classes.Add("btn-lg");
                    buttonClasses.Add("btn-lg");
                    break;
                case TypesSize.Small:
                    classes.Add("btn-sm");
                    buttonClasses.Add("btn-sm");
                    break;
            }

            if (Disabled)
            {
                classes.Add("disabled");
                buttonClasses.Add("disabled");
            }

            var html = new HtmlElementA(Text)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = "button",
                Href = Url
            };

            var dropdownButton = new HtmlElementP()
            {
                ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                Class = string.Join(" ", buttonClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
                //Style = StyleButton,
                DataToggle = "dropdown"
            };

            var dropdownElements = new HtmlElementUl
            (
                Items.Select
                (
                    x =>
                    x == null ?
                    new HtmlElementLi() { Class = "dropdown-divider", Inline = true } :
                    x is ControlDropdownMenuHeader ?
                    x.ToHtml() :
                    new HtmlElementLi(x.ToHtml().AddClass("dropdown-item")) { }
                )
            )
            {
                Class = HorizontalAlignment == TypesHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
            };

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);
            }

            return new HtmlElementDiv(html, dropdownButton, dropdownElements, Modal?.ToHtml())
            {
                Class = string.Join(" ", containerClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
            };
        }
    }
}
