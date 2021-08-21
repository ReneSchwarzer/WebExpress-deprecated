using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public interface IResource: II18N
    {
        /// <summary>
        /// Liefert die RessourcenID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Liefert die URL, auf dem der Worker reagiert
        /// </summary>
        IUri Uri { get; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext indem die Ressource existiert
        /// </summary>
        IModuleContext Context { get; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        void Initialization();

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        void PreProcess(Request request);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        Response Process(Request request);

        /// <summary>
        /// Nachbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        Response PostProcess(Request request, Response response);

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
        string GetParamValue(string name);

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <param name="defaultValue">Standardwert</param>
        /// <returns>Der Wert</returns>
        int GetParamValue(string name, int defaultValue);

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        bool HasParam(string name);
    }
}
