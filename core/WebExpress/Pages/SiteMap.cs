using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.Workers;

namespace WebExpress.Pages
{
    /// <summary>
    /// Erstellt eine Seitenstruktur
    /// z.B.:
    /// <code>
    /// var siteMap = new SiteMap(Context);
    /// siteMap.AddPage("Home", (x) => { return new WorkerPage<PageDashboard>(x); });
    /// siteMap.AddPage("Hilfe", "help", (x) => { return new WorkerPage<PageHelp>(x); });
    /// siteMap.AddPath("Home");
    /// siteMap.AddPath("Home/Hilfe");
    /// </code>
    /// </summary>
    public class SiteMap : ISiteMap
    {
        /// <summary>
        /// Liefert oder setzt die Seiten
        /// </summary>
        public Dictionary<UriSegmentID, SiteMapPage> Pages { get; protected set; } = new Dictionary<UriSegmentID, SiteMapPage>();

        /// <summary>
        /// Liefert oder setzt die Variablen
        /// </summary>
        public Dictionary<UriSegmentID, UriPathSegmentDynamic> PathSegmentVariables { get; protected set; } = new Dictionary<UriSegmentID, UriPathSegmentDynamic>();

        /// <summary>
        /// Liefert oder setzt die Pfade
        /// </summary>
        public List<SiteMapPath> Paths { get; protected set; } = new List<SiteMapPath>();

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IPluginContext Context { get; protected set; }

        /// <summary>
        /// Konstukor
        /// </summary>
        /// <param name="context">der Kontext</param>
        public SiteMap(IPluginContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="siteMap">Die zu kopierende Sitemap</param>
        public SiteMap(SiteMap siteMap)
        {
            Context = siteMap.Context;
            
            foreach(var v in siteMap.Pages)
            {
                Pages.Add(v.Key, new SiteMapPage(v.Value));
            }

            foreach (var v in siteMap.PathSegmentVariables)
            {
                PathSegmentVariables.Add(v.Key, new UriPathSegmentDynamic(v.Value));
            }

            Paths.AddRange(siteMap.Paths.Select(x => new SiteMapPath(x)));
        }

        /// <summary>
        /// Fügt eine Seite hinzu 
        /// </summary>
        /// <param name="id">Die Seiten-ID</param>
        /// <param name="create">Die Rückrufsfunktion zum erzeugen der Worker-Instanzen</param>
        public void AddPage(string id, Func<UriPage, IWorker> create)
        {
            AddPage(id, string.Empty, create);
        }

        /// <summary>
        /// Fügt eine Seite hinzu 
        /// </summary>
        /// <param name="id">Die Seiten-ID</param>
        /// <param name="segment">Das Segment des Uri-Pfades</param>
        /// <param name="create">Die Rückrufsfunktion zum erzeugen der Worker-Instanzen</param>
        public void AddPage(string id, string segment, Func<UriPage, IWorker> create)
        {
            var pageID = new UriSegmentID(id);

            if (!Pages.ContainsKey(pageID))
            {
                Pages.Add(pageID, new SiteMapPage(pageID, segment, id, create));
            }
            else
            {
                throw new SiteMapException("Die SeitenID ist nicht eindeutig. Stellen Sie sicher, dass sie nur eindeutige SeitenIDs verwenden.");
            }
        }

        /// <summary>
        /// Fügt eine Seite hinzu 
        /// </summary>
        /// <param name="id">Die Seiten-ID</param>
        /// <param name="segment">Das Segment des Uri-Pfades</param>
        /// <param name="display">Der Anzeigestring </param>
        /// <param name="create">Die Rückrufsfunktion zum erzeugen der Worker-Instanzen</param>
        public void AddPage(string id, string segment, string display, Func<UriPage, IWorker> create)
        {
            var pageID = new UriSegmentID(id);

            if (!Pages.ContainsKey(pageID))
            {
                Pages.Add(pageID, new SiteMapPage(pageID, segment, display, create));
            }
            else
            {
                throw new SiteMapException("Die SeitenID ist nicht eindeutig. Stellen Sie sicher, dass sie nur eindeutige SeitenIDs verwenden.");
            }
        }

        /// <summary>
        /// Fügt eine Pfadvariable hinzu 
        /// </summary>
        /// <param name="id">Die SeitenID</param>
        public void AddPathSegmentVariable(string id, UriPathSegmentDynamicDisplay display, params UriPathSegmentVariable[] variables)
        {
            AddPathSegmentVariable(id, display, null, variables);
        }

        /// <summary>
        /// Fügt eine Pfadvariable hinzu 
        /// </summary>
        /// <param name="id">Die SeitenID</param>
        /// <param name="display">Der Anzeigestring (z.B. Hallo $x_1)</param>
        /// <param name="seperator">Der Trenner, zur Abgrenzung der einzelnen Variablen</param>
        /// <param name="variables">Die Variablen</param>
        public void AddPathSegmentVariable(string id, UriPathSegmentDynamicDisplay display, string seperator, params UriPathSegmentVariable[] variables)
        {
            var pageID = new UriSegmentID(id);

            if (!PathSegmentVariables.ContainsKey(pageID))
            {
                PathSegmentVariables.Add(pageID, new UriPathSegmentDynamic(display, seperator, variables));
            }
            else
            {
                throw new SiteMapException("Die Variable ist nicht eindeutig. Stellen Sie sicher, dass sie nur eindeutige Variablen verwenden.");
            }
        }

        /// <summary>
        /// Fügt ein Pfad hinzu 
        /// </summary>
        /// <param name="path">Der Pfad</param>
        /// <param name="includeSubPath">Bestimmt ob alle Unterfade herangezogen werden</param>
        public void AddPath(string path, bool includeSubPath = false)
        {
            Paths.Add(new SiteMapPath(path, includeSubPath));
        }

        /// <summary>
        /// Ermittelt die Uri zur angegebenen Seite
        /// </summary>
        /// <param name="pageID">Die SeitenID</param>
        /// <returns>Die Uri</returns>
        public IUri GetUri(UriSegmentID pageID)
        {
            // Ermittle alle Pages aus allen Pfaden
            var pagesFromPath = Paths.Select
            (
                x => new
                {
                    Page = Pages.Where(y => y.Value.ID.Equals(x.Path.Split('/').Where(x => !x.Equals(".*")).LastOrDefault())).Select(y => y.Value).FirstOrDefault(),
                    Path = x,
                    x.IncludeSubPaths
                }
            ).Where(x => x.Page.ID == pageID);

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
                    var segmentPage = Pages.Where(x => x.Value.ID.Equals(segment)).Select(x => x.Value).FirstOrDefault();

                    // Pfadvariablen ermitteln
                    if (PathSegmentVariables.ContainsKey(segmentPage.ID))
                    {
                        var variable = PathSegmentVariables[segmentPage.ID];

                        uri.Variables.Add(segmentPage.ID, variable);
                    }

                    uri.Path.Add(new UriPathSegmentPage(segmentPage?.Segment, segmentPage?.ID)
                    {
                        Display = segmentPage?.Display
                    });
                }
                
                return uri;
            }

            return null;
        }

    }
}