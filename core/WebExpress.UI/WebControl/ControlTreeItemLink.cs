using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlTreeItemLink : ControlTreeItem
    {
        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public TypeTarget Target { get; set; }

        /// <summary>
        /// Returns or sets the tooltip.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die für den Link gültigen Parameter
        /// </summary>
        public List<Parameter> Params { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlTreeItemLink(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItemLink(string id, params Control[] content)
            : base(id, content)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItemLink(params Control[] content)
            : base(content)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItemLink(string id, List<Control> content)
            : base(id, content)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlTreeItemLink(List<Control> content)
            : base(content)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Liefert alle lokalen und temporären Parameter
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>Die Parameter</returns>
        public string GetParams(IPage page)
        {
            var dict = new Dictionary<string, Parameter>();

            // Übernahme der Parameter des Link
            if (Params != null)
            {
                foreach (var v in Params)
                {
                    if (string.IsNullOrWhiteSpace(Uri?.ToString()))
                    {
                        if (!dict.ContainsKey(v.Key.ToLower()))
                        {
                            dict.Add(v.Key.ToLower(), v);
                        }
                        else
                        {
                            dict[v.Key.ToLower()] = v;
                        }
                    }
                }
            }

            return string.Join("&amp;", from x in dict where !string.IsNullOrWhiteSpace(x.Value.Value) select x.Value.ToString());
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var param = GetParams(context?.Page);

            var link = new HtmlElementTextSemanticsA(from x in Content select x.Render(context))
            {
                ID = Id,
                Class = Css.Concatenate("link tree-link", Active == TypeActive.Active ? "tree-link-active" : ""),
                Role = Role,
                Href = Uri?.ToString() + (param.Length > 0 ? "?" + param : string.Empty),
                Target = Target,
                Title = Title,
                OnClick = OnClick?.ToString()
            };

            if (Icon != null && Icon.HasIcon)
            {
                link.Elements.Add(new ControlIcon()
                {
                    Icon = Icon,
                    Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                    (
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.Two,
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.None
                    ) : new PropertySpacingMargin(PropertySpacing.Space.None),
                    VerticalAlignment = Icon.IsUserIcon ? TypeVerticalAlignment.TextBottom : TypeVerticalAlignment.Default
                }.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                link.Elements.Add(new HtmlText(Text));
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                link.AddUserAttribute("data-toggle", "tooltip");
            }

            var expander = new HtmlElementTextSemanticsSpan
            {
                Class = Css.Concatenate("tree-treeview-expander", Children.Count > 0 ? "tree-treeview-angle" : "tree-treeview-dot")
            };

            if (Children.Count > 0 && Expand != TypeExpandTree.Collapse)
            {
                expander.Class = Css.Concatenate("tree-treeview-angle-down", expander.Class);
            }
            var container = new HtmlElementTextContentDiv(expander, link)
            {
                Class = Css.Concatenate("tree-treeview-container")
            };

            var html = new HtmlElementTextContentLi(Layout == TypeLayoutTreeItem.TreeView ? container : link)
            {
                ID = Id,
                Class = Css.Concatenate(Layout switch
                {
                    TypeLayoutTreeItem.Group => "list-group-item-action",
                    TypeLayoutTreeItem.Flush => "list-group-item-action",
                    TypeLayoutTreeItem.Horizontal => "list-group-item-action",
                    TypeLayoutTreeItem.TreeView => "tree-item",
                    _ => ""
                }, Active.ToClass()),
                Style = GetStyles(),
                Role = Role
            };

            if (Children.Count > 0)
            {
                var items = (from x in Children select x.Render(context)).ToList();
                var ul = new HtmlElementTextContentUl(items)
                {
                    Class = Css.Concatenate(Layout switch
                    {
                        TypeLayoutTreeItem.TreeView => "tree-treeview-node",
                        TypeLayoutTreeItem.Simple => "tree-simple-node",
                        _ => Layout.ToClass()
                    }, Expand.ToClass())
                };

                switch (Layout)
                {
                    case TypeLayoutTreeItem.Horizontal:
                    case TypeLayoutTreeItem.Flush:
                    case TypeLayoutTreeItem.Group:
                        items.ForEach(x => x.AddClass("list-group-item"));
                        break;
                }

                html.Elements.Add(ul);
            }

            return html;
        }
    }
}
