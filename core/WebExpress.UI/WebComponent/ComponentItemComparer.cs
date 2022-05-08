using System;
using System.Collections.Generic;

namespace WebExpress.UI.WebComponent
{
    /// <summary>
    /// Repräsentiert ein Komponeteneintrag im Komponentenverzeichnis
    /// </summary>
    internal class ComponentItemComparer : IComparer<ComponentItem>
    {
        /// <summary>
        /// Vergleicht zwei Objekte und gibt einen Wert zurück, der angibt, ob ein Wert niedriger, gleich oder größer als der andere Wert ist.
        /// </summary>
        /// <param name="x">Das erste zu vergleichende Objekt.</param>
        /// <param name="y">Das zweite zu vergleichende Objekt.</param>
        /// <returns>Eine ganze Zahl mit Vorzeichen, die die relativen Werte von x und y angibt. Kleiner als 0 (null) => x ist kleiner als y. Größer als 0 (null) => x ist größer als y. 0 (null) => x ist gleich y.</returns>
        public int Compare(ComponentItem x, ComponentItem y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order < y.Order)
            { 
                return -1;
            }

            return 0;
        }
    }
}
