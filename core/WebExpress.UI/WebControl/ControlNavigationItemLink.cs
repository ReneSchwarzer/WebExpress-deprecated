﻿using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlNavigationItemLink : ControlLink, IControlNavigationItem
    {
        /// <summary>
        /// Verhindert den Zeilenumbruch
        /// </summary>
        public bool NoWrap { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlNavigationItemLink(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = base.Render(context);

            if (NoWrap)
            {
                html.AddClass("text-nowrap");
            }

            return html;
        }
    }
}
