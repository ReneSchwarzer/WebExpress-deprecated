using System;
using System.Reflection;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage.PageSetting
{
    /// <summary>
    /// Einstellungsseite mit Systeminformationen
    /// </summary>
    public abstract class PageTemplateWebAppSettingSystemInformation : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebAppSettingSystemInformation()
        {
            Icon = new PropertyIcon(TypeIcon.InfoCircle);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContext context)
        {
            base.Process(context);

            var visualTree = context.GetVisualTree<VisualTreeWebApp>();

            var version = typeof(HttpServer).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            var memory = 0.0;
            using (var proc = System.Diagnostics.Process.GetCurrentProcess())
            {
                memory = proc.PrivateMemorySize64 / (1024 * 1024);
            }

            var server = new ControlTable() { Striped = false };
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.version") }, new ControlText() { Text = version, Format = TypeFormatText.Code });
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.systemdate") }, new ControlText() { Text = DateTime.Now.ToString(Culture.DateTimeFormat.LongDatePattern), Format = TypeFormatText.Code });
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.systemtime") }, new ControlText() { Text = DateTime.Now.ToString(Culture.DateTimeFormat.LongTimePattern), Format = TypeFormatText.Code });
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.basisurl") }, new ControlText() { Text = Uri.Root.ToString(), Format = TypeFormatText.Code });
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.currentdirectory") }, new ControlText() { Text = Environment.CurrentDirectory, Format = TypeFormatText.Code });
            server.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.memory") }, new ControlText() { Text = memory.ToString(Culture) + " MB", Format = TypeFormatText.Code });

            visualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.server.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            visualTree.Content.Primary.Add(server);

            var environment = new ControlTable() { Striped = false };
            environment.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.operatingsystem") }, new ControlText() { Text = Environment.OSVersion.ToString(), Format = TypeFormatText.Code });
            environment.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.machinename") }, new ControlText() { Text = Environment.MachineName, Format = TypeFormatText.Code });
            environment.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.processorcount") }, new ControlText() { Text = Environment.ProcessorCount.ToString(), Format = TypeFormatText.Code });
            environment.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.64bit") }, new ControlText() { Text = Environment.Is64BitOperatingSystem.ToString(Culture), Format = TypeFormatText.Code });
            environment.AddRow(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.username") }, new ControlText() { Text = Environment.UserName, Format = TypeFormatText.Code });

            visualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.systeminformation.group.environment.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            visualTree.Content.Primary.Add(environment);
        }
    }
}

