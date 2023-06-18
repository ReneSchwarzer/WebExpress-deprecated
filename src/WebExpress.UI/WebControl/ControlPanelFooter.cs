using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlPanelFooter : ControlPanel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelFooter(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanelFooter(string id, params Control[] content)
            : base(id, content)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanelFooter(string id, List<Control> content)
            : base(id, content)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Content.Add(new ControlPanel(new ControlLine()));
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return new HtmlElementSectionFooter(from x in Content select x.Render(context))
            {
                Id = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
