using System;
using System.Collections.Generic;
using WebExpress.Config;
using WebExpress.Messages;
using WebExpress.Workers;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Diese Interface repräsentiert ein Plugin
    /// </summary>
    public interface IPlugin : IDisposable
    {
        /// <summary>
        /// Liefert oder setzt die Plugin-Einstellungen
        /// </summary>
        PluginConfig Config { get; }

        /// <summary>
        /// Initialisierung des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        void Init(string configFileName = null);

        /// <summary>
        /// Registriert einen Worker 
        /// </summary>
        /// <param name="worker">Der zu registrierende Worker</param>
        void Register(IWorker worker);

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort aus der Vorverarbeitung oder null</returns>
        Response PreProcess(Request request);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        Response Process(Request request);

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        Response PostProcess(Request request, Response response);

        /// <summary>
        /// Liefert den Namen des Plugins
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        IPluginContext Context { get; set; }

        /// <summary>
        /// Liste der Worker
        /// </summary>
        Dictionary<string, IWorker> Workers { get; }
    }
}
