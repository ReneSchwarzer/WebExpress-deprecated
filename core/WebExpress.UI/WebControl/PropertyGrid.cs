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
        /// Konstruktor
        /// </summary>
        public PropertyGrid()
        {
            Columns = 1;
        }

        /// <summary>
        /// Konstruktor
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
            switch (Device)
            {
                case TypeDevice.Auto:
                    return "col-" + Columns;
                case TypeDevice.ExtraSmall:
                    return "col-xs" + Columns;
                case TypeDevice.Small:
                    return "col-sm-" + Columns;
                case TypeDevice.Medium:
                    return "col-md-" + Columns;
                case TypeDevice.Large:
                    return "col-lg-" + Columns;
                case TypeDevice.ExtraLarge:
                    return "col-xl-" + Columns;
            }

            return null;
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
