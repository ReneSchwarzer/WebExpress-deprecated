using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Html;
using WebExpress.Plugins;

namespace WebExpress.Pages
{
    public class UriPage : UriRelative
    {
        /// <summary>
        /// Liefert oder setzt die Variabeln
        /// </summary>
        public Dictionary<UriSegmentID, UriPathSegmentDynamic> Variables { get; protected set; } = new Dictionary<UriSegmentID, UriPathSegmentDynamic>();

        /// <summary>
        /// Liefert oder setzt, ob alle Subpfade micht berücksichtigt werden sollen
        /// </summary>
        public bool IncludeSubPaths { get; set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IPluginContext Context { get; set; }

        /// <summary>
        /// Ermittelt den Anzeigestring der Uri
        /// </summary>
        public override string Display
        {
            get
            {
                var last = Path.LastOrDefault();
                if (last is UriPathSegmentPage page)
                {
                    return !string.IsNullOrWhiteSpace(page?.Display) ? page?.Display : last?.Value;
                }

                return last?.Value;
            }
        }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public override IUri Root
        {
            get
            {
                var root = new UriPage(this);
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
        /// <param name="context">Der Kontext</param>
        public UriPage(IPluginContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="context">Der Kontext</param>
        public UriPage(string uri, IPluginContext context)
            : base(uri)
        {
            Context = context;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="uri">Die zu kopierende Uri</param>
        public UriPage(UriPage uri)
        {
            Context = uri.Context;
            IncludeSubPaths = uri.IncludeSubPaths;

            Path = uri.Path.Select
            (
                x =>
                x is UriPathSegmentPage ?
                new UriPathSegmentPage(x.Value, (x as UriPathSegmentPage).SegmentID, x.Tag) { Display = (x as UriPathSegmentPage).Display } as IUriPathSegment :
                new UriPathSegment(x.Value, x.Tag) as IUriPathSegment
            ).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
            Fragment = uri.Fragment;

            foreach (var v in uri.Variables)
            {
                Variables.Add
                (
                    v.Key,
                    new UriPathSegmentDynamic
                    (
                        new UriPathSegmentDynamicDisplay(v.Value.Display),
                        v.Value.Seperator,
                        v.Value.Items.Select(x => new UriPathSegmentVariable(x)).ToArray()
                    )
                );
            }
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path">Der anzufügende Pfad</param>
        /// <returns>Die erweiterte Pfad</returns>
        public override IUri Append(string path)
        {
            var copy = new UriPage(this);

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
            var copy = new UriPage(this);

            if (count > 0)
            {
                copy.Path = copy.Path.Take(count).ToList();
            }
            else if (count < 0 && count < copy.Path.Count)
            {
                copy.Path = copy.Path.Take(copy.Path.Count - Math.Abs(count)).ToList();
            }
            else
            {
                return new UriPage(Context);
            }

            return copy;
        }

        /// <summary>
        /// Liefere eine verkürzte Uri
        /// </summary>
        /// <param name="segmentID">Die SegmentID, bis zu dem (inklusive) Uri kopiert wird. Achtung! Die SeeegmentID ist in der Regel != dem Pfadsegment. </param>
        /// <returns>Die Teiluri</returns>
        public virtual IUri Take(string segmentID)
        {
            var path = Path.Select(x => x as UriPathSegmentPage).ToList();
            var index = path.FindIndex(x => x.SegmentID.Equals(segmentID.ToLower()));

            if (index < 0)
            {
                Context?.Log.Warning(MethodBase.GetCurrentMethod(), string.Format("SegmentID '{0}' wurde in der Uri '{1}' nicht gefunden.", segmentID, ToString()));
            }

            return Take(index + 1);
        }

        /// <summary>
        /// Prüft ob die angegebene SegmentID teil der Uri ist.
        /// </summary>
        /// <param name="segmentID">Die SegmentID</param>
        /// <returns>true wenn die SegmentID der Uri angehört, false sonst</returns>
        public virtual bool ContainsSegemtID(string segmentID)
        {
            var path = Path.Where(x => x is UriPathSegmentPage).Select(x => x as UriPathSegmentPage).ToList();

            if (path.Count() == 0)
            {
                return false;
            }

            return path.Where(x => x.SegmentID.Equals(segmentID)).Count() > 0;
        }

        /// <summary>
        /// Erstellt den Rohdaten-String der Uri
        /// </summary>
        /// <returns>Die Uri</returns>
        public string ToRawString()
        {
            var raw = new List<string>();
            foreach (var path in Path)
            {
                if (path is UriPathSegmentPage page)
                {
                    if (Variables.ContainsKey(page.SegmentID))
                    {
                        var variable = Variables[page.SegmentID];

                        raw.Add(string.Join(variable.Seperator, variable.Items.Select(x => x.Pattern)));
                    }
                    else
                    {
                        raw.Add(path.Value);
                    }
                }
                else
                {
                    raw.Add(path.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(Context?.UrlBasePath))
            {
                return Context?.UrlBasePath + "/" + string.Join("/", raw.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            return "/" + string.Join("/", raw.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Context?.UrlBasePath))
            {
                return Context?.UrlBasePath + base.ToString();
            }

            return base.ToString();
        }
    }
}
