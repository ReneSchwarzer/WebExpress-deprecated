using System;
using System.Collections.Generic;

namespace WebExpress.Uri
{
    /// <summary>
    /// URI welche nur aus dem Fragment besteht (z.B. #)
    /// </summary>
    public class UriFragment : IUri
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public UriFragment()
        {
        }

        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        public ICollection<IUriPathSegment> Path => throw new NotImplementedException();

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        public ICollection<UriQuerry> Query => throw new NotImplementedException();

        /// <summary>
        /// Referenziert eine Stelle innerhalb einer Ressource (z.B. #Anker)
        /// </summary>
        public string Fragment { get; set; }

        public string Display { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring der Uri
        /// </summary>
        public bool Empty => throw new NotImplementedException();

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public IUri Root => throw new NotImplementedException();

        /// <summary>
        /// Ermittelt, ob es sich bei der Uri um die Wurzel handelt
        /// </summary>
        public bool IsRoot => throw new NotImplementedException();

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        public IUri Append(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ermittelt, ob das gegebene Segment Teil der Uri ist
        /// </summary>
        /// <param name="segment">Das Segment, welches geprüft wird</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        public bool Contains(string segment)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefere eine verkürzte Uri indem die ersten n-Elemente nicht enthalten sind
        /// count > 0 es werden count-Elemente übersprungen
        /// count <= 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        public IUri Skip(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prüft, ob eine gegebene Uri Teil dieser Uri ist
        /// </summary>
        /// <param name="uri">Die zu prüfende Uri</param>
        /// <returns>true, wenn Teil der Uri</returns>
        public bool StartsWith(IUri uri)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefere eine verkürzte Uri welche n-Elemente enthällt
        /// count > 0 es sind count-Elemente enthalten 
        /// count < 0 es werden count-Elemente abgeschnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        public IUri Take(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            return "#" + Fragment;
        }
    }
}