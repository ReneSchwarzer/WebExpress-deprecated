using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlPanelFlexbox : ControlPanel
    {
        /// <summary>
        /// Bestimmt, ob die Items inline angezeigt werden sollen
        /// </summary>
        public virtual TypeLayoutFlexbox Layout
        {
            get => (TypeLayoutFlexbox)GetProperty(TypeLayoutFlexbox.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt, ob die horizentale Ausrichtung der Items
        /// </summary>
        public virtual TypeJustifiedFlexbox Justify
        {
            get => (TypeJustifiedFlexbox)GetProperty(TypeJustifiedFlexbox.Start);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt, ob die vertikale Ausrichtung der Items
        /// </summary>
        public virtual TypeAlignFlexbox Align
        {
            get => (TypeAlignFlexbox)GetProperty(TypeAlignFlexbox.Start);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt, das Überlaufverhalten der Items
        /// </summary>
        public virtual TypeWrap Wrap
        {
            get => (TypeWrap)GetProperty(TypeWrap.Nowrap);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlPanelFlexbox(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="content">Die Listeneinträge</param>
        public ControlPanelFlexbox(string id, params IControl[] content)
            : base(id, content)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Die Listeneinträge</param>
        public ControlPanelFlexbox(params IControl[] content)
            : base(null, content)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlPanelFlexbox(string id, IEnumerable<IControl> content)
            : base(id, content)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Listeneinträge</param>
        public ControlPanelFlexbox(IEnumerable<IControl> content)
            : base(null, content)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            html.Elements.AddRange(Content.Select(x => x.Render(context)));

            return html;
        }
    }
}
