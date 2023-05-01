﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlDropdown : Control, IControlNavigationItem
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<IControlDropdownItem> Items { get; private set; } = new List<IControlDropdownItem>();

        /// <summary>
        /// Liefert oder setzt die Farbe der Schaltfläche
        /// </summary>
        public new PropertyColorButton BackgroundColor
        {
            get => (PropertyColorButton)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(Outline), () => value?.ToStyle(Outline));
        }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Outline-Eigenschaft
        /// </summary>
        public bool Outline { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche die volle Breite einnehmen soll
        /// </summary>
        public TypeBlockButton Block
        {
            get => (TypeBlockButton)GetProperty(TypeBlockButton.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt ein Indikator, welcher darauf hinweist, dass ein Menü vorhanden ist.
        /// </summary>
        public TypeToggleDropdown Toogle
        {
            get => (TypeToggleDropdown)GetProperty(TypeToggleDropdown.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the text. The text.Box
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the tooltip.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Aktivierungsstatus der Schaltfläche
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty(TypeActive.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt den die Ausrichtung des Menüs
        /// </summary>
        public TypeAlignmentDropdownMenu AlignmentMenu
        {
            get => (TypeAlignmentDropdownMenu)GetProperty(TypeAlignmentDropdownMenu.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        public Uri Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe
        /// </summary>
        public new int Height { get; set; } = -1;

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public new int Width { get; set; } = -1;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlDropdown(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">The content of the html element.</param>
        public ControlDropdown(string id, params IControlDropdownItem[] content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlDropdown(params IControlDropdownItem[] content)
            : base(null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">The content of the html element.</param>
        public ControlDropdown(string id, IEnumerable<IControlDropdownItem> content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlDropdown(IEnumerable<IControlDropdownItem> content)
            : base(null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Fügt ein neues Item hinzu
        /// </summary>
        /// <param name="item"></param>
        public void Add(IControlDropdownItem item)
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
            Items.Add(new ControlDropdownItemHeader() { Text = text });
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Default;
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">Die Einträge welcher hinzugefügt werden sollen</param>
        public void Add(params IControlDropdownItem[] item)
        {
            Items.AddRange(item);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                ID = Id,
                Class = Css.Concatenate("dropdown", Margin.ToClass()),
                Role = Role
            };

            if (Image == null)
            {
                var button = new HtmlElementFieldButton()
                {
                    ID = string.IsNullOrWhiteSpace(Id) ? "" : Id + "_btn",
                    Class = Css.Concatenate("btn", Css.Remove(GetClasses(), Margin.ToClass())),
                    Style = GetStyles(),
                    Title = Title
                };
                button.AddUserAttribute("data-bs-toggle", "dropdown");
                button.AddUserAttribute("aria-expanded", "false");

                if (Icon != null && Icon.HasIcon)
                {
                    button.Elements.Add(new ControlIcon()
                    {
                        Icon = Icon,
                        Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                    (
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.Two,
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.None
                    ) : new PropertySpacingMargin(PropertySpacing.Space.None),
                        VerticalAlignment = Icon.IsUserIcon ? TypeVerticalAlignment.TextBottom : TypeVerticalAlignment.Default
                    }.Render(context));
                }

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    button.Elements.Add(new HtmlText(Text));
                }

                html.Elements.Add(button);
            }
            else
            {
                var button = new HtmlElementMultimediaImg()
                {
                    ID = string.IsNullOrWhiteSpace(Id) ? "" : Id + "_btn",
                    Class = Css.Concatenate("btn", Css.Remove(GetClasses(), Margin.ToClass())),
                    Style = GetStyles(),
                    Src = Image.ToString()
                };
                button.AddUserAttribute("data-bs-toggle", "dropdown");
                button.AddUserAttribute("aria-expanded", "false");

                if (Height > 0)
                {
                    button.Height = Height;
                }

                if (Width > 0)
                {
                    button.Width = Width;
                }

                html.Elements.Add(button);
            }

            html.Elements.Add
            (
                new HtmlElementTextContentUl
                (
                    Items.Select
                    (
                        x =>
                        x == null || x is ControlDropdownItemDivider || x is ControlLine ?
                        new HtmlElementTextContentLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownItemHeader ?
                        x.Render(context) :
                        new HtmlElementTextContentLi(x.Render(context)) { Class = "dropdown-item " + ((x as ControlDropdownItemLink).Active == TypeActive.Disabled ? "disabled" : "") }
                    )
                )
                {
                    Class = Css.Concatenate
                    (
                        "dropdown-menu",
                        AlignmentMenu.ToClass()
                    )
                }
            );

            return html;
        }
    }
}
