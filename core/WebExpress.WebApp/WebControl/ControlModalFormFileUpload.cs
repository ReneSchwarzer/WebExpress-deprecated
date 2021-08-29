using System;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormFileUpload : ControlModalForm
    {
        /// <summary>
        /// Event wird ausgelöst, wenn das Hochladen bestätigt wurde
        /// </summary>
        public event EventHandler Upload;

        /// <summary>
        /// Liefert oder setzt das Dokument
        /// </summary>
        public ControlFormularItemInputFile File { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon ButtonIcon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe der Schaltfläche
        /// </summary>
        public PropertyColorButton ButtonColor { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschriftung der Schaltfläche
        /// </summary>
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlFormularItem Prologue { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlFormularItem Epilogue { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Uri
        /// </summary>
        public IUri RedirectUri { get { return Formular?.RedirectUri; } set { Formular.RedirectUri = value; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormFileUpload(string id = null)
            : this(id, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormFileUpload(string id, params ControlFormularItem[] content)
            : base(id, string.Empty, content)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Formular.ProcessFormular += (s, e) =>
            {
                OnUpload();
            };
        }

        /// <summary>
        /// Löst das Upload-Event aus
        /// </summary>
        protected virtual void OnUpload()
        {
            Upload?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            File = new ControlFormularItemInputFile()
            {
                Name = "file",
                //Label = "inventoryexpress.media.form.image.label",
                Help = context.Page.I18N("webexpress.webapp", "fileupload.file.description"),
                //Icon = new PropertyIcon(TypeIcon.Image),
                //AcceptFile = new string[] { "image/*, video/*, audio/*, .pdf, .doc, .docx, .txt" },
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            };

            File.Validation += (s, e) =>
            {
                if (!((context.Page as Resource).GetParam((s as ControlFormularItemInputFile).Name) is ParameterFile))
                {
                    e.Results.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = context.Page.I18N("webexpress.webapp", "fileupload.file.validation.error.nofile") });
                }
            };

            if (string.IsNullOrWhiteSpace(Header))
            {
                Header = context.Page.I18N("webexpress.webapp", "fileupload.header");
            }
            
            if (string.IsNullOrWhiteSpace(ButtonLabel))
            {
                ButtonLabel = context.Page.I18N("webexpress.webapp", "fileupload.label");
            }

            if (ButtonIcon == null)
            {
                ButtonIcon = new PropertyIcon(TypeIcon.Upload);
            }

            if (ButtonColor == null)
            {
                ButtonColor = new PropertyColorButton(TypeColorButton.Primary);
            }

            Formular.RedirectUri = RedirectUri != null ? RedirectUri : context.Uri;
            Formular.SubmitButton.Text = ButtonLabel;
            Formular.SubmitButton.Icon = ButtonIcon;
            Formular.SubmitButton.Color = ButtonColor;

            Formular.Add(Prologue);
            Formular.Add(File);
            Formular.Add(Epilogue);
            
            return base.Render(context);
        }
    }
}
