using System;
using System.Linq;
using WebExpress.Message;
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
        /// Konstruktor
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
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Instanz oder null</returns>
        public IComponent CreateInstance(IPage page, Request request)
        {
            if (!CheckControl(request))
            {
                return null;
            }
            else if (Context.Cache && Instance != null)
            {
                return Instance;
            }

            if (Type.Assembly.CreateInstance(Type.FullName) is IComponent instance)
            {
                instance.Initialization(Context, page);

                if (Context.Cache)
                {
                    Instance = instance;
                }

                return instance;
            }

            return null;
        }

        /// <summary>
        /// Prüft die Komponente, ob diese angezeigt werden oder deaktiviert sind
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>true die Komponente ist aktiv, false sonst</returns>
        private bool CheckControl(Request request)
        {
            return !Context.Conditions.Any() || Context.Conditions.All(x => x.Fulfillment(request));
        }
    }
}
