using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;
using WebExpress.Workers;

namespace WebExpress.WebResource
{
    public class ResourcePage : Resource, IPage
    {
        /// <summary>
        /// Liefert oder setzt die Session
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt der Seite
        /// </summary>
        public Request Request { get; set; }

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
        /// Liefert oder setzt die Parameter der Seite
        /// </summary>
        public Dictionary<string, Parameter> Params { get; private set; }

        /// <summary>
        /// Liefert den Inhalt der Ressource
        /// </summary>
        public object Content => Render();

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Context.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }
       
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="context">Der Kontext</param>
        public ResourcePage()
        {
            Params = new Dictionary<string, Parameter>();

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
        /// Fügt ein Parameter hinzu. Der Wert wird aus dem Request ermittelt
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(string name, ParameterScope scope = ParameterScope.Global)
        {
            AddParam(name, Request.GetParamValue(name), scope);
        }

        /// <summary>
        /// Fügt ein Parameter hinzu.
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(string name, string value, ParameterScope scope = ParameterScope.Global)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddParam(new Parameter(name, value) { Scope = scope });
            }
        }

        /// <summary>
        /// Fügt ein Parameter hinzu. 
        /// </summary>
        /// <param name="param">Der Parameter</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(Parameter param)
        {
            var key = param.Key.ToLower();

            if (param.Scope != ParameterScope.Session)
            {
                if (!Params.ContainsKey(key))
                {
                    Params.Add(key, param);
                }
                else
                {
                    Params[key] = param;
                }

                return;
            }

            // alternativ Parameter in Session speichern
            var session = Session.GetProperty<SessionPropertyParameter>();
            if (session != null && session.Params != null && session.Params.ContainsKey(key))
            {
                session.Params[key] = param;
            }
            else
            {
                session.Params[key] = param;
            }
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public string GetParam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            if (Params.ContainsKey(name.ToLower()))
            {
                return Params[name.ToLower()].Value;
            }

            if (Request.HasParam(name))
            {
                var value = Request.GetParamValue(name);
                return value;
            }

            if (Session == null)
            {
                return string.Empty;
            }

            // alternativ Parameter aus der Session ermitteln
            var session = Session.GetProperty<SessionPropertyParameter>();
            if (session != null && session.Params != null && session.Params.ContainsKey(name.ToLower()))
            {
                return session.Params[name.ToLower()].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <param name="defaultValue">Standardwert</param>
        /// <returns>Der Wert</returns>
        public int GetParam(string name, int defaultValue)
        {
            if (int.TryParse(GetParam(name), out var v))
            {
                return v;
            }

            return defaultValue;
        }

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        public bool HasParam(string name)
        {
            if (Params.ContainsKey(name.ToLower()))
            {
                return true;
            }

            if (Request.HasParam(name))
            {
                return true;
            }

            // alternativ Parameter aus der Session ermitteln
            var session = Session?.GetProperty<SessionPropertyParameter>();
            if (session != null && session.Params != null && session.Params.ContainsKey(name.ToLower()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        public virtual void AddScript(string key, string code)
        {
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
