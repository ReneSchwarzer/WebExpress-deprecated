using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemButton : ControlFormItem
    {
        /// <summary>
        /// Returns or sets the color of the button.
        /// </summary>
        public PropertyColorButton Color
        {
            get => (PropertyColorButton)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(Outline), () => value?.ToStyle(Outline));
        }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the Outline property.
        /// </summary>
        public bool Outline { get; set; }

        /// <summary>
        /// Returns or sets whether the button should take up the full width.
        /// </summary>
        public TypeBlockButton Block
        {
            get => (TypeBlockButton)GetProperty(TypeBlockButton.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets whether the button is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Returns or sets the content.
        /// </summary>
        public List<Control> Content { get; private set; } = new List<Control>();

        /// <summary>
        /// Event is triggered when the button is clicked.
        /// </summary>
        public EventHandler<FormularEventArgs> Click;

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the type. (button, submit, reset)
        /// </summary>
        public TypeButton Type { get; set; } = TypeButton.Default;

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemButton(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementFieldButton()
            {
                Id = Id,
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
        /// Triggers the click event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnClickEvent(FormularEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
