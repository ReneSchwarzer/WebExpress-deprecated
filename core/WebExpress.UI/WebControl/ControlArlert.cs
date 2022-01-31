using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Erstellt eine Box, welche die Aufmerksamkeit des Benutzers erlangen soll 
    /// </summary>
    public class ControlAlert : Control
    {
        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackgroundAlert BackgroundColor
        {
            get => (PropertyColorBackgroundAlert)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt ob das Control geschlossen werden kann
        /// </summary>
        public TypeDismissibleAlert Dismissible
        {
            get => (TypeDismissibleAlert)GetProperty(TypeDismissibleAlert.Dismissible);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt ob der Fadereffekt verwendet werden soll
        /// </summary>
        public TypeFade Fade
        {
            get => (TypeFade)GetProperty(TypeFade.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Überschrift
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlAlert(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var head = new HtmlElementTextSemanticsStrong
            (
                new HtmlText(Head),
                new HtmlNbsp()
            );

            var button = new HtmlElementFieldButton()
            {
                Class = "btn-close"
            };
            button.AddUserAttribute("data-bs-dismiss", "alert");
            button.AddUserAttribute("aria-label", "close");

            return new HtmlElementTextContentDiv
            (
                !string.IsNullOrWhiteSpace(Head) ? head : null,
                new HtmlText(Text),
                Dismissible != TypeDismissibleAlert.None ? button : null
            )
            {
                ID = ID,
                Class = Css.Concatenate("alert", GetClasses()),
                Style = GetStyles(),
                Role = "alert"
            };
        }
    }
}
