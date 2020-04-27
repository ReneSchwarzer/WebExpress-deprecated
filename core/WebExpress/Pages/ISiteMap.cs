using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Html;
using WebExpress.Workers;

namespace WebExpress.Pages
{
    public interface ISiteMap
    {
        /// <summary>
        /// Liefert die Seiten
        /// </summary>
        Dictionary<UriSegmentID, SiteMapPage> Pages { get; }

        /// <summary>
        /// Liefert oder setzt die Variablen
        /// </summary>
        Dictionary<UriSegmentID, UriPathSegmentDynamic> PathSegmentVariables { get; }

        /// <summary>
        /// Liefert oder setzt die Pfade
        /// </summary>
        List<SiteMapPath> Paths { get; }

        /// <summary>
        /// Fügt eine Seite hinzu 
        /// </summary>
        /// <param name="id">Die Seiten-ID</param>
        /// <param name="create">Die Rückrufsfunktion zum erzeugen der Worker-Instanzen</param>
        void AddPage(string id, Func<UriPage, IWorker> create);

        /// <summary>
        /// Fügt eine Seite hinzu 
        /// </summary>
        /// <param name="id">Die Seiten-ID</param>
        /// <param name="segment">Das Segment des Uri-Pfades</param>
        /// <param name="create">Die Rückrufsfunktion zum erzeugen der Worker-Instanzen</param>
        void AddPage(string id, string segment, Func<UriPage, IWorker> create);

        /// <summary>
        /// Fügt eine Pfadvariable hinzu 
        /// </summary>
        /// <param name="id">Die SeitenID</param>
        void AddPathSegmentVariable(string id, UriPathSegmentDynamicDisplay display, params UriPathSegmentVariable[] variables);

        /// <summary>
        /// Fügt eine Pfadvariable hinzu 
        /// </summary>
        /// <param name="id">Die SeitenID</param>
        /// <param name="display">Der Anzeigestring (z.B. Hallo $x_1)</param>
        /// <param name="seperator">Der Trenner, zur Abgrenzung der einzelnen Variablen</param>
        /// <param name="variables">Die Variablen</param>
        void AddPathSegmentVariable(string id, UriPathSegmentDynamicDisplay display, string seperator, params UriPathSegmentVariable[] variables);

        /// <summary>
        /// Fügt ein Pfad hinzu 
        /// </summary>
        /// <param name="path">Der Pfad</param>
        /// <param name="includeSubPath">Bestimmt ob alle Unterfade herangezogen werden</param>
        void AddPath(string path, bool includeSubPath = false);

        /// <summary>
        /// Ermittelt die Uri zur angegebenen Seite
        /// </summary>
        /// <param name="pageID">Die SeitenID</param>
        /// <returns>Die Uri</returns>
        IUri GetUri(UriSegmentID pageID);
    }
}
