using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlToolBarItemButton : ControlLink, IControlToolBarItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlToolBarItemButton(IPage page, string id = null)
            : base(page, id)
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
