using System.Collections.Generic;

namespace WebExpress.WebApp.Model
{
    public class WebItemUser : WebItem
    {
        /// <summary>
        /// Die Bezeichnung des Objektes
        /// </summary>
        public override string Label => !string.IsNullOrWhiteSpace(Firstname) ? $"{Lastname}, {Firstname}" : Lastname;

        /// <summary>
        /// Die Name des Objektes
        /// </summary>
        public override string Name => Label;

        /// <summary>
        /// Liefert oder setzt den Loginnamen
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Liefert oder setzt den Vornamen
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Liefert oder setzt den Nachnamen
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Liefert oder setzt die Emailadresse
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Liefert oder setzt die Gruppen
        /// </summary>
        public IEnumerable<WebItemGroup> Groups { get; set; }
    }
}
