using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlSplitButton : Control, IControlButton
    {
        /// <summary>
        /// Returns or sets the color. der Schaltfläche
        /// </summary>
        public new PropertyColorButton BackgroundColor
        {
            get => (PropertyColorButton)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(Outline), () => value?.ToStyle(Outline));
        }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the outline property
        /// </summary>
        public bool Outline { get; set; }

        /// <summary>
        /// Returns or sets whether the button should take up the full width.
        /// </summary>
        public TypeBlockButton Block
        {
            get => (TypeBlockButton)GetProperty(TypeBlockButton.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns or sets the activation status of the button.
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty(TypeActive.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Returns or sets a modal dialogue.
        /// </summary>
        public PropertyModal Modal { get; set; } = new PropertyModal();

        /// <summary>
        /// Returns or sets the content.
        /// </summary>
        protected List<IControlSplitButtonItem> Items { get; private set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlSplitButton(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">The content of the html element.</param>
        public ControlSplitButton(params IControlSplitButtonItem[] items)
            : base(null)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="items">The content of the html element.</param>
        public ControlSplitButton(string id, params IControlSplitButtonItem[] items)
            : base(id)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="items">The content of the html element.</param>
        public ControlSplitButton(string id, IEnumerable<IControlSplitButtonItem> items)
            : base(id)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">The content of the html element.</param>
        public ControlSplitButton(IEnumerable<IControlSplitButtonItem> items)
            : base(null)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Default;
            //BackgroundColor = LayoutSchema.ButtonBackground;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var button = new HtmlElementFieldButton()
            {
                Id = string.IsNullOrWhiteSpace(Id) ? "" : Id + "_btn",
                Class = Css.Concatenate("btn", Css.Remove(GetClasses(), Margin.ToClass())),
                Style = GetStyles()
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
                button.OnClick = $"new webexpress.ui.modalFormCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{Modal.Uri}', size: '{Modal.Size.ToString().ToLower()}', redirect: '{Modal.RedirectUri}'}});";
            }
            else if (Modal.Type == TypeModal.Brwoser)
            {
                button.OnClick = $"new webexpress.ui.modalPageCtrl({{ close: '{InternationalizationManager.I18N(context.Culture, "webexpress.ui:form.cancel.label")}', uri: '{Modal.Uri}', size: '{Modal.Size.ToString().ToLower()}', redirect: '{Modal.RedirectUri}'}});";
            }
            else if (Modal.Type == TypeModal.Modal)
            {
                button.AddUserAttribute("data-bs-toggle", "modal");
                button.AddUserAttribute("data-bs-target", "#" + Modal.Modal.Id);
            }

            var dropdownButton = new HtmlElementFieldButton(new HtmlElementTextSemanticsSpan() { Class = "caret" })
            {
                Id = string.IsNullOrWhiteSpace(Id) ? "" : Id + "_btn",
                Class = Css.Concatenate("btn dropdown-toggle dropdown-toggle-split", Css.Remove(GetClasses(), "btn-block", Margin.ToClass())),
                Style = GetStyles(),
                DataToggle = "dropdown"
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
                Modal != null && Modal.Type == TypeModal.Modal ? new HtmlList(button, Modal.Modal.Render(context)) : button,
                dropdownButton,
                dropdownElements
            )
            {
                Class = Css.Concatenate
                (
                    "btn-group ",
                    Margin.ToClass(),
                    (Block == TypeBlockButton.Block ? "btn-block" : "")
                ),
                Role = Role
            };

            return html;
        }
    }
}
