using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlAvatar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Avatarbild
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Users
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Liefert oder setzt einen modalen Dialag
        /// </summary>
        public ControlModal Modal { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlAvatar(IPage page, string id = null)
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
            Classes.Add("profile");

            var img = null as HtmlElement;

            if (!string.IsNullOrWhiteSpace(Image))
            {
                img = new HtmlElementMultimediaImg() { Src = Image, Class = "" };
            }
            else if (!string.IsNullOrWhiteSpace(User))
            {
                var split = User.Split(' ');
                var i = split[0].FirstOrDefault().ToString();
                i += split.Count() > 1 ? split[1].FirstOrDefault().ToString() : "";

                img = new HtmlElementTextSemanticsB(new HtmlText(i)) 
                { 
                    Class = "bg-info text-light" 
                };
            }

            var html = new HtmlElementTextContentDiv(img, new HtmlText(User))
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

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
