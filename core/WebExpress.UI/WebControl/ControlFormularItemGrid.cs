using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemGrid : ControlFormularItem
    {
        /// <summary>
        /// Liefert die Items
        /// </summary>
        public ICollection<ControlFormularItem> Items { get; } = new List<ControlFormularItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemGrid(string id = null)
            : base(id)
        {
            Name = ID;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="controls">Die Steuerelemente</param>
        public ControlFormularItemGrid(string id, params ControlFormularItem[] controls)
            : this(id)
        {
            (Items as List<ControlFormularItem>).AddRange(controls);
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementTextContentDiv(Items.Select(x => x.Render(context)))
            {
                ID = ID,
                Class = Css.Concatenate("form-row", GetClasses()),
                Style = GetStyles()
            };

            return html;
        }
    }
}
