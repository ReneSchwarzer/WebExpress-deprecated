using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebCondition;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Application("ViLa")]
    [Context("webexpress.webpp.systeminformation")]
    [Condition(typeof(ConditionUnix))]
    public sealed class ComponentPropertyReboot : ComponentControlButtonLink
    {
        /// <summary>
        /// Liefert den modalen Dialog zur Bestätigung der Löschaktion
        /// </summary>
        private ControlModalFormConfirm ModalDlg = new ControlModalFormConfirm("archive_btn")
        {
            Header = "webexpress.webapp:setting.systeminformation.reboot.header",
            Content = new ControlFormularItemStaticText() { Text = "webexpress.webapp:setting.systeminformation.reboot.description" }
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentPropertyReboot()
            : base("archive_btn")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "webexpress.webapp:setting.systeminformation.reboot.label";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Danger);
            Icon = new PropertyIcon(TypeIcon.PowerOff);
            TextColor = new PropertyColorText(TypeColorText.Light);

            ModalDlg.ButtonIcon = Icon;
            ModalDlg.ButtonLabel = Text;
            ModalDlg.ButtonColor = new PropertyColorButton(TypeColorButton.Danger);
            ModalDlg.Confirm += OnConfirm;

            Modal = ModalDlg;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Löschaktion bestätigt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \" sudo shutdown -r now \"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            Task.Run(() =>
            {
                try
                {
                    process.Start();
                    var result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                }
                catch (Exception ex)
                {
                    e.Context.Log.Exception(ex);
                }
            });
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
