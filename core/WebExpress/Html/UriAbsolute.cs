using System.Linq;

namespace WebExpress.Html
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
                    root.Path.RemoveAt(1);
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
            Path = uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
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
                copy.Path = copy.Path.Take(count).ToList();
            }
            else if (count > 0 && count < copy.Path.Count)
            {
                copy.Path = copy.Path.Take(copy.Path.Count - count).ToList();
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

            switch (Scheme)
            {
                case UriScheme.Mailto:
                    return string.Format("{0}{1}", scheme, authority);
                default:
                    return string.Format("{0}{1}{2}", scheme, authority, base.ToString());
            }
        }
    }
}