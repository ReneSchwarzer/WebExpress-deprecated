using System;
using System.Collections.Generic;
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
        /// Initialisierung des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        void Init(string configFileName);

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
        /// Liefert oder setzt den Host
        /// </summary>
        IHost Host { get; set; }

        /// <summary>
        /// Liste der Worker
        /// </summary>
        Dictionary<string, IWorker> Workers { get; }
    }
}
