using System;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.Module;

namespace WebExpress.WebResource
{
    public class SearchResult
    {
        /// <summary>
        /// Liefert der Ressourcentitel
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Liefert den Resourcenyyp
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext
        /// </summary>
        public IModuleContext ModuleContext { get; internal set; }

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
        /// <param name="title">Der Ressourcentitel</param>
        /// <param name="type">Der Ressorcen-Typ</param>
        /// <param name="moduleContext">Den Modulkontext</param>
        /// <param name="resourceContext">Den Kontext indem die Ressource existiert</param>
        /// <param name="path">Der Pfad</param>
        /// <param name="variables">Variablen-Wert-Paare</param>
        public SearchResult
        (
            string title,
            Type type,
            IModuleContext moduleContext,
            IReadOnlyList<string> resourceContext,
            ICollection<SitemapNode> path,
            IDictionary<string, string> variables
        )
        {
            Title = title;
            Type = type;
            ModuleContext = moduleContext;
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
                        ModuleContext.Log.Warning(message: InternationalizationManager.I18N(InternationalizationManager.DefaultCulture, "webexpress", "resource.variable.duplicate"), args: v.Key);
                    }
                }
            }
        }
    }
}
