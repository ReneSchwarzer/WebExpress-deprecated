namespace WebExpress.UI.Controls
{
    public class ControlToolBarItemButton : ControlLink, IControlToolBarItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlToolBarItemButton(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Classes.Add("nav-link");
        }
    }
}
