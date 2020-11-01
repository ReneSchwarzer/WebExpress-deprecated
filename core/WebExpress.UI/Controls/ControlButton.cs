using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlButton : Control
    {
        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackground BackgroundColor { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Farbe der Schaltfläche
        /// </summary>
        public PropertyColorButton Color
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
        /// Liefert oder setzt ob die Schaltfläche deaktiviert ist
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Text der TextBox
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt einen modalen Dialag
        /// </summary>
        public ControlModal Modal { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }
        //{
        //    get => (PropertyIcon)GetPropertyObject();
        //    set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        //}

        /// <summary>
        /// Liefert oder setzt den Aktivierungsstatus der Schaltfläche
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty();
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlButton(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypeSizeButton.Default;
            Content = new List<Control>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementFieldButton()
            {
                Type = "button",
                Value = Value,
                Class = Css.Concatenate("btn", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Disabled = Disabled
            };

            if (Icon != null && Icon.HasIcon)
            {
                html.Elements.Add(new ControlIcon()
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
                html.Elements.Add(new HtmlText(Text));
            }

            if (!string.IsNullOrWhiteSpace(OnClick))
            {
                html.AddUserAttribute("onclick", OnClick);
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.Render(context)));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.Render(context));
            }

            return html;
        }
    }
}
