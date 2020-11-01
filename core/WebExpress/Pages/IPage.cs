using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Plugins;
using WebExpress.Workers;

namespace WebExpress.Pages
{
    public interface IPage
    {
        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        IPluginContext Context { get; set; }

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
        List<string> CssLinks { get; set; }

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
        List<string> HeaderScriptLinks { get; set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        List<string> HeaderScripts { get; set; }

        /// <summary>
        /// Liefert oder setzt die Uri der Seite
        /// </summary>
        UriPage Uri { get; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        void Init();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="path">Der Pfad</param>
        /// <param name="session">Die aktuelle Session</param>
        void Init(UriPage path, Session session);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        void Process();

        /// <summary>
        /// Fügt ein Parameter hinzu. Der Wert wird aus dem Request ermittelt
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        void AddParam(string name, ParameterScope scope = ParameterScope.Global);

        /// <summary>
        /// Fügt ein Parameter hinzu.
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        void AddParam(string name, string value, ParameterScope scope = ParameterScope.Global);

        /// <summary>
        /// Fügt ein Parameter hinzu. 
        /// </summary>
        /// <param name="param">Der Parameter</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        void AddParam(Parameter param);

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        string GetParam(string name);

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <param name="defaultValue">Standardwert</param>
        /// <returns>Der Wert</returns>
        int GetParam(string name, int defaultValue);

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        bool HasParam(string name);

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
