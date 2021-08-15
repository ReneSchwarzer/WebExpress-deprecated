using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlTreeItem : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; } = new List<Control>();

        /// <summary>
        /// Liefert oder setzt die Kinderknoten
        /// </summary>
        public List<ControlTreeItem> Children { get; private set; } = new List<ControlTreeItem>();

        /// <summary>
        /// Ermittelt ob ein untergeordneter Baumknoten aktiv ist
        /// </summary>
        public bool IsAnyChildrenActive
        {
            get
            {
                if (Active == TypeActive.Active) return true;

                return Children.Where(x => x.IsAnyChildrenActive).Count() > 0;
            }
        }

        /// <summary>
        /// Liefert oder setzt die Ativitätsstatus des Listenelements
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty(TypeActive.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypeLayoutTreeItem Layout
        {
            get => (TypeLayoutTreeItem)GetProperty(TypeLayoutTreeItem.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt, ob die Kinknoten angezeigt werden oder nicht
        /// </summary>
        public TypeExpandTree Expand
        {
            get => (TypeExpandTree)GetProperty(TypeExpandTree.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTreeItem(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlTreeItem(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content.Where(x => !(x is ControlTreeItem)));
            Children.AddRange(content.Where(x => x is ControlTreeItem).Select(x => x as ControlTreeItem));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlTreeItem(params Control[] content)
            : this(null, content)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlTreeItem(string id, List<Control> content)
            : this(id, content.ToArray())
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlTreeItem(List<Control> content)
            : this(null, content)
        {
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var content = from x in Content select x.Render(context);
            var container = null as HtmlElementTextContentDiv;

            if (Layout == TypeLayoutTreeItem.TreeView)
            {
                var expander = new HtmlElementTextSemanticsSpan();
                expander.Class = Css.Concatenate("tree-treeview-expander", Children.Count > 0 ? "tree-treeview-angle" : "tree-treeview-dot");
                if (Children.Count > 0 && Expand != TypeExpandTree.Collapse)
                {
                    expander.Class = Css.Concatenate("tree-treeview-angle-down", expander.Class);
                }

                container = new HtmlElementTextContentDiv(expander, content.Count() > 1 ? new HtmlElementTextContentDiv(content) : content.FirstOrDefault());
                container.Class = Css.Concatenate("tree-treeview-container");
            }
            else
            {
                container = new HtmlElementTextContentDiv(content.Count() > 1 ? new HtmlElementTextContentDiv(content) : content.FirstOrDefault());
            }

            var html = new HtmlElementTextContentLi(container)
            {
                ID = ID,
                Class = Css.Concatenate(Active.ToClass()),
                Style = GetStyles(),
                Role = Role
            };

            if (Children.Count > 0)
            {
                var items = (from x in Children select x.Render(context)).ToList();

                switch (Layout)
                {
                    case TypeLayoutTreeItem.Horizontal:
                    case TypeLayoutTreeItem.Flush:
                    case TypeLayoutTreeItem.Group:
                        items.ForEach(x => x.AddClass("list-group-item"));
                        break;
                }

                var ul = new HtmlElementTextContentUl(items) 
                {
                    Class = Css.Concatenate(Layout switch
                    {
                        TypeLayoutTreeItem.TreeView => "tree-treeview-node",
                        TypeLayoutTreeItem.Simple => "tree-simple-node", 
                        _ => Layout.ToClass() 
                    }, Expand.ToClass())
                };
                
                html.Elements.Add(ul);
            }

            return html;
        }
    }
}
