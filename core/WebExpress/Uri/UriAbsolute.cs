using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Uri
{
    /// <summary>
    /// Absolute Uri (z.B: https://de.wikipedia.org/wiki/Uniform_Resource_Identifier)
    /// </summary>
    public class UriAbsolute : UriRelative
    {
        /// <summary>
        /// Die Zuständigkeit
        /// </summary>
        public UriScheme Scheme { get; set; }

        /// <summary>
        /// Die Zuständigkeit (z.B. user@example.com:8080)
        /// </summary>
        public UriAuthority Authority { get; set; }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public override IUri Root
        {
            get
            {
                var root = new UriAbsolute(this);
                if (root.Path.Count > 1)
                {
                    (root.Path as List<IUriPathSegment>).AddRange(root.Path.Skip(1));
                }

                return root;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public UriAbsolute()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="uri">Die zu kopierende Uir</param>
        public UriAbsolute(UriAbsolute uri)
        {
            Scheme = uri.Scheme;
            Authority = uri.Authority;
            (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
            (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        /// <returns>Die erweiterte Pfad</returns>
        public override IUri Append(string path)
        {
            var copy = new UriAbsolute(this);

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
        public override IUri Take(int count)
        {
            var copy = new UriAbsolute(this);

            if (count > 0)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(copy.Path.Take(count));
            }
            else if (count > 0 && count < copy.Path.Count)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(copy.Path.Take(copy.Path.Count - count));
            }
            else
            {
                return new UriAbsolute();
            }

            return copy;
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            var scheme = Scheme.ToString("g").ToLower() + ":";
            var authority = Authority.ToString();

            return Scheme switch
            {
                UriScheme.Mailto => string.Format("{0}{1}", scheme, authority),
                _ => string.Format("{0}{1}{2}", scheme, authority, base.ToString()),
            };
        }
    }
}