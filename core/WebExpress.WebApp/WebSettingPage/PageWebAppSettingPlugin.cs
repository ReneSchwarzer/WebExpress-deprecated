using System;
using System.IO;
using System.Linq;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebModule;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;
using WebExpress.WebResource;
using WebExpress.WebTask;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseite mit Informationen zu den aktiven Plugins
    /// </summary>
    [ID("SettingPlugin")]
    [Title("webexpress.webapp:setting.titel.plugin.label")]
    [Segment("plugin", "webexpress.webapp:setting.titel.plugin.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.PuzzlePiece)]
    [SettingGroup("webexpress.webapp:setting.group.system.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Context("webexpress.webapp.plugin")]
    [Optional]
    public sealed class PageWebAppSettingPlugin : PageWebAppSetting
    {
        /// <summary>
        /// Die ID des WebTask zum Import eines Plugins
        /// </summary>
        private const string TaskID = "webexpress-webapp-plugin-upload";

        /// <summary>
        /// Liefert die Bezeichnung.
        /// </summary>
        private ControlText Label { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.plugin.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Info)
        };

        /// <summary>
        /// Liefert den Hilfetext.
        /// </summary>
        private ControlText Description { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.plugin.description",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Liefert den Upload-Button zum hochladen und inizialisieren eines Plugins.
        /// </summary>
        private ControlButton DownloadButton { get; } = new ControlButton()
        {
            Text = "webexpress.webapp:setting.plugin.upload.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary),
            Icon = new PropertyIcon(TypeIcon.Upload),
            OnClick = new PropertyOnClick("$('#modal_plugin_upload').modal('show');"),
            Active = TypeActive.Disabled
        };

        /// <summary>
        /// Formular zum Hochladen eines Plugins
        /// </summary>
        private ControlModalFormFileUpload ModalUploadForm { get; } = new ControlModalFormFileUpload("plugin_upload")
        {
            Header = "webexpress.webapp:setting.plugin.upload.header"
        };

        /// <summary>
        /// Formular zum Hochladen eines Plugins
        /// </summary>
        private ControlApiModalProgressTaskState ModalTaskUpdate { get; } = new ControlApiModalProgressTaskState(TaskID)
        {
            Header = "webexpress.webapp:setting.plugin.upload.header"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageWebAppSettingPlugin()
        {
            Icon = new PropertyIcon(TypeIcon.PuzzlePiece);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            ModalUploadForm.File.Help = "webexpress.webapp:setting.plugin.upload.description";
            ModalUploadForm.Prologue = new ControlFormularItemStaticText() { Text = "webexpress.webapp:setting.plugin.upload.help" };
            ModalUploadForm.File.AcceptFile = new string[] { ".dll" };
            ModalUploadForm.Upload += OnUpload;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Upload erfolgen soll
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var task = TaskManager.CreateTask(TaskID, OnTaskProcess, e);
            task.Run();
        }

        /// <summary>
        /// Ausführung des WebTask
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnTaskProcess(object sender, EventArgs e)
        {
            var task = sender as Task;
            var file = (task.Arguments.FirstOrDefault() as FormularUploadEventArgs)?.File as ParameterFile;
            var context = (task.Arguments.FirstOrDefault() as FormularUploadEventArgs)?.Context as RenderContext;

            //// eventuell installiertes Plugin ermitteln
            //var plugin = PluginManager.GetPluginByFileName(file.Value);

            //if (plugin == null)
            //{
            //    var host = context.Host;
            //}
            //else if (Directory.Exists(plugin.Assembly.Location))
            //{
            //    // Datei entfernen
            //    Directory.Delete(plugin.Assembly.Location);
            //}


            //// Plugin aus Rgistrierung entfernen
            //PluginManager.Unsubscribe(file.Value);

            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1000);
            //    task.Progress = i;
            //    task.Message = "ABC" + i;
            //}
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var plugins = new ControlTable() { Striped = false };
            plugins.AddColumn("");
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.name.label"));
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.version.label"));

            foreach (var application in ApplicationManager.Applications.Where(x => !x.ApplicationID.StartsWith("webexpress", StringComparison.OrdinalIgnoreCase)))
            {
                var plugin = application.Plugin;
                var mudules = ModuleManager.Modules.Where(x => x.Application.ApplicationID.Equals(application.ApplicationID, StringComparison.OrdinalIgnoreCase)).ToList();

                plugins.AddRow
                (
                    new ControlImage() { Uri = application.Icon ?? null, Width = 32 },
                    new ControlPanel
                    (
                        new ControlLink()
                        {
                            Text = this.I18N(plugin.PluginID, application.ApplicationName),
                            Uri = application.ContextPath
                        },
                        new ControlText()
                        {
                            Text = string.Format(this.I18N("webexpress.webapp", "setting.plugin.manufacturer.label"), plugin.Manufacturer),
                            Format = TypeFormatText.Default,
                            TextColor = new PropertyColorText(TypeColorText.Secondary),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Size = new PropertySizeText(TypeSizeText.Small)
                        },
                        !string.IsNullOrWhiteSpace(plugin.Copyright) ? new ControlText()
                        {
                            Text = string.Format(this.I18N("webexpress.webapp", "setting.plugin.copyright.label"), plugin.Copyright),
                            Format = TypeFormatText.Default,
                            TextColor = new PropertyColorText(TypeColorText.Secondary),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Size = new PropertySizeText(TypeSizeText.Small)
                        } : null,
                        !string.IsNullOrWhiteSpace(plugin.License) ? new ControlText()
                        {
                            Text = string.Format(this.I18N("webexpress.webapp", "setting.plugin.license.label"), plugin.License),
                            Format = TypeFormatText.Default,
                            TextColor = new PropertyColorText(TypeColorText.Secondary),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Size = new PropertySizeText(TypeSizeText.Small)
                        } : null,
                        new ControlText()
                        {
                            Text = string.Format(I18N(context.Culture, "webexpress.webapp:setting.plugin.description.label"), I18N(context.Culture, application.Description)),
                            Format = TypeFormatText.Default,
                            TextColor = new PropertyColorText(TypeColorText.Secondary),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Size = new PropertySizeText(TypeSizeText.Small)
                        },
                        new ControlText()
                        {
                            Text = string.Format(this.I18N("webexpress.webapp", "setting.plugin.modules.label"), plugin.Description),
                            Format = TypeFormatText.Default,
                            TextColor = new PropertyColorText(TypeColorText.Secondary),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Null, PropertySpacing.Space.Null, PropertySpacing.Space.Two, PropertySpacing.Space.Null)
                        },
                        new ControlPanel
                        (
                            mudules.Select
                            (
                                m => new ControlPanel
                                (
                                    new ControlText()
                                    {
                                        Text = $"{ this.I18N(m.Plugin.PluginID, this.I18N(m.ModuleName)) } - { this.I18N(m.Plugin.PluginID, this.I18N(m.Description)) }",
                                        Format = TypeFormatText.Default,
                                        TextColor = new PropertyColorText(TypeColorText.Secondary),
                                        Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                                        Size = new PropertySizeText(TypeSizeText.Small)
                                    }
                               )
                            ).ToArray()
                        )
                        {

                        }
                    )
                    {
                    },
                    new ControlText() { Text = plugin.Version, Format = TypeFormatText.Code }
                );
            }

            context.VisualTree.Content.Headline.Secondary.Add(DownloadButton);
            context.VisualTree.Content.Primary.Add(Description);
            context.VisualTree.Content.Primary.Add(Label);
            context.VisualTree.Content.Primary.Add(plugins);
            context.VisualTree.Content.Secondary.Add(ModalUploadForm);

            if (TaskManager.GetTask(TaskID) is Task task && task.State == TaskState.Run)
            {
                context.VisualTree.Content.Secondary.Add(ModalTaskUpdate);
            }
        }
    }
}

