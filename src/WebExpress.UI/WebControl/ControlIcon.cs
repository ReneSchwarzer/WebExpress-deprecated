using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlIcon : Control
    {
        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon
        {
            get => (PropertyIcon)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Returns or sets the title.
        /// </summary>
        public string Title { get; set; }

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
        /// <param name="id">Returns or sets the id.</param>
        public ControlIcon(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Icon = new PropertyIcon(TypeIcon.None);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Icon.IsUserIcon)
            {
                return new HtmlElementMultimediaImg()
                {
                    Id = Id,
                    Src = Icon.UserIcon?.ToString(),
                    Class = GetClasses(),
                    Style = GetStyles(),
                    Role = Role,
                    Title = Title
                };
            }

            var html = new HtmlElementTextSemanticsSpan()
            {
                Id = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            html.AddUserAttribute("title", Title);

            return html;
        }
    }
}
