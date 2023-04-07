using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlTag : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypeColorBackgroundBadge Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt ob abgerundete Ecken verwendet werden soll
        /// </summary>
        public bool Pill { get; set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Items { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTag(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTag(string id, params Control[] content)
            : this(id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTag(string id, IEnumerable<Control> content)
            : this(id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Pill = true;
            Items = new List<Control>();
        }

        /// <summary>
        /// Fügt ein neues Item hinzu
        /// </summary>
        /// <param name="item"></param>
        public void Add(Control item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Fügt ein neuen Seterator hinzu
        /// </summary>
        public void AddSeperator()
        {
            Items.Add(null);
        }

        /// <summary>
        /// Fügt ein neuen Kopf hinzu
        /// </summary>
        /// <param name="text">Der Überschriftstext</param>
        public void AddHeader(string text)
        {
            Items.Add(new ControlDropdownItemHeader() { Text = text });
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {

            if (Pill)
            {
                Classes.Add("badge-pill");
            }

            if (Items.Count == 0)
            {
                return new HtmlElementTextSemanticsSpan(new HtmlText(Text))
                {
                    Class = Css.Concatenate("badge", GetClasses()),
                    Style = GetStyles(),
                    Role = Role
                };
            }

            Classes.Add("btn");

            var html = new HtmlElementTextSemanticsSpan()
            {
                ID = Id,
                Class = "dropdown"
            };

            var tag = new HtmlElementTextSemanticsSpan
            (
                new HtmlText(Text), new HtmlElementTextSemanticsSpan()
                {
                    Class = "fas fa-caret-down"
                }
            )
            {
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                DataToggle = "dropdown"
            };

            html.Elements.Add(tag);
            html.Elements.Add
            (
                new HtmlElementTextContentUl
                (
                    Items.Select
                    (
                        x =>
                        x == null ?
                        new HtmlElementTextContentLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownItemHeader ?
                        x.Render(context) :
                        new HtmlElementTextContentLi(x.Render(context).AddClass("dropdown-item")) { }
                    )
                )
                {
                    Class = HorizontalAlignment == TypeHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
                }
            );

            return html;
        }
    }
}
