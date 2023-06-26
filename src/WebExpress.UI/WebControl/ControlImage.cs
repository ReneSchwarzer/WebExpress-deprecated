﻿using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlImage : Control
    {
        /// <summary>
        /// Liefert oder setzt die Bildquelle
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Returns or sets the width.
        /// </summary>
        public new int Width { get; set; }

        /// <summary>
        /// Returns or sets the height.
        /// </summary>
        public new int Height { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlImage(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="source">Die Bildquelle</param>
        public ControlImage(string id, string source)
            : base(id)
        {
            Uri = source;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add(HorizontalAlignment.ToClass());

            var html = new HtmlElementMultimediaImg()
            {
                Id = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Alt = Tooltip,
                Src = Uri?.ToString(),
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