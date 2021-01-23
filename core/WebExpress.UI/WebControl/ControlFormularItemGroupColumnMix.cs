using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Gruppierung von Steuerelementen
    /// </summary>
    public class ControlFormularItemGroupColumnMix : ControlFormularItemGroupColumn
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemGroupColumnMix(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupColumnMix(string id, params ControlFormularItem[] items)
            : base(id, items)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupColumnMix(params ControlFormularItem[] items)
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
                Class = Css.Concatenate("form-group-column-mix", GetClasses()),
                Style = GetStyles(),
            };

            var max = 100;
            var offset = 0;

            foreach (var item in Items)
            {
                var input = item as ControlFormularItemInput;
                var div = new HtmlElementTextContentDiv() { Style = "" };
                var width = -1;

                if (Distribution.Count > offset)
                {
                    width = Distribution.Skip(offset).Take(1).FirstOrDefault();
                    div.Style = $"width: { width }%";
                    max = max - width;

                    offset++;
                }
                else if (Items.Count > offset)
                {
                    width = max / (Items.Count - offset);
                    div.Style = $"width: { width }%";
                }
                
                if (input != null)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(item.ID) ? item.ID + "_label" : string.Empty);
                    var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(item.ID) ? item.ID + "_help" : string.Empty);
                    //var fieldset = new HtmlElementFormFieldset() { Class = "form-group" };
                    var row = new HtmlElementTextContentDiv() { Class = "" };
                    var body = new HtmlElementTextContentDiv(row) { Class = "form-group" };
                    var table = new HtmlElementTextContentDiv(body) { Class = "form-group-horizontal" };

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    help.Text = context.I18N(input?.Help);

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("mr-2 pt-1");
                        row.Elements.Add(new HtmlElementTextContentDiv(icon.Render(renderContext), label.Render(renderContext))
                        {
                            Style = "display: flex;"
                        });
                    }
                    else
                    {
                        row.Elements.Add(new HtmlElementTextContentDiv(label.Render(renderContext)));
                    }

                    if (!string.IsNullOrWhiteSpace(input?.Help))
                    {
                        row.Elements.Add(new HtmlElementTextContentDiv(item.Render(renderContext), help.Render(renderContext)));
                    }
                    else
                    {
                        row.Elements.Add(new HtmlElementTextContentDiv(item.Render(renderContext)));
                    }

                    div.Elements.Add(table);
                }
                else
                {
                    div.Elements.Add(item.Render(context));
                }

                html.Elements.Add(div);
            }

            return html;
        }
    }
}
