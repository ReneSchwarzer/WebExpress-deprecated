using System.Collections.Generic;
using System.Linq;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Repräsentiert ein Paar aus einem Label und einem Formularsteuerelement
    /// </summary>
    public class ControlFormularLabelGroup : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt das Formularitem
        /// </summary>
        public ControlFormularItem Item { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularLabelGroup(IControlFormular formular, string id = null)
            : base(formular, id)
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
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            var labelClasses = new List<string>();
            if (Formular.Layout == TypesLayoutForm.Horizontal)
            {
                labelClasses.Add("col-xs-3 col-form-label mr-2");
            }

            var input = Item as ControlFormularItemInput;
            var label = Item as IControlFormularLabel;

            if (input != null && label != null)
            {
                switch (input.ValidationResult)
                {
                    case TypesInputValidity.Success:
                        labelClasses.Add("text-success");
                        break;
                    case TypesInputValidity.Warning:
                        labelClasses.Add("text-warning");
                        break;
                    case TypesInputValidity.Error:
                        labelClasses.Add("text-danger");
                        break;
                }
            }

            if (label == null || string.IsNullOrWhiteSpace(label.Label))
            {
                return Item.ToHtml();
            }

            var html = new HtmlElementFieldset()
            {
                Class = Formular.Layout == TypesLayoutForm.Horizontal ?
                    "form-group row" :
                    "form-group"
            };

            html.Elements.Add(new HtmlElementLabel(label.Label)
            {
                For = string.IsNullOrWhiteSpace(Item.ID) ? Item.ID : Item.Name,
                Class = string.Join(" ", labelClasses.Where(x => !string.IsNullOrWhiteSpace(x)))
            });

            if (Formular.Layout == TypesLayoutForm.Horizontal)
            {
                html.Elements.Add(new HtmlElementDiv(Item.ToHtml())
                {
                    Class = "col-xs-9"
                });
            }
            else
            {
                html.Elements.Add(Item.ToHtml());
            }

            if (input != null && !string.IsNullOrEmpty(input.Help))
            {
                html.Elements.Add(new HtmlElementSpan(new HtmlText(input.Help))
                {
                    Class = "form-text text-muted",
                    ID = !string.IsNullOrEmpty(Item.ID) ? Item.ID + "_help" : string.Empty
                });
            }

            return html;
        }
    }
}
