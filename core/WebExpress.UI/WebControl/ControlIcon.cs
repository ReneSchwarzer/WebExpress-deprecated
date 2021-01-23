using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlIcon : Control
    {
        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon
        {
            get => (PropertyIcon)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt den Titel
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
        /// Liefert oder setzt die Größe
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlIcon(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Icon = new PropertyIcon(TypeIcon.None);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Icon.IsUserIcon)
            {
                return new HtmlElementMultimediaImg()
                {
                    ID = ID,
                    Src = Icon.UserIcon?.ToString(),
                    Class = GetClasses(),
                    Style = GetStyles(),
                    Role = Role,
                    Title = Title
                };
            }

            var html = new HtmlElementTextSemanticsSpan()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            html.AddUserAttribute("title", Title);

            return html;
        }
    }
}
