using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlLink : Control, IControlLink
    {
        /// <summary>
        /// Bestimmt ob der Link aktiv ist oder nicht
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty(TypeActive.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt ob der Link unterstrichen ist oder nicht
        /// </summary>
        public TypeTextDecoration Decoration
        {
            get => (TypeTextDecoration)GetProperty(TypeTextDecoration.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the tooltip.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the target uri.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public TypeTarget Target { get; set; }

        /// <summary>
        /// Returns or sets a modal dialogue.
        /// </summary>
        public PropertyModal Modal { get; set; } = new PropertyModal();

        /// <summary>
        /// Returns or sets the content.
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Liefert oder setzt die für den Link gültigen Parameter
        /// </summary>
        public List<Parameter> Params { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Die vertikale Ausrichtung
        /// </summary>
        public TypeVerticalAlignment VerticalAlignment
        {
            get => (TypeVerticalAlignment)GetProperty(TypeVerticalAlignment.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlLink(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The content of the html element.</param>
        public ControlLink(string id, string text)
            : this(id)
        {
            Text = text;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlLink(params Control[] content)
            : this((string)null)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlLink(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlLink(string id, List<Control> content)
            : base(id)
        {
            Content = content;
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Content = new List<Control>();
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Liefert alle lokalen und temporären Parameter
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>Die Parameter</returns>
        public string GetParams(IPage page)
        {
            var dict = new Dictionary<string, Parameter>();

            // Übernahme der Parameter des Link
            if (Params != null)
            {
                foreach (var v in Params)
                {
                    if (v.Scope == ParameterScope.Parameter)
                    {
                        if (!dict.ContainsKey(v.Key.ToLower()))
                        {
                            dict.Add(v.Key.ToLower(), v);
                        }
                        else
                        {
                            dict[v.Key.ToLower()] = v;
                        }
                    }
                }
            }

            return string.Join("&amp;", from x in dict where !string.IsNullOrWhiteSpace(x.Value.Value) select x.Value.ToString());
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

                return new HtmlList(html, Modal.Modal.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-bs-toggle", "tooltip");
            }

            return html;
        }
    }
}
