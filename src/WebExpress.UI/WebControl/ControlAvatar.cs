using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlAvatar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Avatarbild
        /// </summary>
        public Uri Image { get; set; }

        /// <summary>
        /// Returns or sets the name. des Users
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets a modal dialogue.
        /// </summary>
        public ControlModal Modal { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Returns or sets the id.</param>
        public ControlAvatar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var img = null as HtmlElement;

            if (Image != null)
            {
                img = new HtmlElementMultimediaImg() { Src = Image.ToString(), Class = "" };
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
                Id = Id,
                Class = Css.Concatenate("profile", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Modal != null)
            {
                html.AddUserAttribute("data-bs-toggle", "modal");
                html.AddUserAttribute("data-bs-target", "#" + Modal.Id);

                return new HtmlList(html, Modal.Render(context));
            }

            return html;
        }
    }
}
