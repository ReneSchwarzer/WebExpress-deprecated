using System;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Numerischer Indikator
    /// </summary>
    public class ControlBadge : Control
    {
        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackgroundBadge BackgroundColor
        {
            get => (PropertyColorBackgroundBadge)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt ob abgerundete Ecken verwendet werden soll
        /// </summary>
        public TypePillBadge Pill
        {
            get => (TypePillBadge)GetProperty(TypePillBadge.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the target uri.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Die vertikale Ausrichtung
        /// </summary>
        public TypeVerticalAlignment VerticalAlignment
        {
            get => (TypeVerticalAlignment)GetProperty(TypeVerticalAlignment.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlBadge(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        public ControlBadge(string id, string value)
            : base(id)
        {
            Value = value;

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        public ControlBadge(string id, int value)
            : base(id)
        {
            Value = value.ToString();

            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Uri != null)
            {
                return new HtmlElementTextSemanticsA(new HtmlText(Value.ToString()))
                {
                    Id = Id,
                    Class = Css.Concatenate("badge", GetClasses()),
                    Style = GetStyles(),
                    Href = Uri.ToString(),
                    Role = Role
                };
            }

            return new HtmlElementTextSemanticsSpan(new HtmlText(Value.ToString()))
            {
                Id = Id,
                Class = Css.Concatenate("badge", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
