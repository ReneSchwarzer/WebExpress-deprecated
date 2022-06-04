using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlModalFormular : ControlModal
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        public ControlFormular Formular { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormular(string id = null)
            : this(id, string.Empty, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        public ControlModalFormular(string id, string header)
            : this(id, header, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormular(string id, params ControlFormularItem[] content)
            : this(id, string.Empty, content)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormular(string id, string header, params ControlFormularItem[] content)
            : base("modal_" + id, string.Empty, content)
        {
            Formular = new ControlFormular();
            Formular.InitializeFormular += OnInitializeFormular;

            Formular.Validated += OnValidatedFormular;
        }

        /// <summary>
        /// Aufruf erfolgt, wenn das Formular initialisiert wird.
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            ShowIfCreated = false;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular validiert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnValidatedFormular(object sender, ValidationResultEventArgs e)
        {
            if (!e.Valid)
            {
                ShowIfCreated = true;
                Fade = false;
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var fade = Fade;
            var classes = Classes.ToList();

            var form = Formular.Render(context) as HtmlElementFormForm;

            classes.Add("modal");

            if (Fade)
            {
                classes.Add("fade");
            }

            var headerText = new HtmlElementSectionH4(I18N(context.Culture, Header))
            {
                Class = "modal-title"
            };

            var headerButton = new HtmlElementFieldButton()
            {
                Class = "btn-close"
            };
            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-bs-dismiss", "modal");

            var header = new HtmlElementTextContentDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var formElements = form.Elements.Where(x => !(x is HtmlElementSectionFooter));

            var body = new HtmlElementTextContentDiv(formElements)
            {
                Class = "modal-body"
            };

            var footer = null as HtmlElementTextContentDiv;

            var submitFooterButton = new ControlFormularItemButton()
            {
                Name = "submit_" + Formular?.Id?.ToLower(),
                Text = Formular.SubmitButton.Text,
                Icon = Formular.SubmitButton.Icon,
                Color = Formular.SubmitButton.Color,
                Type = TypeButton.Submit,
                Value = "1",
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None),
                OnClick = new PropertyOnClick($"$('#{Formular?.SubmitType.Id}').val('submit');")
            };

            var cancelFooterButton = new ControlButtonLink()
            {
                Text = I18N(context.Culture, "webexpress.ui:modal.close.label")
            }.Render(context) as HtmlElement;

            cancelFooterButton.AddUserAttribute("data-bs-dismiss", "modal");

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
                ID = Id,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + Id + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                context.VisualTree.AddScript(Id + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + Id + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                context.VisualTree.AddScript(Id + "_hidden", hidden);
            }

            if (ShowIfCreated)
            {
                var show = "$('#" + Id + "').modal('show');";
                context.VisualTree.AddScript(Id + "_showifcreated", show);
            }

            form.Elements.Clear();
            form.Elements.Add(html);

            Fade = fade;

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
