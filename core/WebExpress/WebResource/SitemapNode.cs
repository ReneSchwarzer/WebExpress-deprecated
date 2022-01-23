using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.WebResource
{
    public class SitemapNode
    {
        /// <summary>
        /// Liefert oder setzt die RessourcenID
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Resourcentitel
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Pfadsegment
        /// </summary>
        public IPathSegment PathSegment { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Liefert die Instanz der Ressource
        /// </summary>
        private IResource Instance { get; set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IResourceContext Context { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Kontext indem die Ressource existiert
        /// </summary>
        public IReadOnlyList<string> ResourceContext { get; internal set; }

        /// <summary>
        /// Liefert die untergeordneten Knoten
        /// </summary>
        public ICollection<SitemapNode> Children { get; } = new List<SitemapNode>();

        /// <summary>
        /// Liefert den Elternknoten
        /// </summary>
        public SitemapNode Parent { get; private set; }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        public SitemapNode Root
        {
            get
            {
                if (IsRoot)
                {
                    return this;
                }

                var parent = Parent;
                while (!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

        /// <summary>
        /// Prüft, ob der Knoten die Wurzel ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        public bool IsRoot => (Parent == null);

        /// <summary>
        /// Prüft, ob Knoten ein Blatt ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        public bool IsLeaf => (Children.Count == 0);

        /// <summary>
        /// Liefert oder setzt, ob alle Subpfade mit berücksichtigt werden sollen
        /// </summary>
        public bool IncludeSubPaths { get; internal set; }

        /// <summary>
        /// Kennzeichnet den Knoten als Dummy ohne zugeordnete Ressource
        /// </summary>
        public bool Dummy { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SitemapNode()
        {

        }

        /// <summary>
        /// Durchläuft den Baum in PreOrder
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        public ICollection<SitemapNode> GetPreOrder()
        {
            var list = new List<SitemapNode>
            {
                this
            };

            foreach (var child in Children)
            {
                list.AddRange(child.GetPreOrder());
            }

            return list;
        }

        /// <summary>
        /// Liefert den Pfad
        /// </summary>
        /// <returns>Der Pfad</returns>
        public ICollection<SitemapNode> Path
        {
            get
            {
                var list = new List<SitemapNode>
                {
                    this
                };

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent);

                    parent = parent.Parent;
                }

                list.Reverse();

                return list;
            }
        }

        /// <summary>
        /// Liefert den Pfad mit den enentuell vorhandenen regulären Ausdrücken
        /// </summary>
        /// <returns>Der Pfad</returns>
        public string ExpressionPath
        {
            get
            {
                if (Parent == null)
                {
                    return "/";
                }

                var list = new List<string>
                (
                    new string[] { PathSegment?.ToString() }
                );

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent.PathSegment?.ToString());

                    parent = parent.Parent;
                }

                list.Reverse();

                return string.Join("/", list);
            }
        }

        /// <summary>
        /// Liefert den Pfad mit den enentuell vorhandenen Knoten
        /// </summary>
        /// <returns>Der Pfad</returns>
        public string IDPath
        {
            get
            {
                var list = new List<string>
                (
                    new string[] { ID }
                );

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent.ID);

                    parent = parent.Parent;
                }

                list.Reverse();

                return string.Join("/", list);
            }
        }

        /// <summary>
        /// Fügt ein Element ein
        /// </summary>
        /// <param name="uri">Der Pfad zu der Ressource</param>
        /// <param name="id">Die RessourcenID</param>
        public SitemapNode Insert(IUri uri, string id)
        {
            // Echter Eintrag
            if (uri == null || uri.Path.Count < 1)
            {
                if (Children.Where(x => x.ID.Equals(id)).FirstOrDefault() is SitemapNode existingChild)
                {
                    // Ressource bereits vorhanden
                    if (existingChild.Dummy)
                    {
                        existingChild.Dummy = false;

                        return existingChild;
                    }

                    return null;
                }

                var child = new SitemapNode()
                {
                    Parent = this,
                    ID = id,
                    Dummy = false
                };

                Children.Add(child);

                return child;
            }

            var first = uri.Take(1).Path.FirstOrDefault();
            var children = Children.Where(x => x.ID.Equals(first.Value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (children == null)
            {
                // Dummy anlegen
                var dummy = new SitemapNode()
                {
                    Parent = this,
                    ID = first.Value,
                    PathSegment = new PathSegmentConstant(first.Value?.ToLower(), string.Empty),
                    Title = string.Empty,
                    Dummy = true
                };

                Children.Add(dummy);

                return dummy.Insert(uri.Skip(1), id);
            }

            return children.Insert(uri.Skip(1), id);
        }

        /// <summary>
        /// Sucht ein Item anahnd der ID
        /// </summary>
        /// <param name="uri">Der Pfad zu der Ressource</param>
        /// <param name="context">Der Suchkontext</param>
        /// <returns>Das Item oder null</returns>
        public SearchResult Find(IUri uri, SearchContext context)
        {
            if (uri == null || uri.Path.Count == 0)
            {
                return null;
            }

            var current = uri.Take(1);
            var next = uri.Skip(1);
            var match = false;
            var uriSegment = current.Path.FirstOrDefault()?.ToString();
            var pathSegment = PathSegment?.ToString();
            var variables = null as IDictionary<string, string>;

            if (string.IsNullOrWhiteSpace(pathSegment) && string.IsNullOrWhiteSpace(uriSegment))
            {
                match = true;
            }
            else if (PathSegment is PathSegmentConstant && pathSegment.Equals(uriSegment, StringComparison.OrdinalIgnoreCase))
            {
                match = true;
            }
            else if (PathSegment is PathSegmentVariable variable && Regex.IsMatch(uriSegment, pathSegment, RegexOptions.IgnoreCase))
            {
                match = true;
                variables = variable.GetVariables(uriSegment);
            }

            if (match && IncludeSubPaths)
            {
                return new SearchResult
                (
                    ID,
                    Title,
                    Type,
                    CreateInstance(context),
                    Context,
                    ResourceContext,
                    Path,
                    variables
                );
            }
            else if (match && next == null)
            {
                return new SearchResult
                (
                    ID,
                    Title,
                    Type,
                    CreateInstance(context),
                    Context,
                    ResourceContext,
                    Path,
                    variables
                );
            }
            else if (match && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    var result = child.Find(next, context);

                    if (result != null)
                    {
                        result.AddVariables(variables);
                        return result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Sucht ein Item anahnd der ID
        /// </summary>
        /// <param name="id">Die ID des gesuchten Items</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        public SitemapNode FindItem(string id, bool oneLevel = false)
        {
            var list = new List<SitemapNode>
            (
                oneLevel ? Children : GetPreOrder()
            );

            return list.Where(x => x.ID != null && x.ID.Equals(id, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
        }

        /// <summary>
        /// Erstellt eine neue Instanz oder wenn caching aktiv ist wird eine eventuell bestehende Instanz zurückgegeben
        /// </summary>
        /// <param name="context">Der Suchkontext</param>
        /// <returns>Die Instanz oder null</returns>
        public IResource CreateInstance(SearchContext context)
        {
            if (Context == null)
            {
                return null;
            }

            if (Context.Cache && Instance != null)
            {
                return Instance;
            }
            
            var instance = Type?.Assembly.CreateInstance(Type?.FullName) as IResource;

            if (instance is II18N i18n)
            {
                i18n.Culture = context.Culture;
            }

            if (instance is Resource resorce)
            {
                resorce.ID = ID;
            }

            if (instance is IPage page)
            {
                page.Title = Title;
            }

            instance.Initialization(Context);

            if (Context.Cache)
            {
                Instance = instance;
            }

            return instance;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Der Baumknoten in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return IDPath;
        }
    }
}
