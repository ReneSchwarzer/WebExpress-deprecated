using System.Collections.Generic;
using System.Globalization;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;
using WebExpress.Workers;

namespace WebExpress.WebResource
{
    public abstract class Resource : IResource
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
        /// Liefert oder setzt die URL, auf dem der Worker reagiert
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext indem die Ressource existiert
        /// </summary>
        public IModuleContext Context { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        public IReadOnlyList<string> ResourceContext { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Parameter der Seite
        /// </summary>
        public Dictionary<string, Parameter> Params { get; } = new Dictionary<string, Parameter>();

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Context?.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Initialization()
        {
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        public virtual void PreProcess(Request request)
        {
            return;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public abstract Response Process(Request request);

        /// <summary>
        /// Nachbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        public virtual Response PostProcess(Request request, Response response)
        {
            return response;
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

            //// alternativ Parameter in Session speichern
            //var session = Session.GetProperty<SessionPropertyParameter>();
            //if (session != null && session.Params != null && session.Params.ContainsKey(key))
            //{
            //    session.Params[key] = param;
            //}
            //else
            //{
            //    session.Params[key] = param;
            //}
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public Parameter GetParam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            if (Params.ContainsKey(name.ToLower()))
            {
                return Params[name.ToLower()];
            }

            if (Request.HasParam(name))
            {
                var value = Request.GetParam(name);
                return value;
            }

            if (Session == null)
            {
                return null;
            }

            // alternativ Parameter aus der Session ermitteln
            var session = Session.GetProperty<SessionPropertyParameter>();
            if (session != null && session.Params != null && session.Params.ContainsKey(name.ToLower()))
            {
                return session.Params[name.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public string GetParamValue(string name)
        {
            var param = GetParam(name);

            return param?.Value;
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <param name="defaultValue">Standardwert</param>
        /// <returns>Der Wert</returns>
        public int GetParamValue(string name, int defaultValue)
        {
            if (int.TryParse(GetParamValue(name), out var v))
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
            if (string.IsNullOrWhiteSpace(name)) return false;

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
    }
}
