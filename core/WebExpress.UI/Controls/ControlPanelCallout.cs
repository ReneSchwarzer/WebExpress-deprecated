using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelCallout : ControlPanel
    {
        /// <summary>
        /// Liefert oder sezt den Titel
        /// </summary>
        public ControlText Title { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelCallout(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelCallout(IPage page, params Control[] content)
            : this(page)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelCallout(IPage page, string id, params Control[] content)
            : this(page, id)
        {
            Content.AddRange(content);
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
            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = Css.Concatenate("callout", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Title != null)
            {
                html.Elements.Add(Title.ToHtml());
            }

            html.Elements.AddRange(from x in Content select x.ToHtml());

            return html;
        }
    }
}
