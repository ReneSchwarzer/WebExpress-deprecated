using System.Collections.Generic;
using System;

namespace WebExpress.WebPackage
{
    public class PackageItem
    {
        /// <summary>
        /// Liefert oder setzt den Paketdateinamen
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Liefert oder setzt die Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Liefert oder setzt die Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die Autoren
        /// </summary>
        public string Authors { get; set; }

        /// <summary>
        /// Liefert oder setzt die Lizenz
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Liefert oder setzt die Url der Lizenz
        /// </summary>
        public string LicenseUrl { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon des Paketes
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Readme.Datei des Paketes (md-Format)
        /// </summary>
        public string Readme { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt die Tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Liefert oder setzt das Repositorie
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ahängigkeiten
        /// </summary>
        //public List<Dependency> Dependencies { get; set; }

        /// <summary>
        /// Ermittelt die im Paket enthaltenden Dateien
        /// </summary>
        public List<Tuple<string, byte[]>> Files { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal PackageItem()
        {
        }
    }
}
