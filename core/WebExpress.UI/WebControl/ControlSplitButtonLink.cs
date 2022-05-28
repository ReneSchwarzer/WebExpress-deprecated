using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlSplitButtonLink : ControlSplitButton
    {
        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public TypeTarget Target { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlSplitButtonLink(string id)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlSplitButtonLink(string id, params IControlSplitButtonItem[] content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlSplitButtonLink(params IControlSplitButtonItem[] content)
            : base(id: null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlSplitButtonLink(string id, IEnumerable<IControlSplitButtonItem> content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlSplitButtonLink(IEnumerable<IControlSplitButtonItem> content)
            : base(id: null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Fügt ein neues Item hinzu
        /// </summary>
        /// <param name="item"></param>
        public void Add(IControlSplitButtonItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Fügt ein neuen Seterator hinzu
        /// </summary>
        public void AddSeperator()
        {
            Items.Add(null);
        }

        /// <summary>
        /// Fügt ein neuen Kopf hinzu
        /// </summary>
        /// <param name="text">Der Überschriftstext</param>
        public void AddHeader(string text)
        {
            Items.Add(new ControlSplitButtonItemHeader() { Text = text });
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Default;
            Role = "button";
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">Die Einträge welcher hinzugefügt werden sollen</param>
        public void Add(params IControlSplitButtonItem[] item)
        {
            Items.AddRange(item);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var button = new HtmlElementTextSemanticsA()
            {
                ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                Class = Css.Concatenate("btn", Css.Remove(GetClasses(), Margin.ToClass())),
                Style = GetStyles(),
                Target = Target,
                Href = Uri?.ToString(),
                OnClick = OnClick?.ToString()
            };

            if (Icon != null && Icon.HasIcon)
            {
                button.Elements.Add(new ControlIcon()
                {
                    Icon = Icon,
                    Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                    (
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.Two,
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.None
                    ) : new PropertySpacingMargin(PropertySpacing.Space.None),
                    VerticalAlignment = Icon.IsUserIcon ? TypeVerticalAlignment.TextBottom : TypeVerticalAlignment.Default
                }.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                button.Elements.Add(new HtmlText(Text));
            }

            if (Modal == null || Modal.Type == TypeModal.None)
            {

            }
            else if (Modal.Type == TypeModal.Formular)
            {
                button.OnClick = $"new webexpress.ui.modalFormularCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{Modal.Uri}', size: '{Modal.Size.ToString().ToLower()}'}});";
            }
            else if (Modal.Type == TypeModal.Brwoser)
            {
                button.OnClick = $"new webexpress.ui.modalPageCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{ Modal.Uri }', size: '{Modal.Size.ToString().ToLower()}'}});";
            }
            else if (Modal.Type == TypeModal.Modal)
            {
                button.AddUserAttribute("data-bs-toggle", "modal");
                button.AddUserAttribute("data-bs-target", "#" + Modal.Modal.ID);
            }

            var dropdownButton = new HtmlElementTextSemanticsSpan(new HtmlElementTextSemanticsSpan() { Class = "caret" })
            {
                ID = string.IsNullOrWhiteSpace(ID) ? "" : ID + "_btn",
                Class = Css.Concatenate("btn dropdown-toggle dropdown-toggle-split", Css.Remove(GetClasses(), "btn-block", Margin.ToClass())),
                Style = GetStyles()
            };
            dropdownButton.AddUserAttribute("data-bs-toggle", "dropdown");
            dropdownButton.AddUserAttribute("aria-expanded", "false");

            var dropdownElements = new HtmlElementTextContentUl
                (
                    Items.Select
                    (
                        x =>
                        x == null || x is ControlDropdownItemDivider || x is ControlLine ?
                        new HtmlElementTextContentLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownItemHeader ?
                        x.Render(context) :
                        new HtmlElementTextContentLi(x.Render(context)) { Class = "dropdown-item" }
                    )
                )
            {
                Class = HorizontalAlignment == TypeHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
            };

            var html = new HtmlElementTextContentDiv
            (
                Modal != null && Modal.Type == TypeModal.Modal ? (IHtmlNode)new HtmlList(button, Modal.Modal.Render(context)) : button,
                dropdownButton,
                dropdownElements
            )
            {
                Class = Css.Concatenate
                (
                    "btn-group",
                    Margin.ToClass(),
                    (Block == TypeBlockButton.Block ? "btn-block" : "")
                ),
                Role = Role
            };

            return html;
        }
    }
}
