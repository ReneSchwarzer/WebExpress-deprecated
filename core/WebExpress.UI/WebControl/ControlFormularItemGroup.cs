using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Repräsentiert ein Paar aus einem Label und einem Formularsteuerelement
    /// </summary>
    public class ControlFormularItemGroup : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt die Formularitems
        /// </summary>
        public ICollection<ControlFormularItem> Items { get; } = new List<ControlFormularItem>();

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public virtual TypeLayoutFormular Layout
        {
            get => (TypeLayoutFormular)GetProperty(TypeLayoutFormular.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemGroup(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        ///<param name="item">Das Formularitem</param> 
        public ControlFormularItemGroup(string id, params ControlFormularItem[] item)
            : base(id)
        {
            (Items as List<ControlFormularItem>).AddRange(item);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        ///<param name="item">Das Formularitem</param> 
        public ControlFormularItemGroup(params ControlFormularItem[] item)
            : base(null)
        {
            (Items as List<ControlFormularItem>).AddRange(item);
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            var grpupContex = new RenderContextFormularGroup(context, this);

            foreach (var item in Items)
            {
                item.Initialize(grpupContex);
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var form = (context as RenderContextFormular)?.Formular;

            

            var groupClass = string.Empty;
            var lableClass = string.Empty;
            var itemClass = string.Empty;

            switch (form.Layout)
            {
                case TypeLayoutFormular.Horizontal:
                    groupClass = "form-group row";
                    lableClass = "col-form-label col-sm-2";
                    break;
                case TypeLayoutFormular.Inline:
                    groupClass = "form-group";
                    break;
                default:
                    groupClass = "form-group";
                    break;
            }

            switch (Layout)
            {
                case TypeLayoutFormular.Horizontal:
                    itemClass = "col-sm-7 ml-0 row";
                    break;
                case TypeLayoutFormular.Inline:
                    itemClass = "col-sm-7 ml-0";
                    break;
                case TypeLayoutFormular.Mix:
                    itemClass = "col-sm-7 ml-0 row";
                    break;
                default:
                    itemClass = "col-sm-7 ml-0";
                    break;
            }

            var label = new HtmlElementTextContentDiv()
            {
                Class = lableClass
            };

            var items = new HtmlElementTextContentDiv(Items.Select(x => new ControlFormularItemLabelGroup(x).Render(new RenderContextFormularGroup(context, this))))
            {
                Class = itemClass
            };

            var html = new HtmlElementTextContentDiv(label, items)
            {
                ID = ID,
                Class = Css.Concatenate(groupClass, GetClasses()),
                Style = GetStyles(),
            };

            return html;
        }
    }
}
