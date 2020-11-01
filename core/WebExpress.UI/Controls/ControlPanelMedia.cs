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
        /// <param name="id">Die ID</param>
        public ControlPanelMedia(string id = null)
            : base(id)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="title">Die Überschrift</param>
        public ControlPanelMedia(string id, string title)
            : this(id)
        {
            Title = new ControlText(string.IsNullOrWhiteSpace(id) ? null : id + "_header")
            {
                Text = title
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("media");

            var img = new HtmlElementMultimediaImg()
            {
                Src = Image?.ToString(),
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

            var heading = new HtmlElementSectionH4(Title?.Render(context))
            {
            };

            var body = new HtmlElementTextContentDiv(Title != null ? heading : null)
            {
                Class = "media-body"
            };

            body.Elements.AddRange(from x in Content select x.Render(context));

            var html = new HtmlElementTextContentDiv(img, body)
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
