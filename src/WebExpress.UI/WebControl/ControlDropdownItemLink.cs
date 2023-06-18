using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlDropdownItemLink : ControlLink, IControlDropdownItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlDropdownItemLink(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var param = GetParams(context?.Page);

            var html = new HtmlElementTextSemanticsA(from x in Content select x.Render(context))
            {
                Id = Id,
                Class = Css.Concatenate("link", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Href = Uri?.ToString() + (param.Length > 0 ? "?" + param : string.Empty),
                Target = Target,
                Title = InternationalizationManager.I18N(context.Culture, Title),
                OnClick = OnClick?.ToString()
            };

            if (Icon != null && Icon.HasIcon)
            {
                html.Elements.Add(new ControlIcon()
                {
                    Icon = Icon,
                    Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                    (
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.Two,
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.None
                    ) : new PropertySpacingMargin(PropertySpacing.Space.None)
                }.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(InternationalizationManager.I18N(context.Culture, Text)));
            }

            if (Modal == null || Modal.Type == TypeModal.None)
            {

            }
            else if (Modal.Type == TypeModal.Formular)
            {
                html.OnClick = $"new webexpress.ui.modalFormularCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{Modal.Uri?.ToString() ?? html.Href}', size: '{Modal.Size.ToString().ToLower()}', redirect: '{Modal.RedirectUri}'}});";
                html.Href = "#";
            }
            else if (Modal.Type == TypeModal.Brwoser)
            {
                html.OnClick = $"new webexpress.ui.modalPageCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{Modal.Uri?.ToString() ?? html.Href}', size: '{Modal.Size.ToString().ToLower()}', redirect: '{Modal.RedirectUri}'}});";
                html.Href = "#";
            }
            else if (Modal.Type == TypeModal.Modal)
            {
                html.AddUserAttribute("data-bs-toggle", "modal");
                html.AddUserAttribute("data-bs-target", "#" + Modal.Modal.Id);
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-bs-toggle", "tooltip");
            }

            return html;
        }
    }
}
