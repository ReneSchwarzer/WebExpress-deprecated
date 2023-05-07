namespace WebExpress.WebApp.WebApiControl
{
    internal interface IControlApi
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Daten ermittelt
        /// </summary>
        public string RestUri { get; set; }
    }
}
