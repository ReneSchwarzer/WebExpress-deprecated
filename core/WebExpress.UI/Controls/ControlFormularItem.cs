namespace WebExpress.UI.Controls
{
    public abstract class ControlFormularItem : Control
    {
        /// <summary>
        /// Liefert oder setzt das zugehörige Formular
        /// </summary>
        public IControlFormular Formular { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Eingabefeldes
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItem(IControlFormular formular, string id)
            : base(formular.Page, id)
        {
            Formular = formular;
        }
    }
}
