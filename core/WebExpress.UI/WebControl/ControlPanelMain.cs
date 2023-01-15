﻿using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlPanelMain : ControlPanel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelMain(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementSectionMain()
            {
                ID = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            html.Elements.AddRange(from x in Content select x?.Render(context));

            return html;
        }
    }
}
