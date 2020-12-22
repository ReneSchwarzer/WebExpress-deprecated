using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Uri
{
    /// <summary>
    /// Relative Uri (z.B: /image.png)
    /// </summary>
    public class UriRelative : IUri
    {
        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        public ICollection<IUriPathSegment> Path { get; } = new List<IUriPathSegment>();

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        public ICollection<UriQuerry> Query { get; } = new List<UriQuerry>();

        /// <summary>
        /// Referenziert eine Stelle innerhalb einer Ressource (z.B. #Anker)
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Liefert den Anzeigestring der Uri
        /// </summary>
        public virtual string Display
        {
            get
            {
                var last = Path.LastOrDefault();
                return last?.Display;
            }

            set
            {
                var last = Path.LastOrDefault();
                if (last != null)
                {
                    last.Display = value;
                }
            }
        }

        /// <summary>
        /// Ermittelt, ob die Uri leer ist
        /// </summary>
        public bool Empty => Path.Count == 0;

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public virtual IUri Root
        {
            get
            {
                var root = new UriRelative(this);
                if (root.Path.Count > 1)
                {
                    return Take(1);
                }

                return root;
            }
        }

        /// <summary>
        /// Ermittelt, ob es sich bei der Uri um die Wurzel handelt
        /// </summary>
        public bool IsRoot => string.IsNullOrWhiteSpace(Path.FirstOrDefault()?.Value);

        /// <summary>
        /// Konstruktor
        /// </summary>
        public UriRelative()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">die Uri</param>
        public UriRelative(string uri)
        {
            if (uri == null) return;

            var fragment = uri.TrimEnd('/').Split('#');
            if (fragment.Count() == 2)
            {
                Fragment = fragment[1];
            }

            var query = fragment[0].Split('?');
            if (query.Count() == 2)
            {
                foreach (var q in query[1].Split('&'))
                {
                    var item = q.Split('=');

                    Query.Add(new UriQuerry(item[0], item.Count() > 1 ? item[1] : null));
                }
            }

            Path.Add(new UriPathSegment(null, "root"));

            foreach (var p in query[0].Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                Path.Add(new UriPathSegment(p));
            }
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="uri">Die zu kopierende Uir</param>
        public UriRelative(UriRelative uri)
        {
            Path = uri.Path.Select(x => new UriPathSegment(x) as IUriPathSegment).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        /// <returns>Der erweiterte Pfad</returns>
        public virtual IUri Append(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return this;
            }

            var copy = new UriRelative(this);

            foreach (var p in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                copy.Path.Add(new UriPathSegment(p));
            }

            return copy;
        }

        /// <summary>
        /// Liefere eine verkürzte Uri indem die ersten n-Elemente enthalten sind
        /// count > 0 es sind count-Elemente enthalten 
        /// count < 0 es werden count-Elemente abgeshnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        public virtual IUri Take(int count)
        {
            var copy = new UriRelative(this);
            var path = copy.Path.ToList();
            copy.Path.Clear();

            if (count == 0)
            {
                return new UriRelative();
            }
            else if (count > 0)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(path.Take(count));
            }
            else if (count < 0 && Math.Abs(count) < path.Count)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(path.Take(path.Count + count));
            }
            else
            {
                return null;
            }

            return copy;
        }

        /// <summary>
        /// Liefere eine verkürzte Uri indem die ersten n-Elemente nicht enthalten sind
        /// count > 0 es werden count-Elemente übersprungen
        /// count <= 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri oder null</returns>
        public IUri Skip(int count)
        {
            if (count >= Path.Count)
            {
                return null;
            }
            if (count > 0)
            {
                var copy = new UriRelative(this);
                var path = copy.Path.ToList();
                copy.Path.Clear();
                (copy.Path as List<IUriPathSegment>).AddRange(path.Skip(count));

                return copy;
            }

            return new UriRelative(this);
        }

        /// <summary>
        /// Ermittelt, ob das gegebene Segment Teil der Uri ist
        /// </summary>
        /// <param name="segment">Das Segment, welches geprüft wird</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        public virtual bool Contains(string segment)
        {
            return Path.Where(x => x.Value.Equals(segment, StringComparison.OrdinalIgnoreCase)).Count() > 0;
        }

        /// <summary>
        /// Prüft, ob eine gegebene Uri Teil dieser Uri ist
        /// </summary>
        /// <param name="uri">Die zu prüfende Uri</param>
        /// <returns>true, wenn Teil der Uri</returns>
        public bool StartsWith(IUri uri)
        {
            return ToString().StartsWith(uri.ToString());
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            var path = "/" + string.Join("/", Path.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Value));

            return string.Format("{0}", path);
        }

        /// <summary>
        /// Verknüft die angegebenen uris zu einer relaiven Uri
        /// </summary>
        /// <param name="uris">Die zu verknüpfenden Uris</param>
        /// <returns>Eine kombinierte Uri</returns>
        public static UriRelative Combine(params string[] uris)
        {
            var uri = new UriRelative();

            uri.Path.Add(new UriPathSegment(null, "root"));
            (uri.Path as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                            .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                            .Select(x => new UriPathSegment(x) as IUriPathSegment));
                            
            return uri;
        }

        /// <summary>
        /// Verknüft die angegebenen uris zu einer relaiven Uri
        /// </summary>
        /// <param name="uris">Die zu verknüpfenden Uris</param>
        /// <returns>Eine kombinierte Uri</returns>
        public static UriRelative Combine(params IUri[] uris)
        {
            var uri = new UriRelative();
            (uri.Path as List<IUriPathSegment>).AddRange(uris.Where(x => x != null).SelectMany(x => x.Path));
            
            return uri;
        }

        /// <summary>
        /// Verknüft die angegebenen uris zu einer relaiven Uri
        /// </summary>
        /// <param name="uri">Eine Uri</param>
        /// <param name="uris">Die zu verknüpfenden Uris</param>
        /// <returns>Eine kombinierte Uri</returns>
        public static UriRelative Combine(IUri uri, params string[] uris)
        {
            var copy = new UriRelative(uri as UriRelative);
            (copy.Path as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                    .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new UriPathSegment(x) as IUriPathSegment));
            

            return copy;
        }

    }
}