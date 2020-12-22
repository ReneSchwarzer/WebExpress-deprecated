using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlList : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypeLayoutList Layout
        {
            get => (TypeLayoutList)GetProperty(TypeLayoutList.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Listeneinträge
        /// </summary>
        public List<ControlListItem> Items { get; private set; } = new List<ControlListItem>();

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
        public ControlList(string id, params ControlListItem[] items)
            : base(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(params ControlListItem[] items)
            : this(null, items)
        {
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
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(List<ControlListItem> items)
            : this(null, items)
        {
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
        /// Fügt Listeneinträge hinzu
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public void Add(IEnumerable<ControlListItem> items)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Fügt Listeneinträge hinzu
        /// </summary>
        /// <param name="item">Der Listeneintrag</param>
        public void Add(ControlListItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            //Classes.Add(HorizontalAlignment.ToClass());

            var items = (from x in Items select x.Render(context)).ToList();

            switch (Layout)
            {
                case TypeLayoutList.Horizontal:
                case TypeLayoutList.Flush:
                case TypeLayoutList.Group:
                    items.ForEach(x => x.AddClass("list-group-item"));
                    break;
            }

            if (!ShowBorder)
            {
                //items.ForEach(x => x.AddClass("border-0"));
            }


            var html = new HtmlElementTextContentUl(items)
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
