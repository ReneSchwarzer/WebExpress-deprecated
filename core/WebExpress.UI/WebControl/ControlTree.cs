using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlTree : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypeLayoutTree Layout
        {
            get => (TypeLayoutTree)GetProperty(TypeLayoutTree.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Baumknoten
        /// </summary>
        public List<ControlTreeItem> Items { get; private set; } = new List<ControlTreeItem>();

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
        public ControlTree(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlTree(string id, params ControlTreeItem[] items)
            : base(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlTree(params ControlTreeItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlTree(string id, List<ControlTreeItem> items)
            : base(id)
        {
            Items = items;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlTree(List<ControlTreeItem> items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            ShowBorder = true;
        }

        /// <summary>
        /// Fügt Listeneinträge hinzu
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public void Add(IEnumerable<ControlTreeItem> items)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Fügt Listeneinträge hinzu
        /// </summary>
        /// <param name="item">Der Listeneintrag</param>
        public void Add(ControlTreeItem item)
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
            var items = (from x in Items select x.Render(context)).ToList();

            switch (Layout)
            {
                case TypeLayoutTree.Horizontal:
                case TypeLayoutTree.Flush:
                case TypeLayoutTree.Group:
                    items.ForEach(x => x.AddClass("list-group-item"));
                    break;
            }

            var html = new HtmlElementTextContentUl(items)
            {
                ID = Id,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Layout == TypeLayoutTree.TreeView)
            {
                context.VisualTree.AddScript("treeview", @"var toggler = document.getElementsByClassName(""tree-treeview-angle"");
                for (var i = 0; i < toggler.length; i++)
                {
                    toggler[i].addEventListener(""click"", function() {
                        this.parentElement.parentElement.querySelector("".tree-treeview-node"").classList.toggle(""tree-node-hide"");
                        this.classList.toggle(""tree-treeview-angle-down"");
                        
                    });
            }
            ");
            }

            return html;
        }
    }
}
