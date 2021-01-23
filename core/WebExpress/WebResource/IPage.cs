using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.Workers;

namespace WebExpress.WebResource
{
    public interface IPage : IResource
    {
        /// <summary>
        /// Liefert die Session
        /// </summary>
        Session Session { get; }

        /// <summary>
        /// Liefert oder setzt den Inhalt der Seite
        /// </summary>
        Request Request { get; set; }

        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt das Favicon
        /// </summary>
        List<Favicon> Favicons { get; set; }

        /// <summary>
        /// Liefert oder setzt das internes Stylesheet  
        /// </summary>
        List<string> Styles { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden Css-Dateien
        /// </summary>
        ICollection<IUri> CssLinks { get; }

        /// <summary>
        /// Liefert oder setzt die Metainformationen
        /// </summary>
        List<KeyValuePair<string, string>> Meta { get; }

        /// <summary>
        /// Liefert oder setzt die Parameter der Seite
        /// </summary>
        Dictionary<string, Parameter> Params { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        ICollection<IUri> HeaderScriptLinks { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        List<string> HeaderScripts { get; set; }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        void Process();

        /// <summary>
        /// Weiterleitung an eine andere Seite
        /// Die Funktion löst die RedirectException aus 
        /// </summary>
        /// <param name="url">Die URL zu der weitergeleitet werden soll</param>
        void Redirecting(IUri url);

        /// <summary>
        /// Fügt eine Java-Script hinzu
        /// </summary>
        /// <param name="url">Der Link</param>
        void AddScriptLink(string url);

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        void AddScript(string key, string code);

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML</returns>
        IHtmlNode Render();
    }
}
