using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Repräsentiert ein Paar aus einem Label und einem Formularsteuerelement
    /// </summary>
    public class ControlFormularItemLabelGroup : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt das Formularitem
        /// </summary>
        public ControlFormularItem Item { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemLabelGroup(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        ///<param name="item">Das Formularitem</param> 
        public ControlFormularItemLabelGroup(string id, ControlFormularItem item)
            : base(id)
        {
            Item = item;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        ///<param name="item">Das Formularitem</param> 
        public ControlFormularItemLabelGroup(ControlFormularItem item)
            : base(null)
        {
            Item = item;
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
            var form = (context as RenderContextFormular)?.Formular;
            var input = Item as ControlFormularItemInput;

            var icon = new ControlIcon() { Icon = (Item as ControlFormularItemInput)?.Icon };
            var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(Item.ID) ? Item.ID + "_label" : string.Empty) { Text = context.I18N(input?.Label), FormularItem = Item };
            var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(Item.ID) ? Item.ID + "_help" : string.Empty) { Text = context.I18N(input?.Help) };
            label.Initialize(context);
            help.Initialize(context);

            switch (form.Layout)
            {
                case TypeLayoutFormular.Horizontal:
                    icon.Classes.Add("col-form-label ml-3");
                    label.Classes.Add("col-form-label");
                    label.GridColumn = new PropertyGrid(TypeDevice.Small, 2);
                    help.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);

                    break;
                case TypeLayoutFormular.Inline:
                    icon.Classes.Add("ml-3 mr-2");
                    //label.Classes.Add("mr-2");
                    help.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);

                    break;
            }

            if (input != null && label != null)
            {
                switch (input.ValidationResult)
                {
                    case TypesInputValidity.Success:
                        icon.TextColor = new PropertyColorText(TypeColorText.Success);
                        label.TextColor = new PropertyColorText(TypeColorText.Success);
                        break;
                    case TypesInputValidity.Warning:
                        icon.TextColor = new PropertyColorText(TypeColorText.Warning);
                        label.TextColor = new PropertyColorText(TypeColorText.Warning);
                        break;
                    case TypesInputValidity.Error:
                        icon.TextColor = new PropertyColorText(TypeColorText.Danger);
                        label.TextColor = new PropertyColorText(TypeColorText.Danger);
                        break;
                }
            }

            if (string.IsNullOrWhiteSpace(label.Text))
            {
                return Item?.Render(context);
            }

            var html = new HtmlElementFormFieldset()
            {
                Class = Css.Concatenate(form?.Layout == TypeLayoutFormular.Horizontal ?
                    "form-group row" :
                    "form-group", GetClasses()),
                Style = GetStyles()
            };

            if (icon.Icon != null)
            {
                html.Elements.Add(icon.Render(context));
            }
            else
            {
                //labelClasses.Add("ml-3");
            }

            html.Elements.Add(label.Render(context));

            //    new HtmlElementFieldLabel(label.Label)
            //{
            //    For = string.IsNullOrWhiteSpace(Item.ID) ? Item.ID : Item.Name,
            //    Class = string.Join(" ", labelClasses.Where(x => !string.IsNullOrWhiteSpace(x)))
            //});

            if (form?.Layout == TypeLayoutFormular.Horizontal)
            {
                html.Elements.Add(new HtmlElementTextContentDiv(Item.Render(context))
                {
                    Class = "col-sm-7"
                });
            }
            else
            {
                html.Elements.Add(Item.Render(context));
            }

            if (!string.IsNullOrEmpty(help.Text))
            {
                html.Elements.Add(help.Render(context));
            }

            return html;
        }
    }
}
