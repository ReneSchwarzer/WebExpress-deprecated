using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControlRest
{
    internal interface IControlRest
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Daten ermittelt
        /// </summary>
        public IUri RestUri { get; set; }
    }
}
