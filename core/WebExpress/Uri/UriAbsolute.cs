using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebExpress.Uri
{
    /// <summary>
    /// Absolute Uri (z.B: https://de.wikipedia.org/wiki/Uniform_Resource_Identifier)
    /// </summary>
    public class UriAbsolute : UriRelative
    {
        /// <summary>
        /// Pattern der Uri
        /// </summary>
        private const string Pattern = "^([a-z0-9+.-]+):(?://(?:((?:[a-z0-9-._~!$&'()*+,;=:]|%[0-9A-F]{2})*)@)?((?:[a-z0-9-._~!$&'()*+,;=]|%[0-9A-F]{2})*)(?::(\\d*))?(.*)?)$";

        /// <summary>
        /// Der Typ
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
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die Url</param>
        public UriAbsolute(string url)
        {
            if (url == null) return;

            var match = Regex.Match(url, Pattern);

            try
            {
                Scheme = (UriScheme)Enum.Parse(typeof(UriScheme), match.Groups[1].Value, true);
            }
            catch
            {
                Scheme = UriScheme.Http;
            }

            Authority = new UriAuthority() 
            { 
                User = match.Groups[2].Value,
                Host = match.Groups[3].Value,
                Port = !string.IsNullOrWhiteSpace(match.Groups[4].Value) ? Convert.ToInt32(match.Groups[4].Value) : null
            };

            var uri = new UriRelative(match.Groups[5].Value);
            (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
            (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="authority">Die Zuständigkeit (z.B. user@example.com:8080)</param>
        /// <param name="uri">Die Uri</param>
        public UriAbsolute(UriScheme scheme, UriAuthority authority, IUri uri)
        {
            Scheme = scheme;
            Authority = authority;

            if (uri != null)
            {
                (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
                (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
                Fragment = uri.Fragment;
            }
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="uri">Die zu kopierende Uri</param>
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