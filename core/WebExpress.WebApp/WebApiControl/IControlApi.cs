using WebExpress.WebUri;

namespace WebExpress.WebApp.WebApiControl
{
    internal interface IControlApi
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Daten ermittelt
        /// </summary>
        public IUri RestUri { get; set; }
    }
}
