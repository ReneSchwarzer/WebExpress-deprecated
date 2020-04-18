using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlList : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutList Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Listeneinträge
        /// </summary>
        public List<ControlListItem> Items { get; private set; }

        /// <summary>
        /// Bestimm, ob es sich um eine sotrierte oder unsortierte Liste handelt
        /// </summary>
        public bool Sorted { get; set; }

        /// <summary>
        /// Zeigt einen Rahmen an oder keinen
        /// </summary>
        public bool ShowBorder { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlList(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(IPage page, string id, List<ControlListItem> items)
            : base(page, id)
        {
            Items = items;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlListItem>();
            ShowBorder = true;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            switch (Layout)
            {
                case TypesLayoutList.Group:
                    classes.Add("list-group");
                    break;
                case TypesLayoutList.Simple:
                    classes.Add("list-unstyled");
                    break;
                case TypesLayoutList.Inline:
                    classes.Add("list-inline");
                    break;
            }

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            var items = (from x in Items select x.ToHtml()).ToList();

            if (Layout == TypesLayoutList.Group)
            {
                items.ForEach(x => x.AddClass("list-group-item"));
            }

            if (!ShowBorder)
            {
                items.ForEach(x => x.AddClass("border-0"));
            }

            var html = null as HtmlElement;

            switch (Sorted)
            {
                case true:
                    html = new HtmlElementUl(items)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                default:
                    html = new HtmlElementUl(items)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
            }

            return html;
        }
    }
}
