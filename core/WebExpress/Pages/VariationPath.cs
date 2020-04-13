using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Plugins;

namespace WebExpress.Pages
{
    public class VariationPath
    {
        /// <summary>
        /// Liefert oder setzt die Pfadelemente
        /// </summary>
        public List<Path> Items { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        private IPluginContext Context { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(IPluginContext context, string tag, params IPathItem[] paths)
            : this(context, new VariationPath[] { }, tag, paths)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="basePath">Der Basispfad</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(VariationPath basePath, string tag, params IPathItem[] paths)
            : this(basePath.Context, new VariationPath[] { basePath }, tag, paths)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="basePath1">Der erste Basispfad</param>
        /// <param name="basePath2">Der zweite Basispfad</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(VariationPath basePath1, VariationPath basePath2, string tag, params IPathItem[] paths)
            : this
            (
                  basePath1.Context != null ? basePath1.Context : basePath2.Context,
                  new VariationPath[] { basePath1, basePath2 },
                  tag,
                  paths
            )
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="basePath1">Der erste Basispfad</param>
        /// <param name="basePath2">Der zweite Basispfad</param>
        /// <param name="basePath3">Der dritte Basispfad</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(VariationPath basePath1, VariationPath basePath2, VariationPath basePath3, string tag, params IPathItem[] paths)
            : this
            (
                  basePath1.Context != null ? basePath1.Context : basePath2.Context,
                  new VariationPath[] { basePath1, basePath2, basePath3 },
                  tag,
                  paths
            )
        {
            Context = basePath1.Context;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="basePath1">Der erste Basispfad</param>
        /// <param name="basePath2">Der zweite Basispfad</param>
        /// <param name="basePath3">Der dritte Basispfad</param>
        /// <param name="basePath4">Der vierte Basispfad</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(VariationPath basePath1, VariationPath basePath2, VariationPath basePath3, VariationPath basePath4, string tag, params IPathItem[] paths)
            : this
            (
                  basePath1.Context != null ? basePath1.Context : basePath2.Context,
                  new VariationPath[] { basePath1, basePath2, basePath3, basePath4 },
                  tag,
                  paths
            )
        {
            Context = basePath1.Context;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="basePath">Der Basispfad</param>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die zusammengehörigen Pfade</param>
        public VariationPath(IPluginContext context, VariationPath[] basePath, string tag, params IPathItem[] paths)
        {
            Context = context;

            Items = new List<Path>();

            foreach (var v in paths)
            {
                IPathItem i = null;

                if (v is PathItem item)
                {
                    i = new PathItem(item) { Tag = tag };
                }
                else if (v is PathItemVariable variable)
                {
                    i = new PathItemVariable(variable) { Tag = tag };
                }

                if (basePath.Count() > 0)
                {
                    foreach (var b in from b in basePath from pitem in b.Items select pitem)
                    {
                        Items.Add(new Path(b, i));
                    }
                }
                else
                {
                    Items.Add(new Path(Context, i));
                }
            }
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="tag">Eine Bezeichnung, welche dem Tag zugeordnet wird</param>
        /// <param name="paths">Die hinzuzufügenden Pfad-Elemente</param>
        public void AddSubPath(string tag, params PathItem[] paths)
        {
            var items = Items.ToList();

            foreach (var b in items)
            {
                foreach (var v in paths)
                {
                    var i = new PathItem(v) { Tag = tag };

                    Items.Add(new Path(b, i));
                }
            }
        }

        /// <summary>
        /// Liefert alle Pfad-Kombinationsmöglichkeiten bis zum Pfadelement mit dem gegebenen Namen
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Path> GetUrls(string name)
        {
            return Items.Where(x => x.Items.LastOrDefault().Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
