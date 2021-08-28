using System;
using System.Linq;
using System.Reflection;
using WebExpress.Application;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.Plugin;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebResource.PageSetting
{
    /// <summary>
    /// Einstellungsseite mit Systeminformationen
    /// </summary>
    public abstract class PageTemplateWebAppSettingPlugin : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebAppSettingPlugin()
        {
            Icon = new PropertyIcon(TypeIcon.PuzzlePiece);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            var plugins = new ControlTable() { Striped = false };
            plugins.AddColumn("");
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.name.label"));
            plugins.AddColumn(this.I18N("webexpress.webapp", "setting.plugin.version.label"));

            foreach (var application in ApplicationManager.Applications.Where(x => !x.ApplicationID.StartsWith("webexpress", StringComparison.OrdinalIgnoreCase)))
            {
                var plugin = PluginManager.GetPlugin(application.PluginID);
                var mudules = ModuleManager.Modules.Where(x => x.ApplicationID.Equals(application.ApplicationID, StringComparison.OrdinalIgnoreCase));

                plugins.AddRow
                (
                    new ControlImage() { Uri = application.Icon != null ? application.Icon : null, Width = 32 },
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
                            Format = TypeFormatText.Default, TextColor = new PropertyColorText(TypeColorText.Secondary),
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
                            Text = string.Format(this.I18N("webexpress.webapp", "setting.plugin.description.label"), this.I18N(plugin.PluginID, application.Description)),
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
                                        Text = $"{ this.I18N(plugin.PluginID, m.ModuleName) } - { this.I18N(plugin.PluginID, m.Description) }" ,
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
                    new ControlText() { Text = plugin.Version, Format = TypeFormatText.Code}
                );
            }

            Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.plugin.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            Content.Primary.Add(plugins);

            base.Initialization();
        }
    }
}

