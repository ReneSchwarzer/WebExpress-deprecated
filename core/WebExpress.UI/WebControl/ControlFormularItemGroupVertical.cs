using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Gruppierung von Steuerelementen
    /// </summary>
    public class ControlFormularItemGroupVertical : ControlFormularItemGroup
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemGroupVertical(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupVertical(string id, params ControlFormularItem[] items)
            : base(id, items)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupVertical(params ControlFormularItem[] items)
            : base(null, items)
        {
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
            var renderContext = new RenderContextFormularGroup(context, this);

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
            };

            foreach (var item in Items)
            {
                if (item is ControlFormularItemInput input)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(item.ID) ? item.ID + "_label" : string.Empty);
                    var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(item.ID) ? item.ID + "_help" : string.Empty);
                    var fieldset = new HtmlElementFormFieldset() { Class = "form-group" };

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    help.Text = context.I18N(input?.Help);

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("mr-2 pt-1");
                        fieldset.Elements.Add(new HtmlElementTextSemanticsSpan(icon.Render(renderContext), label.Render(renderContext))
                        {
                            Style = "display: flex;"
                        });
                    }
                    else
                    {
                        fieldset.Elements.Add(label.Render(renderContext));
                    }

                    fieldset.Elements.Add(item.Render(renderContext));

                    if (!string.IsNullOrWhiteSpace(input?.Help))
                    {
                        fieldset.Elements.Add(help.Render(renderContext));
                    }

                    html.Elements.Add(fieldset);
                }
                else
                {
                    html.Elements.Add(item?.Render(context));
                }
            }

            return html;
        }
    }
}
