using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlLine : Control
    {
        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new PropertyColorText TextColor { get; private set; }

        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackground BackgroundColor { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Farbe
        /// </summary>
        public PropertyColorLine Color
        {
            get => (PropertyColorLine)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlLine(IPage page, string id = null)
            : base(page, id)
        {
            Init();
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
            var html = new HtmlElementTextContentHr()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
