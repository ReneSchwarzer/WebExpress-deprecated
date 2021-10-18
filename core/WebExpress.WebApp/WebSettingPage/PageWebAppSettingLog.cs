﻿using System;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseite mit Systeminformationen
    /// </summary>
    public abstract class PageWebAppSettingLog : PageWebAppSetting
    {
        /// <summary>
        /// Liefert oder setzt die Uri zum Download des Losfiles. Null wenn kein Logfiledownlod erfolgen soll
        /// </summary>
        public IUri DownloadUri { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageWebAppSettingLog()
        {
            Icon = new PropertyIcon(TypeIcon.FileMedicalAlt);
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var file = new FileInfo(Context.Log.Filename);
            var fileSize = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", file.Exists ? file.Length : 0);

            var deleteForm = new ControlModalFormConfirmDelete("delte_log")
            {
                Header = this.I18N("webexpress.webapp", "setting.logfile.delete.header"),
                Content = new ControlFormularItemStaticText() { Text = this.I18N("webexpress.webapp", "setting.logfile.delete.description") }
            };

            deleteForm.Confirm += (s, e) =>
            {
                File.Delete(Context.Log.Filename);
            };

            var switchOnForm = new ControlModalFormConfirm("swichon_log")
            {
                Header = this.I18N("webexpress.webapp", "setting.logfile.switchon.header"),
                Content = new ControlFormularItemStaticText() { Text = this.I18N("webexpress.webapp", "setting.logfile.switchon.description") },
                ButtonIcon = new PropertyIcon(TypeIcon.PowerOff),
                ButtonColor = new PropertyColorButton(TypeColorButton.Success),
                ButtonLabel = this.I18N("webexpress.webapp", "setting.logfile.switchon.label")
            };

            switchOnForm.Confirm += (s, e) =>
            {
                Context.Log.LogModus = Log.Modus.Override;
                Context.Log.Info(this.I18N("webexpress.webapp", "setting.logfile.switchon.success"));
            };

            var info = new ControlTable() { Striped = false };
            info.AddRow
            (
                new ControlText() { Text = this.I18N("webexpress.webapp", "setting.logfile.path") }, new ControlText() { Text = Context.Log.Filename, Format = TypeFormatText.Code },
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
                new ControlText() { Text = this.I18N("webexpress.webapp", "setting.logfile.size") }, new ControlText() { Text = file.Exists ? fileSize : "n.a.", Format = TypeFormatText.Code },
                file.Exists ? new ControlButton()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.delete.label"),
                    Modal = deleteForm,
                    Icon = new PropertyIcon(TypeIcon.TrashAlt),
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Danger)
                } : new ControlPanel()
            );

            info.AddRow
            (
                new ControlText() { Text = this.I18N("webexpress.webapp", "setting.logfile.modus") }, new ControlText() { Text = Context.Log.LogModus.ToString(), Format = TypeFormatText.Code },
                Context.Log.LogModus == Log.Modus.Off ? new ControlButton()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.switchon.label"),
                    Modal = switchOnForm,
                    Icon = new PropertyIcon(TypeIcon.PowerOff),
                    BackgroundColor = new PropertyColorButton(TypeColorButton.Success)
                } : new ControlPanel()
            );

            visualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.logfile.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            visualTree.Content.Primary.Add(info);

            if (file.Exists)
            {
                var content = File.ReadLines(Context.Log.Filename).TakeLast(100);

                visualTree.Content.Primary.Add(new ControlText()
                {
                    Text = this.I18N("webexpress.webapp", "setting.logfile.extract"),
                    Format = TypeFormatText.H3
                });

                visualTree.Content.Primary.Add(new ControlText()
                {
                    Text = string.Join("<br/>", content.Reverse()),
                    Format = TypeFormatText.Code
                });
            }
        }
    }
}
