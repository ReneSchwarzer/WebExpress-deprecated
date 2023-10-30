using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlTreeItem : Control
    {
        /// <summary>
        /// Returns or sets the content.
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

                return Children.Where(x => x.IsAnyChildrenActive).Any();
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
        /// Returns or sets the layout.
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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlTreeItem(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItem(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content.Where(x => !(x is ControlTreeItem)));
            Children.AddRange(content.Where(x => x is ControlTreeItem).Select(x => x as ControlTreeItem));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItem(params Control[] content)
            : this(null, content)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItem(string id, List<Control> content)
            : this(id, content.ToArray())
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItem(List<Control> content)
            : this(null, content)
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var content = from x in Content select x.Render(context);
            var container = default(HtmlElementTextContentDiv);

            if (Layout == TypeLayoutTreeItem.TreeView)
            {
                var expander = new HtmlElementTextSemanticsSpan
                {
                    Class = Css.Concatenate("tree-treeview-expander", Children.Count > 0 ? "tree-treeview-angle" : "tree-treeview-dot")
                };

                if (Children.Count > 0 && Expand != TypeExpandTree.Collapse)
                {
                    expander.Class = Css.Concatenate("tree-treeview-angle-down", expander.Class);
                }

                container = new HtmlElementTextContentDiv(expander, content.Count() > 1 ? new HtmlElementTextContentDiv(content) : content.FirstOrDefault())
                {
                    Class = Css.Concatenate("tree-treeview-container")
                };
            }
            else
            {
                container = new HtmlElementTextContentDiv(content.Count() > 1 ? new HtmlElementTextContentDiv(content) : content.FirstOrDefault());
            }

            var html = new HtmlElementTextContentLi(container)
            {
                Id = Id,
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
