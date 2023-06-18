using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Box with superimposed tools.
    /// </summary>
    public class ControlPanelTool : ControlPanel
    {
        /// <summary>
        /// Returns or sets the tools
        /// </summary>
        public ControlDropdown Tools { get; } = new ControlDropdown();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelTool(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelTool(string id, params Control[] items)
            : base(id, items)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelTool(params Control[] items)
            : base(items)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Border = new PropertyBorder(true);
        }

        /// <summary>
        /// Adds controls to the panel.
        /// </summary>
        /// <param name="items">The controls to insert.</param>
        public void Add(params Control[] items)
        {
            Content.AddRange(items);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var dropDown = Tools.Render(context);
            var content = new HtmlElementTextContentDiv(Content.Select(x => x.Render(context)));

            var html = new HtmlElementTextContentDiv(dropDown, content)
            {
                Id = Id,
                Class = Css.Concatenate("toolpanel", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
