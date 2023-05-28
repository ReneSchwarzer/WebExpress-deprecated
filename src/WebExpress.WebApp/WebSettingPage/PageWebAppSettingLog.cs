using System;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Logging settings page.
    /// </summary>
    [WebExTitle("webexpress.webapp:setting.titel.log.label")]
    [WebExSegment("log", "webexpress.webapp:setting.titel.log.label")]
    [WebExContextPath("/setting")]
    [WebExSettingSection(WebExSettingSection.Secondary)]
    [WebExSettingIcon(TypeIcon.FileMedicalAlt)]
    [WebExSettingGroup("webexpress.webapp:setting.group.system.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule(typeof(Module))]
    [WebExContext("admin")]
    [WebExOptional]
    public sealed class PageWebAppSettingLog : PageWebAppSetting
    {
        /// <summary>
        /// Delivers or sets the Uri to download the lot file. Null if no log file downlod is to be performed.
        /// </summary>
        public string DownloadUri { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageWebAppSettingLog()
        {
            Icon = new PropertyIcon(TypeIcon.FileMedicalAlt);
        }

        /// <summary>
        /// Pre processing of the request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void PreProcess(Request request)
        {
            DownloadUri = request.Uri.Append("download");

            base.PreProcess(request);
        }

        /// <summary>
        /// Processing of the request.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var file = new FileInfo(context.Host.Log.Filename);
            var fileSize = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", file.Exists ? file.Length : 0);

            var deleteForm = new ControlModalFormularConfirmDelete("delte_log")
            {
                Header = this.I18N("webexpress.webapp", "setting.logfile.delete.header"),
                Content = new ControlFormItemStaticText() { Text = this.I18N("webexpress.webapp", "setting.logfile.delete.description") }
            };

            deleteForm.Confirm += (s, e) =>
            {
                File.Delete(context.Host.Log.Filename);
            };

            var switchOnForm = new ControlModalFormularConfirm("swichon_log")
            {
                Header = this.I18N("webexpress.webapp", "setting.logfile.switchon.header"),
                Content = new ControlFormItemStaticText() { Text = this.I18N("webexpress.webapp", "setting.logfile.switchon.description") },
                ButtonIcon = new PropertyIcon(TypeIcon.PowerOff),
                ButtonColor = new PropertyColorButton(TypeColorButton.Success),
                ButtonLabel = this.I18N("webexpress.webapp", "setting.logfile.switchon.label")
            };

            switchOnForm.Confirm += (s, e) =>
            {
                context.Host.Log.LogMode = Log.Mode.Override;
                context.Host.Log.Info(this.I18N("webexpress.webapp", "setting.logfile.switchon.success"));
            };

            var info = new ControlTable() { Striped = false };
            info.AddRow
            (
                new ControlText()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.path")
                },
                new ControlText()
                {
                    Text = context.Host.Log.Filename,
                    Format = TypeFormatText.Code
                },
                DownloadUri != null && file.Exists ? new ControlButtonLink()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.download"),
                    Icon = new PropertyIcon(TypeIcon.Download),
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Primary),
                    Uri = DownloadUri
                } : new ControlPanel()
            );

            info.AddRow
            (
                new ControlText()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.size")
                },
                new ControlText()
                {
                    Text = file.Exists ? fileSize : "n.a.",
                    Format = TypeFormatText.Code
                },
                file.Exists ? new ControlButton()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.delete.label"),
                    Modal = new PropertyModal(TypeModal.Modal, deleteForm),
                    Icon = new PropertyIcon(TypeIcon.TrashAlt),
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Danger)
                } : new ControlPanel()
            );

            info.AddRow
            (
                new ControlText()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.modus")
                },
                new ControlText()
                {
                    Text = context.Host.Log.LogMode.ToString(),
                    Format = TypeFormatText.Code
                },
                context.Host.Log.LogMode == Log.Mode.Off ? new ControlButton()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.switchon.label"),
                    Modal = new PropertyModal(TypeModal.Modal, switchOnForm),
                    Icon = new PropertyIcon(TypeIcon.PowerOff),
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Success)
                } : new ControlPanel()
            );

            context.VisualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.logfile.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            context.VisualTree.Content.Primary.Add(info);

            if (file.Exists)
            {
                var content = File.ReadLines(context.Host.Log.Filename).TakeLast(100);

                context.VisualTree.Content.Primary.Add(new ControlText()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.extract"),
                    Format = TypeFormatText.H3
                });

                context.VisualTree.Content.Primary.Add(new ControlText()
                {
                    Text = string.Join("<br/>", content.Reverse()),
                    Format = TypeFormatText.Code
                });
            }
        }
    }
}

