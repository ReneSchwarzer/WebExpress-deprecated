﻿using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlDropdownDivider : Control, IControlDropdownItem, IControlSplitButtonItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlDropdownDivider(string id = null)
            : base(id)
        {
            Init();
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
            Classes.Add("dropdown-divider");

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
