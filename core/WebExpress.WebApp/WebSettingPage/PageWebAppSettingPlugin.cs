﻿using System;
using System.Linq;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;
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
    [Optional]
    public sealed class PageWebAppSettingPlugin : PageWebAppSetting
    {
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
                                        Text = $"{ this.I18N(m.Plugin.PluginID, m.ModuleName) } - { this.I18N(m.Plugin.PluginID, m.Description) }",
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

            context.VisualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("webexpress.webapp", "setting.plugin.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            context.VisualTree.Content.Primary.Add(plugins);
        }
    }
}

