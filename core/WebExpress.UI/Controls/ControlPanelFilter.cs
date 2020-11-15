namespace WebExpress.UI.Controls
{
    public class ControlPanelFilter : ControlFormular
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelFilter(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            SubmitButton.Text = "Aktualisieren";
        }
    }
}
