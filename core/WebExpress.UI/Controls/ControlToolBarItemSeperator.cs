using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlToolBarItemSeperator : ControlLine, IControlToolBarItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlToolBarItemSeperator(IPage page, string id = null)
            : base(page, id)
        {

        }
    }
}
