using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebCondition;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebFragment
{
    [WebExSection(Section.HeadlineSecondary)]
    [WebExModule("webexpress.webapp")]
    [WebExContext("webexpress.webpp.systeminformation")]
    [WebExCondition(typeof(ConditionUnix))]
    public sealed class FragmentPropertyReboot : FragmentControlButtonLink
    {
        /// <summary>
        /// Returns the modal dialog to confirm the delete action.
        /// </summary>
        private ControlModalFormularConfirm ModalDlg = new ControlModalFormularConfirm("archive_btn")
        {
            Header = "webexpress.webapp:setting.systeminformation.reboot.header",
            Content = new ControlFormularItemStaticText() { Text = "webexpress.webapp:setting.systeminformation.reboot.description" }
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentPropertyReboot()
            : base("archive_btn")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the fragment is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
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

            Modal = new PropertyModal(TypeModal.Modal, ModalDlg);
        }

        /// <summary>
        /// Called when the delete action has been confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
