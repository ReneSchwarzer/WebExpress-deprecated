using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    public class ControlModalForm : ControlModal
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        public ControlFormular Formular { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalForm(string id = null)
            : this(id, string.Empty, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Formularname</param>
        public ControlModalForm(string id, string name)
            : this(id, name, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalForm(string id, params ControlFormularItem[] content)
            : this(id, string.Empty, content)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalForm(string id, string name, params ControlFormularItem[] content)
            : base("modal_" + id, string.Empty, content)
        {
            Formular = new ControlFormular("form_" + id, content)
            {
                EnableCancelButton = false,
                EnableSubmitAndNextButton = false
            };

            Formular.Validated += (s, e) =>
            {
                if (!e.Valid)
                {
                    ShowIfCreated = true;
                }
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (!Formular.Valid)
            {
                Fade = false;
            }

            var form = Formular.Render(context) as HtmlElementFormForm;

            Classes.Add("modal");

            if (Fade)
            {
                Classes.Add("fade");
            }

            var headerText = new HtmlElementSectionH4(Header)
            {
                Class = "modal-title"
            };

            var headerButtonLabel = new HtmlElementTextSemanticsSpan(new HtmlText("&times;"))
            {
            };

            headerButtonLabel.AddUserAttribute("aria-hidden", "true");

            var headerButton = new HtmlElementFieldButton(headerButtonLabel)
            {
                Class = "close"
            };

            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-dismiss", "modal");

            var header = new HtmlElementTextContentDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var formElements = form.Elements.Where(x => !(x is HtmlElementFieldButton));

            var body = new HtmlElementTextContentDiv(formElements)
            {
                Class = "modal-body"
            };

            var footer = null as HtmlElementTextContentDiv;

            var submitFooterButton = new ControlFormularItemButton()
            {
                Name = "submit_" + Formular?.ID?.ToLower(),
                Text = context.I18N("webexpress", "form.submit.label"),
                Icon = new PropertyIcon(TypeIcon.Save),
                Color = new PropertyColorButton(TypeColorButton.Success),
                Type = "submit",
                Value = "1",
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None)
            };
           
            var cancelFooterButton = new HtmlElementFieldButton(new HtmlText(context.I18N("webexpress", "modal.close.label")))
            {
                Type = "button",
                Class = Css.Concatenate("btn", new PropertyColorButton(TypeColorButton.Primary).ToStyle())
            };
            cancelFooterButton.AddUserAttribute("data-dismiss", "modal");

            footer = new HtmlElementTextContentDiv(submitFooterButton.Render(new RenderContextFormular(context, Formular)), cancelFooterButton)
            {
                Class = "modal-footer d-flex justify-content-between"
            };

            var content = new HtmlElementTextContentDiv(header, body, footer)
            {
                Class = "modal-content"
            };

            var dialog = new HtmlElementTextContentDiv(content)
            {
                Class = "modal-dialog",
                Role = "document"
            };

            var html = new HtmlElementTextContentDiv(dialog)
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + ID + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                context.Page.AddScript(ID + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + ID + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                context.Page.AddScript(ID + "_hidden", hidden);
            }

            if (ShowIfCreated)
            {
                var show = "$('#" + ID + "').modal('show');";
                context.Page.AddScript(ID + "_showifcreated", show);
            }

            form.Elements.Clear();
            form.Elements.Add(html);

            return form;
        }

        /// <summary>
        /// Fügt eine Formularsteuerelement hinzu
        /// </summary>
        /// <param name="item">Das Formularelement</param>
        public void Add(params ControlFormularItem[] item)
        {
            Formular.Add(item);
        }
    }
}
