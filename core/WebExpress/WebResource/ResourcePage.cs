using System.Collections.Generic;
using System.Globalization;
using WebExpress.Html;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.Workers;

namespace WebExpress.WebResource
{
    public class ResourcePage : Resource, IPage
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt das Favicon
        /// </summary>
        public List<Favicon> Favicons { get; set; }

        /// <summary>
        /// Liefert oder setzt das internes Stylesheet  
        /// </summary>
        public List<string> Styles { get; set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        public ICollection<IUri> HeaderScriptLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        public List<string> ScriptLinks { get; set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        public List<string> HeaderScripts { get; set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        protected Dictionary<string, string> Scripts { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden Css-Dateien
        /// </summary>
        public ICollection<IUri> CssLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert oder setzt die Metainformationen
        /// </summary>
        public List<KeyValuePair<string, string>> Meta { get; private set; }

        /// <summary>
        /// Liefert den Inhalt der Ressource
        /// </summary>
        public object Content => Render();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="context">Der Kontext</param>
        public ResourcePage()
        {
            Favicons = new List<Favicon>();
            Styles = new List<string>();
            ScriptLinks = new List<string>();
            HeaderScripts = new List<string>();
            Scripts = new Dictionary<string, string>();

            Meta = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("charset", "UTF-8"),
                new KeyValuePair<string, string>("viewport", "width=device-width, initial-scale=1")
            };
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        public virtual void AddScript(string key, string code)
        {
            if (key == null) return;

            var k = key.ToLower();
            if (Scripts.ContainsKey(k))
            {
                Scripts[k] = code;
            }
            else
            {
                Scripts.Add(k, code);
            }
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu
        /// </summary>
        /// <param name="url">Der Link</param>
        public virtual void AddScriptLink(string url)
        {
            ScriptLinks.Add(url);
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return Render().ToString();
        }

        /// <summary>
        /// Weiterleitung an eine andere Seite
        /// Die Funktion löst die RedirectException aus
        /// </summary>
        /// <param name="uri">Die URL zu der weitergeleitet werden soll</param>
        public void Redirecting(IUri uri)
        {
            throw new RedirectException(uri?.ToString());
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML</returns>
        public virtual IHtmlNode Render()
        {
            return null;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            Request = request;

            Process();

            return new ResponseOK()
            {
                Content = Render()
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public virtual void Process()
        {
        }
    }
}
