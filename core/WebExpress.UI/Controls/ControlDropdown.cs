﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlDropdown : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Items { get; private set; } = new List<Control>();

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
        /// Liefert oder setzt den Text der TextBox
        /// </summary>
        public string Text { get; set; }

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
        /// Liefert oder setzt das Bild
        /// </summary>
        public IUri Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe
        /// </summary>
        public int Heigt { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlDropdown(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdown(string id, params Control[] content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdown(params Control[] content)
            : base(null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdown(string id, IEnumerable<Control> content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlDropdown(IEnumerable<Control> content)
            : base(null)
        {
            Items.AddRange(content);

            Init();
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
            Items.Add(new ControlDropdownHeader() { Text = text });
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Default; 
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {

            //Classes.Add(HorizontalAlignment.ToClass());

            //if (Block)
            //{
            //    buttonClasses.Add("btn-block");
            //}

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = "dropdown",
                Role = Role
            };

            if (Image == null)
            {
                var button = new HtmlElementFieldButton()
                {
                    ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                    Class = Css.Concatenate("btn", GetClasses()),
                    Style = GetStyles(),
                    DataToggle = "dropdown"
                };

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
                    ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                    Class = Css.Concatenate("", GetClasses()),
                    Style = GetStyles(),
                    Src = Image.ToString(),
                    Height = Heigt,
                    Width = Width,
                    DataToggle = "dropdown"
                };

                html.Elements.Add(button);
            }

            html.Elements.Add
            (
                new HtmlElementTextContentUl
                (
                    Items.Select
                    (
                        x =>
                        x == null || x is ControlDropdownDivider || x is ControlLine ?
                        new HtmlElementTextContentLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownHeader ?
                        x.Render(context) :
                        new HtmlElementTextContentLi(x.Render(context)) { Class = "dropdown-item" }
                    )
                )
                {
                    Class = HorizontalAlignment == TypeHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
                }
            );

            return html;
        }
    }
}
