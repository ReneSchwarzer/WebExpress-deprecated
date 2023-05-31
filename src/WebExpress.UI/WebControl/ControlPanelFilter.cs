namespace WebExpress.UI.WebControl
{
    public class ControlPanelFilter : ControlForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
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
