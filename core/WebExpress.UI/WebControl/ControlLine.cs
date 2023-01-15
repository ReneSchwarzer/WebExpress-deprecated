﻿using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlLine : Control, IControlSplitButtonItem, IControlDropdownItem
    {
        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new PropertyColorText TextColor { get; private set; }

        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackground BackgroundColor { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Farbe
        /// </summary>
        public PropertyColorLine Color
        {
            get => (PropertyColorLine)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlLine(string id = null)
            : base(id)
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentHr()
            {
                ID = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
