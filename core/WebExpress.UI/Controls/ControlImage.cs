using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlImage : Control
    {
        /// <summary>
        /// Liefert oder setzt die Bildquelle
        /// </summary>
        public Path Source { get; set; }

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
        public ControlImage(IPage page, string id, Path source)
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
            var classes = new List<string>
            {
                Class
            };

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            var html = new HtmlElementImg()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
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
