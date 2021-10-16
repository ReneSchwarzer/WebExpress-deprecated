using System;
using System.Collections.Generic;
using System.Globalization;

namespace WebExpress.WebResource
{
    public class PathSegmentVariable : IPathSegment
    {
        /// <summary>
        /// Liefert oder setzt den regulären Ausdruck
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring
        /// </summary>
        public Func<string, string, CultureInfo, string> CallBackDisplay { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Rückruffunktion zur Ermittlung der Variablen
        /// </summary>
        private Func<string, IDictionary<string, string>> CallBackVariables { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="expression">Der reguläre Ausdruck</param>
        /// <param name="callBackDisplay">Rückruffunktion zur Ermittlung des Anzeigestrings</param>
        /// <param name="callBackVariables">Rückruffunktion zur Ermittlung der Variablen</param>
        public PathSegmentVariable(string expression, Func<string, string, CultureInfo, string> callBackDisplay, Func<string, IDictionary<string, string>> callBackVariables)
        {
            Expression = expression;
            CallBackDisplay = callBackDisplay;
            CallBackVariables = callBackVariables;
        }

        /// <summary>
        /// Liefert den Anzeigenamen
        /// </summary>
        /// <param name="segment">Das Segemnt</param>
        /// <param name="pluginID">Die PlugiinID</param>
        /// <param name="culture">Die Kultur</param>
        /// <returns>Der Anzeigestring zum Segment</returns>
        public string GetDisplay(string segment, string pluginID, CultureInfo culture)
        {
            return CallBackDisplay(segment, pluginID, culture);
        }

        /// <summary>
        /// Liefert die Variablen
        /// </summary>
        /// <param name="segment">Das Segemnt</param>
        /// <returns>Die Variablen-Wert-Paare</returns>
        public IDictionary<string, string> GetVariables(string segment)
        {
            return CallBackVariables(segment);
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Der Baumknoten in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return Expression;
        }
    }
}
