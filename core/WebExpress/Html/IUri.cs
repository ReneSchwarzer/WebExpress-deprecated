using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Uniform Resource Identifier (RFC 3986)
    /// </summary>
    public interface IUri
    {
        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        List<IUriPathSegment> Path { get; }

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        List<UriQuerry> Query { get; }

        /// <summary>
        /// Referenziert eine Stelle innerhalb einer Ressource (z.B. #Anker)
        /// </summary>
        string Fragment { get; set; }

        /// <summary>
        /// Liefert den Anzeigestring der Uri
        /// </summary>
        string Display { get; }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        IUri Root { get; }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        IUri Append(string path);

        /// <summary>
        /// Liefere eine verkürzte Uri
        /// count > 0 es sind count-Elemente enthalten sind
        /// count < 0 es werden count-Elemente abgeshnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        IUri Take(int count);

        /// <summary>
        /// Ermittelt, ob das gegebene Segment Teil der Uri ist
        /// </summary>
        /// <param name="segment">Das Segment, welches geprüft wird</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        bool Contains(string segment);
    }
}