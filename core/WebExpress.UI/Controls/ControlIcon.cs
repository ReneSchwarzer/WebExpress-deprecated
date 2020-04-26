using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlIcon : Control
    {
        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon
        {
            get => (Icon)GetProperty();
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public PropertyColorText Color
        {
            get => (PropertyColorText)GetPropertyObject();
            set => SetProperty(value, () => value.ToClass(), () => value.ToStyle());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlIcon(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Icon = Icon.None;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var html = new HtmlElementTextSemanticsSpan()
            {
                ID = ID,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
