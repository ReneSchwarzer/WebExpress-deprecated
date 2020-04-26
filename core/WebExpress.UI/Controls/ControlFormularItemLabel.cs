using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemLabel : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt den Text des Labels
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das zugehörige Formularfeld
        /// </summary>
        public ControlFormularItem FormularItem { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemLabel(IControlFormular formular, string id)
            : base(formular, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        public ControlFormularItemLabel(IControlFormular formular, string id, string text)
            : this(formular, id)
        {
            Text = text;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            return new HtmlElementFieldLabel()
            {
                Text = Text,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                For = FormularItem != null ?
                    string.IsNullOrWhiteSpace(FormularItem.ID) ?
                    FormularItem.Name :
                    FormularItem.ID :
                    null
            };
        }
    }
}
