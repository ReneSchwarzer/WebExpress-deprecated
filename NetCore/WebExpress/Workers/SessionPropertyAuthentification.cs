using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Workers
{
    public class SessionPropertyAuthentification : SessionProperty
    {
        /// <summary>
        /// Liefert oder setzt den Loginnamen
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Liefert oder setzt das Passwort
        /// </summary>
        public string Password { get; set; }
    }
}
