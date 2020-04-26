using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlToolBar : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Items { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlToolBar(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Toolitems</param>
        public ControlToolBar(IPage page, string id = null, params Control[] items)
            : this(page, id)
        {
            Add(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<Control>();
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">Die Einträge welcher hinzugefügt werden sollen</param>
        public void Add(params Control[] item)
        {
            Items.AddRange(item);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var html = new HtmlElementSectionNav()
            {
                ID = ID,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            html.Elements.AddRange
            (
                Items.Select(x => new ControlPanel(Page) 
                { 
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.One, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None) 
                }.ToHtml())
            );

            return html;
        }
    }
}
