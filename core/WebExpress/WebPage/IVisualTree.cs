using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Uri;

namespace WebExpress.WebPage
{
    public interface IVisualTree
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Liefert oder setzt das Favicon
        /// </summary>
        List<Favicon> Favicons { get; }

        /// <summary>
        /// Liefert oder setzt das internes Stylesheet  
        /// </summary>
        List<string> Styles { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        List<IUri> HeaderScriptLinks { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        List<string> ScriptLinks { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        List<string> HeaderScripts { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        IDictionary<string, string> Scripts { get; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden Css-Dateien
        /// </summary>
        List<IUri> CssLinks { get; }

        /// <summary>
        /// Liefert oder setzt die Metainformationen
        /// </summary>
        List<KeyValuePair<string, string>> Meta { get; }

        /// <summary>
        /// Fügt eine Java-Script hinzu
        /// </summary>
        /// <param name="url">Der Link</param>
        void AddScriptLink(string url);

        /// <summary>
        /// Fügt eine Java-Script im Header hinzu
        /// </summary>
        /// <param name="url">Der Link</param>
        void AddHeaderScriptLinks(IUri url);

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        void AddScript(string key, string code);

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        /// <returns>Die Seite als HTML</returns>
        IHtmlNode Render(RenderContext context);
    }
}
