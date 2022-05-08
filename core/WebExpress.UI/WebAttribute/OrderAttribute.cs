namespace WebExpress.UI.WebAttribute
{
    /// <summary>
    /// Attribut zur Kennzeichnun einer Klasse als Plugin-Komponente
    /// </summary>
    public class OrderAttribute : System.Attribute, IComponentAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="order">Die Reihenfolge innerhalb der Sektion</param>
        public OrderAttribute(int order)
        {
        }

    }
}
