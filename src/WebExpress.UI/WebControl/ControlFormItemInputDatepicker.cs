using WebExpress.WebHtml;
using WebExpress.WebComponent;
using WebExpress.WebUri;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputDatepicker : ControlFormItemInput
    {
        /// <summary>
        /// Determines whether the control is automatically initialized.
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the minimum length.
        /// </summary>
        public string MinLength { get; set; }

        /// <summary>
        /// Returns or sets the maximum length.
        /// </summary>
        public string MaxLength { get; set; }

        /// <summary>
        /// Returns or sets whether inputs are enforced.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Returns or sets a search pattern that checks the content.
        /// </summary>
        public string Pattern { get; set; }

        ///// <summary>
        ///// Returns the initialization code (JQuerry).
        ///// </summary>
        //public string InitializeCode => "$('#" + Id + " input').datepicker({ startDate: -3 });";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemInputDatepicker(string id = null)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "datepicker")
        {
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            AutoInitialize = true;

            Value = context?.Request.GetParameter(Name)?.Value;

            var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, typeof(Module));
            if (module != null)
            {
                context.VisualTree.HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "/assets/js/bootstrap-datepicker.min.js"));
                context.VisualTree.HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "/assets/js/locales_datepicker/bootstrap-datepicker." + context.Culture.TwoLetterISOLanguageName.ToLower() + ".min.js"));
                context.VisualTree.CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/bootstrap-datepicker3.min.css"));
            }

            context.AddScript(Id, @"$('#" + Id + @"').datepicker({format: ""dd.mm.yyyy"", todayBtn: true, language: ""de"", zIndexOffset: 999});");
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {


            //if (Disabled)
            //{
            //    Classes.Add("disabled");
            //}

            //if (AutoInitialize)
            //{
            //    context.Page.AddScript(Id, InitializeCode);
            //    AutoInitialize = false;
            //}

            var input = new HtmlElementFieldInput()
            {
                Id = Id,
                Name = Name,
                Type = "text",
                Class = "form-control",
                Value = Value
            };

            //var span = new HtmlElementTextSemanticsSpan()
            //{
            //    Class = TypeIcon.Calendar.ToClass()
            //};

            //var div = new HtmlElementTextContentDiv(span)
            //{
            //    Class = "input-group-text"
            //};

            //var html = new HtmlElementTextContentDiv(input, div)
            //{
            //    Id = Id,
            //    Class = "input-group",
            //    //DataProvide = "datepicker"
            //};

            return input;
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);
        }
    }
}
