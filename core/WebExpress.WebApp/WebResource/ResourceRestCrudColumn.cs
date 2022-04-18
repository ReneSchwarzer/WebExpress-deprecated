using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Metainformationen einer CRUD-Tabellensplate 
    /// </summary>
    public class ResourceRestCrudColumn
    {
        /// <summary>
        /// Liefert oder setzt die Beschriftung der Splalte
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon der Spalte oder null
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weite der Tabellenspalte in %, null für auto
        /// </summary>
        public uint? Width { get; set; }

        /// <summary>
        /// Liefert oder setzt den Javascriptcode, welche die Daten der Zelle rendert
        /// </summary>
        public string Render { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="label">Die Beschriftung der Splalte</param>
        public ResourceRestCrudColumn(string label)
        {
            Label = label;
        }
    }
}
