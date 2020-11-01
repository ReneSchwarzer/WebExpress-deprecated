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
        /// <param name="id">Die ID</param>
        public ControlList(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(string id, List<ControlListItem> items)
            : base(id)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add(Layout.ToClass());
            Classes.Add(HorizontalAlignment.ToClass());

            var items = (from x in Items select x.Render(context)).ToList();

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
                    html = new HtmlElementTextContentUl(items)
                    {
                        ID = ID,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role
                    };
                    break;
                default:
                    html = new HtmlElementTextContentUl(items)
                    {
                        ID = ID,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role
                    };
                    break;
            }

            return html;
        }
    }
}
