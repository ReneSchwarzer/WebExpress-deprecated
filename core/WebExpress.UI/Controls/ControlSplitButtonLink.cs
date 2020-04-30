﻿using System.Collections.Generic;
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
            Size = TypeSizeButton.Default;
            Role = "button";
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
            Classes.Add("btn");

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
                        Classes.Add("btn-outline-primary");
                        buttonClasses.Add("btn-outline-primary");
                        break;
                    case TypesLayoutButton.Success:
                        Classes.Add("btn-outline-success");
                        buttonClasses.Add("btn-outline-success");
                        break;
                    case TypesLayoutButton.Info:
                        Classes.Add("btn-outline-info");
                        buttonClasses.Add("btn-outline-info");
                        break;
                    case TypesLayoutButton.Warning:
                        Classes.Add("btn-outline-warning");
                        buttonClasses.Add("btn-outline-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        Classes.Add("btn-outline-danger");
                        buttonClasses.Add("btn-outline-danger");
                        break;
                    case TypesLayoutButton.Light:
                        Classes.Add("btn-outline-light");
                        buttonClasses.Add("btn-outline-light");
                        break;
                    case TypesLayoutButton.Dark:
                        Classes.Add("btn-outline-dark");
                        buttonClasses.Add("btn-outline-dark");
                        break;
                }
            }
            else
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        Classes.Add("btn-primary");
                        buttonClasses.Add("btn-primary");
                        break;
                    case TypesLayoutButton.Success:
                        Classes.Add("btn-success");
                        buttonClasses.Add("btn-success");
                        break;
                    case TypesLayoutButton.Info:
                        Classes.Add("btn-info");
                        buttonClasses.Add("btn-info");
                        break;
                    case TypesLayoutButton.Warning:
                        Classes.Add("btn-warning");
                        buttonClasses.Add("btn-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        Classes.Add("btn-danger");
                        buttonClasses.Add("btn-danger");
                        break;
                    case TypesLayoutButton.Light:
                        Classes.Add("btn-light");
                        buttonClasses.Add("btn-light");
                        break;
                    case TypesLayoutButton.Dark:
                        Classes.Add("btn-dark");
                        buttonClasses.Add("btn-dark");
                        break;
                }
            }

            switch (Size)
            {
                case TypeSizeButton.Large:
                    Classes.Add("btn-lg");
                    buttonClasses.Add("btn-lg");
                    break;
                case TypeSizeButton.Small:
                    Classes.Add("btn-sm");
                    buttonClasses.Add("btn-sm");
                    break;
            }

            if (Disabled)
            {
                Classes.Add("disabled");
                buttonClasses.Add("disabled");
            }

            var html = new HtmlElementTextSemanticsA(Text)
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                Href = Url
            };

            var dropdownButton = new HtmlElementTextContentP()
            {
                ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                Class = string.Join(" ", buttonClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
                //Style = StyleButton,
                DataToggle = "dropdown"
            };

            var dropdownElements = new HtmlElementTextContentUl
            (
                Items.Select
                (
                    x =>
                    x == null ?
                    new HtmlElementTextContentLi() { Class = "dropdown-divider", Inline = true } :
                    x is ControlDropdownMenuHeader ?
                    x.ToHtml() :
                    new HtmlElementTextContentLi(x.ToHtml().AddClass("dropdown-item")) { }
                )
            )
            {
                Class = HorizontalAlignment == TypeHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
            };

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);
            }

            return new HtmlElementTextContentDiv(html, dropdownButton, dropdownElements, Modal?.ToHtml())
            {
                Class = string.Join(" ", containerClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
            };
        }
    }
}
