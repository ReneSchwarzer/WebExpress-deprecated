using System.Collections.Generic;

namespace WebExpress.UI.Script
{
    public class JQuerryOn : JQuerry
    {
        /// <summary>
        /// Liefert oder setzt das Event
        /// </summary>
        public string Event { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt der Funktion
        /// </summary>
        public List<string> Function { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        /// <param name="eventName">Der Name des Events</param>
        public JQuerryOn(string selector, string eventName)
            : base(selector)
        {
            Event = eventName;
            Function = new List<string>();
        }

        /// <summary>
        /// Wandelt die Instanz in einem String um
        /// </summary>
        /// <returns>Die Zeichenkettenrepräsentation der Instanz</returns>
        public override string ToString()
        {
            return base.ToString() + ".on('" + Event + "', function () { " + string.Join(" ", Function) + " });";
        }
    }
}
