namespace WebExpress.UI.Scripts
{
    public class JQuerryIs : JQuerry
    {
        /// <summary>
        /// Liefert oder setzt die Eigenschaft
        /// </summary>
        public string Propperty { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        /// <param name="propperty">Der Name der Eigenschaft</param>
        public JQuerryIs(string selector, string propperty)
            : base(selector)
        {
            Propperty = propperty;
        }

        /// <summary>
        /// Wandelt die Instanz in einem String um
        /// </summary>
        /// <returns>Die Zeichenkettenrepräsentation der Instanz</returns>
        public override string ToString()
        {
            return base.ToString() + ".is(':" + Propperty + "')";
        }
    }
}
