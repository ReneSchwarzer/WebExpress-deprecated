namespace WebExpress.UI.WebControl
{
    public class PropertyGrid : IProperty
    {
        /// <summary>
        /// Liefert oder setzt das zu verwendende Anzeigegerät
        /// </summary>
        public TypeDevice Device { get; private set; }

        /// <summary>
        /// Die Anzahl der verwendeten Spalten 
        /// Hinweis: Alle Spalten innerhalb eines PannelGrids muss die Summer 12 ergeben!
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyGrid()
        {
            Columns = 1;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Bestimmt, ob ein einheitlicher Rahmen angezeigt werden soll</param>
        /// <param name="columns">Die Anzahl der verwendeten Spalten. Hinweis: Alle Spalten innerhalb eines PannelGrids muss die Summer 12 ergeben!</param>
        public PropertyGrid(TypeDevice device, int columns)
        {
            Device = device;
            Columns = columns;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Rahmen gehörende CSS-KLasse</returns>
        public string ToClass()
        {
            return Device switch
            {
                TypeDevice.Auto => "col",
                TypeDevice.ExtraSmall => "col-" + Columns,
                TypeDevice.Small => "col-sm-" + Columns,
                TypeDevice.Medium => "col-md-" + Columns,
                TypeDevice.Large => "col-lg-" + Columns,
                TypeDevice.ExtraLarge => "col-xl-" + Columns,
                _ => null,
            };
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public virtual string ToStyle()
        {
            return null;
        }
    }
}
