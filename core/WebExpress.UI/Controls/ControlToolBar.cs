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
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutCard Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anordnung
        /// </summary>
        public TypesFlexboxDirection Direction { get; set; }

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
            var classes = new List<string>
            {
                Class,
                //"d-flex",
                Layout.ToClass(),
                Direction.ToClass()
            };

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            var html = new HtmlElementNav()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style
            };

            html.Elements.AddRange(Items.Select(x => x.ToHtml().AddClass("mr-1")));

            return html;
        }
    }
}
