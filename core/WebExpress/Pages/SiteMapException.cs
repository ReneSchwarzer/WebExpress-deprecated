using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Pages
{
    public class SiteMapException : Exception
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="massgae">Die Ausnahmenachricht</param>
        public SiteMapException(string massgae)
            :base(massgae)
        {

        }
    }
}
