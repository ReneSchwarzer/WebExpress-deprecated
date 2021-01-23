using System.Collections.Generic;

namespace WebExpress.Uri
{
    /// <summary>
    /// Uniform Resource Identifier (RFC 3986)
    /// </summary>
    public interface IUri
    {
        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        ICollection<IUriPathSegment> Path { get; }

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        ICollection<UriQuerry> Query { get; }

        /// <summary>
        /// Referenziert eine Stelle innerhalb einer Ressource (z.B. #Anker)
        /// </summary>
        string Fragment { get; set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring der Uri
        /// </summary>
        string Display { get; set; }

        /// <summary>
        /// Ermittelt, ob die Uri leer ist
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        IUri Root { get; }

        /// <summary>
        /// Ermittelt, ob es sich bei der Uri um die Wurzel handelt
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        IUri Append(string path);

        /// <summary>
        /// Liefere eine verkürzte Uri welche n-Elemente enthällt
        /// count > 0 es sind count-Elemente enthalten 
        /// count < 0 es werden count-Elemente abgeschnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        IUri Take(int count);

        /// <summary>
        /// Liefere eine verkürzte Uri indem die ersten n-Elemente nicht enthalten sind
        /// count > 0 es werden count-Elemente übersprungen
        /// count <= 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        IUri Skip(int count);

        /// <summary>
        /// Ermittelt, ob das gegebene Segment Teil der Uri ist
        /// </summary>
        /// <param name="segment">Das Segment, welches geprüft wird</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        bool Contains(string segment);

        /// <summary>
        /// Prüft, ob eine gegebene Uri Teil dieser Uri ist
        /// </summary>
        /// <param name="uri">Die zu prüfende Uri</param>
        /// <returns>true, wenn Teil der Uri</returns>
        bool StartsWith(IUri uri);
    }
}