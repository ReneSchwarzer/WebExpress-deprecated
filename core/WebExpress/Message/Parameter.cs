using System.Collections.Generic;
using System.Text;

namespace WebExpress.Message
{
    public class Parameter
    {
        /// <summary>
        /// liefert oder setzt den Gültigkeitsbereich des Parameters
        /// </summary>
        public ParameterScope Scope { get; private set; }

        /// <summary>
        /// Der Schlüssel
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Der Wert
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Parameter()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public Parameter(string key, string value, ParameterScope scope)
        {
            Key = key.ToLower();
            Value = value;
            Scope = scope;

            if (scope == ParameterScope.Parameter)
            {
                var decode = System.Web.HttpUtility.UrlDecode(value);
                Value = decode;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public Parameter(string key, int value, ParameterScope scope)
        {
            Key = key.ToLower();
            Value = value.ToString();
            Scope = scope;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public Parameter(string key, char value, ParameterScope scope)
        {
            Key = key.ToLower();
            Value = value.ToString();
            Scope = scope;
        }

        /// <summary>
        /// Erstellt eine Parameterliste
        /// </summary>
        /// <param name="param">Die Elemente der Parameterliste</param>
        /// <returns>Die Parameterliste</returns>
        public static List<Parameter> Create(params Parameter[] param)
        {
            return new List<Parameter>(param);
        }

        /// <summary>
        /// Umwandlung in Stringform
        /// </summary>
        /// <returns>Das Objekt in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            var sb = new StringBuilder(Value);

            sb.Replace("%", "%25"); // Achtung! & muss an erster Stelle stehen
            sb.Replace(" ", "%20");
            sb.Replace("!", "%21");
            sb.Replace("\"", "%22");
            sb.Replace("#", "%23");
            sb.Replace("$", "%24");
            sb.Replace("&", "%26");
            sb.Replace("'", "%27");
            sb.Replace("(", "%28");
            sb.Replace(")", "%29");
            sb.Replace("*", "%2A");
            sb.Replace("+", "%2B");
            sb.Replace(",", "%2C");
            sb.Replace("-", "%2D");
            sb.Replace(".", "%2E");
            sb.Replace("/", "%2F");
            sb.Replace(":", "%3A");
            sb.Replace(";", "%3B");
            sb.Replace("<", "%3C");
            sb.Replace("=", "%3D");
            sb.Replace(">", "%3E");
            sb.Replace("?", "%3F");
            sb.Replace("@", "%40");
            sb.Replace("[", "%5B");
            sb.Replace("\\", "%5C");
            sb.Replace("]", "%5D");
            sb.Replace("{", "%7B");
            sb.Replace("|", "%7C");
            sb.Replace("}", "%7D");

            return string.Format("{0}={1}", Key, sb.ToString());
        }

    }
}
