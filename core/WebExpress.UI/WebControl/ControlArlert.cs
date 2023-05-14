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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlAlert(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
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
                ID = Id,
                Class = Css.Concatenate("alert", GetClasses()),
                Style = GetStyles(),
                Role = "alert"
            };
        }
    }
}
