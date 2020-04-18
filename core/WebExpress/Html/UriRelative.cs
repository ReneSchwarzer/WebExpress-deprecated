using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Relative Uri (z.B: /image.png)
    /// </summary>
    public class UriRelative : IUri
    {
        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        public List<IUriPathSegment> Path { get; protected set; } = new List<IUriPathSegment>();

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        public List<UriQuerry> Query { get; protected set; } = new List<UriQuerry>();

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

                return last?.Value;
            }
        }

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
            var fragment = uri.Split('#');
            if (fragment.Count() == 2)
            {
                Fragment = fragment[1];
            }

            var query = fragment[0].Split('?');
            if (query.Count() == 2)
            {
                foreach(var q in query[1].Split('&'))
                {
                    var item = q.Split('=');

                    Query.Add(new UriQuerry(item[0], item.Count() > 1 ? item[1] : null));
                }
            }

            foreach (var p in query[0].Split('/'))
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
            Path = uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        /// <returns>Die erweiterte Pfad</returns>
        public virtual IUri Append(string path)
        {
            var copy = new UriRelative(this);

            foreach (var p in path.Split('/'))
            {
                copy.Path.Add(new UriPathSegment(p));
            }

            return copy;
        }

        /// <summary>
        /// Liefere eine verkürzte Uri
        /// count > 0 es sind count-Elemente enthalten sind
        /// count < 0 es werden count-Elemente abgeshnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        public virtual IUri Take(int count)
        {
            var copy = new UriRelative(this);

            if (count > 0)
            {
                copy.Path = copy.Path.Take(count).ToList();
            }
            else if (count > 0 && count < copy.Path.Count)
            {
                copy.Path = copy.Path.Take(copy.Path.Count - count).ToList();
            }
            else
            {
                return new UriRelative();
            }

            return copy;
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
    }
}