using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlButtonLink : ControlButton
    {
        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlButtonLink(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypesSize.Default;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            Classes.Add("btn");
            Classes.Add(Layout.ToClass(Outline));
            Classes.Add(Size.ToClass());

            if (Block)
            {
                Classes.Add("btn-block");
            }

            var html = new HtmlElementTextSemanticsA()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Href = Uri?.ToString()
            };

            if (Icon != Icon.None && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementTextSemanticsSpan()
                {
                    ID = ID + "_icon",
                    Class = Icon.ToClass()
                });

                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
            }
            else if (Icon != Icon.None && string.IsNullOrWhiteSpace(Text))
            {
                html.AddClass(Icon.ToClass());
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.ToHtml()));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.ToHtml());
            }

            return html;
        }
    }
}
