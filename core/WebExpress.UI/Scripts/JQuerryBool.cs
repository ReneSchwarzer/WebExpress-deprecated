namespace WebExpress.UI.Scripts
{
    public class JQuerryBool
    {
        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public bool Value { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="propperty">Der Wert</param>
        public JQuerryBool(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Wandelt die Instanz in einem String um
        /// </summary>
        /// <returns>Die Zeichenkettenrepräsentation der Instanz</returns>
        public override string ToString()
        {
            return Value ? "'true'" : "'false'";
        }
    }
}
