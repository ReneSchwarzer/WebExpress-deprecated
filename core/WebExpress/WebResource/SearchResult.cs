using System;
using System.Collections.Generic;
using WebExpress.Internationalization;

namespace WebExpress.WebResource
{
    public class SearchResult
    {
        /// <summary>
        /// Liefert die SeitenID
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Liefert der Ressourcentitel
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Liefert den Resourcenyyp
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Liefert die Instanz
        /// </summary>
        public IResource Instance { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IResourceContext Context { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Kontext indem die Ressource existiert
        /// </summary>
        public IReadOnlyList<string> ResourceContext { get; internal set; }

        /// <summary>
        /// Liefert den Pfad
        /// </summary>
        /// <returns>Der Pfad</returns>
        public ICollection<SitemapNode> Path { get; internal set; }

        /// <summary>
        /// Liefert die Variablen
        /// </summary>
        public IDictionary<string, string> Variables { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die SeitenID</param>
        /// <param name="title">Der Ressourcentitel</param>
        /// <param name="type">Der Ressorcen-Typ</param>
        /// <param name="instance">Die Instanz</param>
        /// <param name="context">Den Kontext</param>
        /// <param name="resourceContext">Den Kontext indem die Ressource existiert</param>
        /// <param name="path">Der Pfad</param>
        /// <param name="variables">Variablen-Wert-Paare</param>
        public SearchResult
        (
            string id,
            string title,
            Type type,
            IResource instance,
            IResourceContext context,
            IReadOnlyList<string> resourceContext,
            ICollection<SitemapNode> path,
            IDictionary<string, string> variables
        )
        {
            ID = id;
            Title = title;
            Type = type;
            Instance = instance;
            Context = context;
            ResourceContext = resourceContext;
            Path = path;

            AddVariables(variables);
        }

        /// <summary>
        /// Fügt die Variablen-Wert-Paare dem Ergebnis hinzu
        /// </summary>
        /// <param name="variables">Die Variablen-Wert-Paare</param>
        public void AddVariables(IDictionary<string, string> variables)
        {
            if (variables != null)
            {
                foreach (var v in variables)
                {
                    if (!Variables.ContainsKey(v.Key))
                    {
                        Variables.Add(v.Key, v.Value);
                    }
                    else
                    {
                        Context.Log.Warning(message: InternationalizationManager.I18N(InternationalizationManager.DefaultCulture, "webexpress", "resource.variable.duplicate"), args: v.Key);
                    }
                }
            }
        }
    }
}
