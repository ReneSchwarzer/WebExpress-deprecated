using System.Collections.Generic;
using System.Globalization;
using WebExpress.Module;
using WebExpress.WebResource;

namespace WebExpress.Uri
{
    public class UriResource : IUri
    {
        /// <summary>
        /// Liefert oder setzt den Kontextpfad
        /// </summary>
        public IUri ContextPath { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Uri
        /// </summary>
        private IUri Uri { get; set; }

        /// <summary>
        /// Der Pfad (z.B. /over/there)
        /// </summary>
        public ICollection<IUriPathSegment> Path => Uri.Path;

        /// <summary>
        /// Der Abfrageteil (z.B. ?title=Uniform_Resource_Identifier&action=submit)
        /// </summary>
        public ICollection<UriQuerry> Query => Uri.Query;

        /// <summary>
        /// Referenziert eine Stelle innerhalb einer Ressource (z.B. #Anker)
        /// </summary>
        public string Fragment
        {
            get { return Uri.Fragment; }
            set { Uri.Fragment = value; }
        }

        /// <summary>
        /// Liefert den Anzeigestring der Uri
        /// </summary>
        public string Display { get { return Uri.Display; } set { Uri.Display = value; } }

        /// <summary>
        /// Ermittelt, ob die Uri leer ist
        /// </summary>
        public bool Empty => Uri.Empty;

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public IUri Root => new UriResource(ContextPath);

        /// <summary>
        /// Ermittelt, ob es sich bei der Uri um die Wurzel handelt
        /// </summary>
        public bool IsRoot => Uri.IsRoot;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contextPath">Der Kontextpfad</param>
        /// <param name="uri">Die eigentliche Uri</param>
        public UriResource(IUri contextPath, IUri uri = null)
        {
            ContextPath = contextPath;
            Uri = uri ?? new UriRelative();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contextPath">Der Kontextpfad</param>
        /// <param name="uri">Die eigentliche Uri</param>
        public UriResource(IUri contextPath, string uri)
        {
            ContextPath = contextPath;
            Uri = string.IsNullOrWhiteSpace(uri) ? new UriRelative() : new UriRelative(uri);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Moduls</param>
        /// <param name="url">Die eigentliche Uri, welche vom Webbrowser aufgerufen wurde</param>
        /// <param name="node">Der Knoten der Sitemap</param>
        /// <param name="culture">Die Kultur</param>
        internal UriResource(IModuleContext context, IUri url, SitemapNode node, CultureInfo culture)
            : this(context, url.ToString(), node?.Path, culture)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Moduls</param>
        /// <param name="url">Die eigentliche Uri, welche vom Webbrowser aufgerufen wurde</param>
        /// <param name="node">Der Knoten der Sitemap</param>
        /// <param name="culture">Die Kultur</param>
        internal UriResource(IModuleContext context, IUri url, SearchResult node, CultureInfo culture)
            : this(context, url.ToString(), node?.Path, culture)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Moduls</param>
        /// <param name="url">Die eigentliche Uri, welche vom Webbrowser aufgerufen wurde</param>
        /// <param name="node">Der Knoten der Sitemap</param>
        /// <param name="culture">Die Kultur</param>
        internal UriResource(IModuleContext context, string url, ICollection<SitemapNode> path, CultureInfo culture)
    : this(context.ContextPath, new UriRelative())
        {
            var uri = new UriRelative(url[context.ContextPath.ToString().Length..]);
            var uriPath = uri.Path as List<IUriPathSegment>;
            var nodePath = path as List<SitemapNode>;

            for (var i = 0; i < uriPath.Count; i++)
            {
                var u = uriPath[i];

                if (i < nodePath.Count)
                {
                    var item = new UriPathSegment(u.Value, u.Tag) { Display = nodePath[i]?.PathSegment?.GetDisplay(u.ToString(), context.ModuleID, culture) };
                    Uri.Path.Add(item);
                }
                else
                {
                    var item = new UriPathSegment(u.Value, u.Tag);
                    Uri.Path.Add(item);
                }
            }
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        public IUri Append(string path)
        {
            return new UriResource(ContextPath, UriRelative.Combine(Uri, path));
        }

        /// <summary>
        /// Liefere eine verkürzte Uri welche n-Elemente enthällt
        /// count > 0 es sind count-Elemente enthalten 
        /// count < 0 es werden count-Elemente abgeshnitten
        /// count = 0 es wird eine leere Uri zurückgegeben
        /// </summary>
        /// <param name="count">Die Anzahl</param>
        /// <returns>Die Teiluri</returns>
        public IUri Take(int count)
        {
            return new UriResource(ContextPath, Uri.Take(count));
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
            return new UriResource(ContextPath, Uri.Skip(count));
        }

        /// <summary>
        /// Ermittelt, ob das gegebene Segment Teil der Uri ist
        /// </summary>
        /// <param name="segment">Das Segment, welches geprüft wird</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        public bool Contains(string segment)
        {
            return Uri.Contains(segment);
        }

        /// <summary>
        /// Prüft, ob eine gegebene Uri Teil dieser Uri ist
        /// </summary>
        /// <param name="uri">Die zu prüfende Uri</param>
        /// <returns>true, wenn Teil der Uri</returns>
        public bool StartsWith(IUri uri)
        {
            return Uri.ToString().StartsWith(uri.ToString());
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            return "/" + string.Join("/", ContextPath.ToString().Trim('/'), Uri.ToString().Trim('/')).Trim('/');
        }
    }
}
