namespace WebExpress.UI.Scripts
{
    public class JQuerry
    {
        /// <summary>
        /// Liefert oder setzt den Selector
        /// </summary>
        public string Selector { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selector</param>
        public JQuerry(string selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Wandelt die Instanz in einem String um
        /// </summary>
        /// <returns>Die Zeichenkettenrepräsentation der Instanz</returns>
        public override string ToString()
        {
            return "$('#" + Selector + "')";
        }
    }
}
