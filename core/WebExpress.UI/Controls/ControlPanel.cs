using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanel : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; } = new List<Control>();

        /// <summary>
        /// Liefert oder setzt die Anordnung des Inhaltes
        /// </summary>
        public TypesFlexboxDirection Direction
        {
            get => (TypesFlexboxDirection)GetProperty(TypesFlexboxDirection.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanel(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanel(IPage page, params Control[] content)
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
        public ControlPanel(IPage page, string id, params Control[] content)
            : this(page, id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanel(IPage page, string id, IEnumerable<Control> content)
            : this(page, id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanel(IPage page, string id, List<Control> content)
            : base(page, id)
        {
            Content = content;
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
            return new HtmlElementTextContentDiv(from x in Content select x.ToHtml())
            {
                ID = ID,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
