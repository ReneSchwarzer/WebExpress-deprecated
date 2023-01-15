using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputHidden : ControlFormularItemInput
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputHidden(string id = null)
            : base(id)
        {
            Name = Id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemInputHidden(string id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            Value = context?.Request.GetParameter(Name)?.Value;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return new HtmlElementFieldInput()
            {
                ID = Id,
                Value = Value,
                Name = Name,
                Type = "hidden",
                Role = Role
            };
        }
    }
}
