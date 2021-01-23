using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputHidden : ControlFormularItemInput
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputHidden(string id = null)
            : base(id)
        {
            Name = ID;
        }

        /// <summary>
        /// Konstruktor
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (context.Page.HasParam(Name))
            {
                Value = context?.Page.GetParamValue(Name);
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return new HtmlElementFieldInput()
            {
                ID = ID,
                Value = Value,
                Name = Name,
                Type = "hidden",
                Role = Role
            };
        }
    }
}
