using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelMedia : ControlPanel
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public Control Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die URL zum Bild
        /// </summary>
        public IUri Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weite des Bildes in Pixel
        /// </summary>
        public int ImageWidth { get; set; } = -1;

        /// <summary>
        /// Liefert oder setzt die Höhe des Bildes in Pixel
        /// </summary>
        public int ImageHeight { get; set; } = -1;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelMedia(IPage page, string id = null)
            : base(page, id)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="title">Die Überschrift</param>
        public ControlPanelMedia(IPage page, string id, string title)
            : this(page, id)
        {
            Title = new ControlText(page, string.IsNullOrWhiteSpace(id) ? null : id + "_header")
            {
                Text = title
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "media"
            };

            var img = new HtmlElementImg()
            {
                Src = Image.ToString(),
                Class = "mr-3 mt-3 rounded-circle"
            };

            if (ImageWidth > -1)
            {
                img.Width = ImageWidth;
            }

            if (ImageHeight > -1)
            {
                img.Height = ImageHeight;
            }

            var heading = new HtmlElementH4(Title?.ToHtml())
            {
            };

            var body = new HtmlElementDiv(Title != null ? heading : null)
            {
                Class = "media-body"
            };

            body.Elements.AddRange(from x in Content select x.ToHtml());

            var html = new HtmlElementDiv(img, body)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            return html;
        }
    }
}
