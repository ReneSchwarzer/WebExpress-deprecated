using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemButton : ControlFormularItem
    {
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
        /// Liefert oder setzt doe Outline-Eigenschaft
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
        public List<Control> Content { get; private set; } = new List<Control>();

        /// <summary>
        /// Event wird ausgelöst, wenn die Schlatfläche geklickt wurde
        /// </summary>
        public EventHandler<FormularEventArgs> Click;

        /// <summary>
        /// Liefert oder setzt den Text der TextBox
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den Typ (button, submit, reset)
        /// </summary>
        public TypeButton Type { get; set; } = TypeButton.Default;

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemButton(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            Disabled = false;
            Size = TypeSizeButton.Default;

            if (context.Request.HasParameter(Name))
            {
                var value = context.Request.GetParameter(Name)?.Value;

                if (!string.IsNullOrWhiteSpace(Value) && value == Value)
                {
                    OnClickEvent(new FormularEventArgs() { Context = context });
                }
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementFieldButton()
            {
                ID = ID,
                Name = Name,
                Type = Type.ToTypeString(),
                Value = Value,
                Class = Css.Concatenate("btn", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Disabled = Disabled,
                OnClick = OnClick?.ToString()
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
                html.Elements.Add(new HtmlText(I18N(context.Culture, Text)));
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.Render(context)));
            }

            return html;
        }

        /// <summary>
        /// Löst das Click-Event aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnClickEvent(FormularEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
