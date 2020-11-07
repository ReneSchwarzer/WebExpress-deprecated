using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public abstract class Plugin : IPlugin
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
        /// Liefert oder setzt die Sitemap
        /// </summary>
        public ISiteMap SiteMap { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Liste der Worker
        /// </summary>
        public Dictionary<string, IWorker> Workers { get; private set; } = new Dictionary<string, IWorker>();

        /// <summary>
        /// Die Statuspages
        /// </summary>
        public Dictionary<int, Func<IPageStatus>> StatusPages { get; private set; } = new Dictionary<int, Func<IPageStatus>>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        private Plugin()
        {

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

            SiteMap = new SiteMap(Context);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt
        /// </summary>
        public virtual void Run()
        {
            Register(SiteMap);
        }

        /// <summary>
        /// Registriert einen Worker 
        /// </summary>
        /// <param name="worker">Der zu registrierende Worker</param>
        public void Register(IWorker worker)
        {
            worker.Context = Context;
            var key = worker.Uri.ToRawString();

            if (!Workers.ContainsKey(key))
            {
                if (!worker.Uri.IncludeSubPaths)
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
                    if (key.EndsWith("/"))
                    {
                        Workers.Add(key + ".*", worker);
                    }
                    else
                    {
                        Workers.Add(key + "/.*", worker);
                    }
                }
            }
            else
            {
                Workers[key] = worker;
            }
        }

        /// <summary>
        /// Registriert eine SiteMap
        /// </summary>
        /// <param name="worker">Die zu registrierende SiteMap</param>
        public void Register(ISiteMap siteMap)
        {
            // Ermittle alle Pages aus allen Pfaden
            var pagesFromPath = siteMap.Paths.Select
            (
                x => new
                {
                    Page = siteMap.Pages.Where(y => y.Value.ID.Equals(x.Path.Split('/').Where(x => !x.Equals(".*")).LastOrDefault())).Select(y => y.Value).FirstOrDefault(),
                    Path = x,
                    x.IncludeSubPaths
                }
            );

            foreach (var item in pagesFromPath)
            {
                if (item == null || item.Page == null || item.Path == null)
                {
                    throw new SiteMapException("Fehler in der SiteMap. Überprüfen Sie die SiteMap.");
                }

                var uri = new UriPage(Context)
                {
                    IncludeSubPaths = item.IncludeSubPaths
                };

                foreach (var segment in item.Path.Path.Split('/').Where(x => !x.Equals(".*")))
                {
                    // Seite des Segmentes ermitteln
                    var segmentPage = siteMap.Pages.Where(x => x.Value.ID.Equals(segment)).Select(x => x.Value).FirstOrDefault();

                    // Pfadvariablen ermitteln
                    if (siteMap.PathSegmentVariables.ContainsKey(segmentPage.ID))
                    {
                        var variable = siteMap.PathSegmentVariables[segmentPage.ID];

                        uri.Variables.Add(segmentPage.ID, variable);
                    }

                    uri.Path.Add(new UriPathSegmentPage(segmentPage?.Segment, segmentPage?.ID)
                    {
                        Display = segmentPage?.Display
                    });
                }

                var worker = item.Page.Create(uri);

                Register(worker);
            }
        }

        /// <summary>
        /// Registriert eine Statusseite
        /// </summary>
        /// <param name="code">Der Statuscode</param>
        /// <param name="create">Die Rückruffunktion zum Erzeugen einer neuen Instanz</param>
        public void RegisterStatusPage(int code, Func<IPageStatus> create)
        {
            if (!StatusPages.ContainsKey(code))
            {
                StatusPages.Add(code, create);

                return;
            }

            StatusPages[code] = create;
            Context.Log.Warning(MethodBase.GetCurrentMethod(), "Statusseite {0} war bereits registriert und wird durch eine neue überschrieben!");
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
                        if (!(response is ResponseNotFound))
                        {
                            response = w.Value.PostProcess(request, response);
                            
                            return response;
                        }
                    }
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
