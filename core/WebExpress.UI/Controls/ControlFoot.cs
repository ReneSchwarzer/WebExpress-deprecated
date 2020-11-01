using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlFoot : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFoot(string id = null)
            : base(id)
        {
            Content = new List<Control>();

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlFoot(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlFoot(string id, List<Control> content)
            : base(id)
        {
            Init();

            Content = content;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Content.Add(new ControlPanel(new ControlLine()));
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return new HtmlElementSectionFooter(from x in Content select x.Render(context))
            {
                ID = ID,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
