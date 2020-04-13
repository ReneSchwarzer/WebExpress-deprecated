using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebExpress.Config;
using WebExpress.Messages;
using WebExpress.Pages;
using WebExpress.Workers;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Repräsentiert ein Plugin
    /// </summary>
    public class Plugin : IPlugin
    {
        /// <summary>
        /// Liefert oder setzt die Plugin-Einstellungen
        /// </summary>
        public PluginConfig Config { get; protected set; }

        /// <summary>
        /// Der zum Plugin zugehörige Kontext
        /// </summary>
        public IPluginContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Plugins
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Liste der Worker
        /// </summary>
        public Dictionary<string, IWorker> Workers { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private Plugin()
        {
            Workers = new Dictionary<string, IWorker>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Name des Plugins</param>
        /// <param name="icon">Das Icon des Plugins</param>
        public Plugin(string name, string icon)
            : this()
        {
            Name = name;
            Icon = icon;
        }

        /// <summary>
        /// Initialisierung des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        public virtual void Init(string configFileName = null)
        {
            Config = new PluginConfig(configFileName);

            if (!string.IsNullOrWhiteSpace(Config?.UrlBasePath))
            {
                Context = new PluginContext(Context, Config.UrlBasePath);
            }
        }

        /// <summary>
        /// Registriert einen Worker 
        /// </summary>
        /// <param name="worker"></param>
        public void Register(IWorker worker)
        {
            worker.Context = Context;
            var key = worker.Path.ToRawString();

            if (!Workers.ContainsKey(key))
            {
                Workers.Add(key, worker);

                if (key.EndsWith("/"))
                {
                    Workers.Add(key.Substring(0, key.Length - 1), worker);
                }

                if (!key.EndsWith("/"))
                {
                    Workers.Add(key + "/", worker);
                }
            }
            else
            {
                Workers[key] = worker;
            }
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort aus der Vorverarbeitung oder null</returns>
        public virtual Response PreProcess(Request request)
        {
            return null;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public virtual Response Process(Request request)
        {
            if (Workers == null)
            {
                // Kein Worker gefunden => 404 Not Found
                return new ResponseNotFound();
            }

            foreach (var w in Workers)
            {
                if (Regex.IsMatch(request.URL, "^" + w.Key + "$", RegexOptions.IgnoreCase))
                {
                    var response = w.Value.PreProcess(request);
                    if (response == null)
                    {
                        response = w.Value.Process(request);
                        response = w.Value.PostProcess(request, response);
                    }

                    return response;
                }
            }

            // Kein Worker gefunden => 404 Not Found
            return new ResponseNotFound();
        }

        /// <summary>
        /// Nachverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        public virtual Response PostProcess(Request request, Response response)
        {
            return response;
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche bei der Initialisierung reserviert wurden.
        /// </summary>
        /// <param name="data">Die Eingabedaten</param>
        public virtual void Dispose()
        {
        }
    }
}
