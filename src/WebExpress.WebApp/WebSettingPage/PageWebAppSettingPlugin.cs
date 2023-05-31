using System;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebPage;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Settings page with information about the active plugins.
    /// </summary>
    [WebExTitle("webexpress.webapp:setting.titel.plugin.label")]
    [WebExSegment("plugin", "webexpress.webapp:setting.titel.plugin.label")]
    [WebExContextPath("/Setting")]
    [WebExSettingSection(WebExSettingSection.Secondary)]
    [WebExSettingIcon(TypeIcon.PuzzlePiece)]
    [WebExSettingGroup("webexpress.webapp:setting.group.system.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    [WebExContext("admin")]
    [WebExContext("webexpress.webapp.plugin")]
    [WebExOptional]
    public sealed class PageWebAppSettingPlugin : PageWebAppSetting
    {
        /// <summary>
        /// The id of the web task for importing a plugin.
        /// </summary>
        private const string TaskId = "webexpress-webapp-plugin-upload";

        /// <summary>
        /// Returns the label control.
        /// </summary>
        private ControlText Label { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.plugin.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Info)
        };

        /// <summary>
        /// Retruns the help text control.
        /// </summary>
        private ControlText Description { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.plugin.description",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Returns the upload button for uploading and initializing a plugin.
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
        /// Form for uploading a plugin.
        /// </summary>
        private ControlModalFormularFileUpload ModalUploadForm { get; } = new ControlModalFormularFileUpload("plugin_upload")
        {
            Header = "webexpress.webapp:setting.plugin.upload.header"
        };

        /// <summary>
        /// Progress control to monitor plugin initialization.
        /// </summary>
        private ControlApiModalProgressTaskState ModalTaskUpdate { get; } = new ControlApiModalProgressTaskState(TaskId)
        {
            Header = "webexpress.webapp:setting.plugin.upload.header"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageWebAppSettingPlugin()
        {
            Icon = new PropertyIcon(TypeIcon.PuzzlePiece);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context of the resource.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            ModalUploadForm.File.Help = "webexpress.webapp:setting.plugin.upload.description";
            ModalUploadForm.Prologue = new ControlFormItemStaticText() { Text = "webexpress.webapp:setting.plugin.upload.help" };
            ModalUploadForm.File.AcceptFile = new string[] { ".dll" };
            ModalUploadForm.Upload += OnUpload;
        }

        /// <summary>
        /// Called when an upload is to take place.
        /// </summary>
        /// <param name="sender">The trigger.</param>
        /// <param name="e">The event argument.</param>
        private void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var task = ComponentManager.TaskManager.CreateTask(TaskId, OnTaskProcess, e);
            task.Run();
        }

        /// <summary>
        /// Execution of the WebTask.
        /// </summary>
        /// <param name="sender">The trigger.</param>
        /// <param name="e">The event argument.</param>
        private void OnTaskProcess(object sender, EventArgs e)
        {
            var task = sender as Task;
            var file = (task.Arguments.FirstOrDefault() as FormularUploadEventArgs)?.File as ParameterFile;
            var context = (task.Arguments.FirstOrDefault() as FormularUploadEventArgs)?.Context as RenderContext;

            // determine any installed package



            //var plugin = ComponentManager.PluginManager.GetPluginByFileName(file.Value);

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
        /// Processing of the page
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var plugins = new ControlTable() { Striped = false };
            plugins.AddColumn("");
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.name.label"));
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.version.label"));

            foreach (var application in ComponentManager.ApplicationManager.Applications.Where(x => !x.ApplicationId.StartsWith("webexpress", StringComparison.OrdinalIgnoreCase)))
            {
                var plugin = application.PluginContext;
                var mudules = ComponentManager.ModuleManager.Modules
                    .Where(x => x.ApplicationContext.ApplicationId.Equals(application.ApplicationId, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                plugins.AddRow
                (
                    new ControlImage() { Uri = application.Icon ?? null, Width = 32 },
                    new ControlPanel
                    (
                        new ControlLink()
                        {
                            Text = this.I18N(plugin.PluginId, application.ApplicationName),
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
                            Text = string.Format(this.I18N("webexpress.webapp:setting.plugin.description.label"), this.I18N(application.Description)),
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
                                        Text = $"{this.I18N(m.PluginContext.PluginId, this.I18N(m.ModuleName))} - {this.I18N(m.PluginContext.PluginId, this.I18N(m.Description))}",
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

            if (ComponentManager.TaskManager.GetTask(TaskId) is Task task && task.State == TaskState.Run)
            {
                context.VisualTree.Content.Secondary.Add(ModalTaskUpdate);
            }
        }
    }
}

