namespace WebExpress.UI.Scripts
{
    public class JQuerryPropperty : JQuerry
    {
        /// <summary>
        /// Liefert oder setzt die Eigenschaft
        /// </summary>
        public string Propperty { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Wert der Eigenschaft
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        /// <param name="propperty">Der Name der Eigenschaft</param>
        public JQuerryPropperty(string selector, string propperty, string value)
            : base(selector)
        {
            Propperty = propperty;
            Value = value;
        }

        /// <summary>
        /// Wandelt die Instanz in einem String um
        /// </summary>
        /// <returns>Die Zeichenkettenrepräsentation der Instanz</returns>
        public override string ToString()
        {
            return base.ToString() + ".prop('" + Propperty + "', " + Value + " );";
        }
    }
}
