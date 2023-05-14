namespace WebExpress.UI.WebControl
{
    public class ControlPanelFilter : ControlFormular
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelFilter(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            SubmitButton.Text = "Aktualisieren";
        }
    }
}
