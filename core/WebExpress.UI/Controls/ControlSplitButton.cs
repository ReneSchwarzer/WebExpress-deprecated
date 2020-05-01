﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlSplitButton : ControlButton
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Items { get; private set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Klasse der Schaltfläche
        /// </summary>
        public string ClassContainer { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Klasse der Schaltfläche
        /// </summary>
        public string ClassDropDown { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlSplitButton(IPage page, string id)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Der Inhalt</param>
        public ControlSplitButton(IPage page, string id, params Control[] items)
            : base(page, id)
        {
            Init();

            Items.AddRange(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypeSizeButton.Default;
            Items = new List<Control>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var containerClasses = new List<string>
            {
                ClassContainer,
                "btn-group"
            };

            var buttonClasses = new List<string>
            {
                ClassDropDown,
                "btn",
                "dropdown-toggle"
            };

            //if (Outline)
            //{
            //    switch (Color)
            //    {
            //        case TypeColorButton.Primary:
            //            buttonClasses.Add("btn-outline-primary");
            //            break;
            //        case TypeColorButton.Success:
            //            buttonClasses.Add("btn-outline-success");
            //            break;
            //        case TypeColorButton.Info:
            //            buttonClasses.Add("btn-outline-info");
            //            break;
            //        case TypeColorButton.Warning:
            //            buttonClasses.Add("btn-outline-warning");
            //            break;
            //        case TypeColorButton.Danger:
            //            buttonClasses.Add("btn-outline-danger");
            //            break;
            //        case TypeColorButton.Light:
            //            buttonClasses.Add("btn-outline-light");
            //            break;
            //        case TypeColorButton.Dark:
            //            buttonClasses.Add("btn-outline-dark");
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (Color)
            //    {
            //        case TypeColorButton.Primary:
            //            buttonClasses.Add("btn-primary");
            //            break;
            //        case TypeColorButton.Success:
            //            buttonClasses.Add("btn-success");
            //            break;
            //        case TypeColorButton.Info:
            //            buttonClasses.Add("btn-info");
            //            break;
            //        case TypeColorButton.Warning:
            //            buttonClasses.Add("btn-warning");
            //            break;
            //        case TypeColorButton.Danger:
            //            buttonClasses.Add("btn-danger");
            //            break;
            //        case TypeColorButton.Light:
            //            buttonClasses.Add("btn-light");
            //            break;
            //        case TypeColorButton.Dark:
            //            buttonClasses.Add("btn-dark");
            //            break;
            //    }
            //}

            switch (Size)
            {
                case TypeSizeButton.Large:
                    buttonClasses.Add("btn-lg");
                    break;
                case TypeSizeButton.Small:
                    buttonClasses.Add("btn-sm");
                    break;
            }

            if (Disabled)
            {
                buttonClasses.Add("disabled");
            }

            var html = base.ToHtml();

            var dropdownButton = new HtmlElementFieldButton()
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

            return new HtmlElementTextContentDiv(html, dropdownButton, dropdownElements)
            {
                Class = string.Join(" ", containerClasses.Where(x => !string.IsNullOrWhiteSpace(x))),
            };
        }
    }
}
