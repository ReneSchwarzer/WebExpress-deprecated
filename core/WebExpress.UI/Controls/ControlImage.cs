using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlImage : Control
    {
        /// <summary>
        /// Liefert oder setzt die Bildquelle
        /// </summary>
        public IUri Source { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlImage(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="source">Die Bildquelle</param>
        public ControlImage(IPage page, string id, IUri source)
            : base(page, id)
        {
            Source = source;

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
            Classes.Add(HorizontalAlignment.ToClass());

            var html = new HtmlElementMultimediaImg()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Alt = Tooltip,
                Src = Source?.ToString(),
            };

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-toggle", "tooltip");
                html.AddUserAttribute("title", Tooltip);
            }

            if (Width > 0)
            {
                html.AddUserAttribute("width", Width.ToString());
            }

            if (Height > 0)
            {
                html.AddUserAttribute("height", Height.ToString());
            }

            return html;
        }
    }
}
