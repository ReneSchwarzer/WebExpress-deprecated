using System;
using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormFileUpload : ControlModalFormular
    {
        /// <summary>
        /// Liefert die Akzeptierten Datein
        /// </summary>
        public ICollection<string> AcceptFile { get => File.AcceptFile; set => File.AcceptFile = value; }

        /// <summary>
        /// Event wird ausgelöst, wenn das Hochladen bestätigt wurde
        /// </summary>
        public event EventHandler<FormularUploadEventArgs> Upload;

        /// <summary>
        /// Liefert oder setzt das Dokument
        /// </summary>
        public ControlFormularItemInputFile File { get; } = new ControlFormularItemInputFile()
        {
            Name = "file",
            //Label = "inventoryexpress.media.form.image.label",
            Help = "webexpress.webapp:fileupload.file.description",
            //Icon = new PropertyIcon(TypeIcon.Image),
            //AcceptFile = new string[] { "image/*, video/*, audio/*, .pdf, .doc, .docx, .txt" },
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
        };

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
        /// Liefert oder setzt den Prolog
        /// </summary>
        private ControlFormularItem prologue;

        /// <summary>
        /// Liefert oder setzt den Prolog
        /// </summary>
        public ControlFormularItem Prologue
        {
            get => prologue;
            set { Formular.Items.Remove(prologue); prologue = value; Formular.Items.Insert(0, prologue); }
        }

        /// <summary>
        /// Liefert oder setzt den Epilog
        /// </summary>
        private ControlFormularItem epilogue;


        /// <summary>
        /// Liefert oder setzt den Epilog
        /// </summary>
        public ControlFormularItem Epilogue
        {
            get => epilogue;
            set { Formular.Items.Remove(epilogue); epilogue = value; Formular.Items.Add(epilogue); }
        }

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
            Header = I18N("webexpress.webapp:fileupload.header");
            ButtonLabel = I18N("webexpress.webapp:fileupload.label");
            ButtonIcon = new PropertyIcon(TypeIcon.Upload);
            ButtonColor = new PropertyColorButton(TypeColorButton.Primary);

            File.Validation += OnValidationFile;
            Formular.ProcessFormular += OnProcessFormular;

            Formular.Add(File);
        }

        /// <summary>
        /// Validierung der Uploaddatei
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnValidationFile(object sender, ValidationEventArgs e)
        {
            if (!(e.Context.Request.GetParameter(File.Name) is ParameterFile))
            {
                e.Results.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = I18N("webexpress.webapp:fileupload.file.validation.error.nofile") });
            }
        }

        /// <summary>
        /// Verarbeitung des Formulares
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            if (e.Context.Request.GetParameter(File.Name) is ParameterFile file)
            {
                OnUpload(new FormularUploadEventArgs(e) { File = file });
            }
        }

        /// <summary>
        /// Löst das Upload-Event aus
        /// </summary>
        /// <param name="args">Die Eventargumente</param>
        protected virtual void OnUpload(FormularUploadEventArgs args)
        {
            Upload?.Invoke(this, args);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Formular.RedirectUri = RedirectUri ?? context.Uri;
            Formular.SubmitButton.Text = ButtonLabel;
            Formular.SubmitButton.Icon = ButtonIcon;
            Formular.SubmitButton.Color = ButtonColor;

            return base.Render(context);
        }
    }
}
