using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebComponent
{
    public class ComponentCacheItem
    {
        /// <summary>
        /// Liefert die den Typ
        /// </summary>
        private Type Type { get; set; }

        /// <summary>
        /// Liefert oder setzt die Instance
        /// </summary>
        private IComponent Instance { get; set; }

        /// <summary>
        /// Liefert den Kontext
        /// </summary>
        public IComponentContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext der Komponente</param>
        /// <param name="type">Der Type</param>
        public ComponentCacheItem(IComponentContext context, Type type)
        {
            Context = context;
            Type = type;
        }

        /// <summary>
        /// Erstellt eine neue Instanz oder liefert eine gecachte Instanz zurück
        /// </summary>
        /// <param name="page">Die Seite, in der die Instanz aktiv ist</param>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung mit den Instanzen</returns>
        public IEnumerable<T> CreateInstance<T>(IPage page, Request request) where T : IControl
        {
            if (!CheckControl(request))
            {
                return new List<T>();
            }
            else if (Context.Cache && Instance != null)
            {
                return new List<T>() { (T) Instance };
            }

            if (Type.Assembly.CreateInstance(Type.FullName) is IComponent instance)
            {
                instance.Initialization(Context, page);

                if (Context.Cache)
                {
                    Instance = instance;
                }

                return new List<T>() { (T)instance };
            }
            else if (Type.Assembly.CreateInstance(Type.FullName) is IComponentDynamic dynamicInstance)
            {
                dynamicInstance.Initialization(Context, page);

                return dynamicInstance.Create<T>();
            }

            return new List<T>();
        }

        /// <summary>
        /// Prüft die Komponente, ob diese angezeigt werden oder deaktiviert sind
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>true die Komponente ist aktiv, false sonst</returns>
        private bool CheckControl(Request request)
        {
            return !Context.Conditions.Any() || Context.Conditions.All(x => x.Fulfillment(request));
        }
    }
}
