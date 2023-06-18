using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlList : Control
    {
        /// <summary>
        /// Returns or sets the layout.
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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlList(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(string id, params ControlListItem[] items)
            : base(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(params ControlListItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(string id, List<ControlListItem> items)
            : base(id)
        {
            Items = items;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlList(List<ControlListItem> items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initialization
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var items = Items.Where(x => x.Enable).Select(x => x.Render(context)).ToList();

            switch (Layout)
            {
                case TypeLayoutList.Horizontal:
                case TypeLayoutList.Flush:
                case TypeLayoutList.Group:
                    items.ForEach(x => x.AddClass("list-group-item"));
                    break;
            }

            var html = new HtmlElementTextContentUl(items)
            {
                Id = Id,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
