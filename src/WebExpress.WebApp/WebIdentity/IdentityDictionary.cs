using System.Collections.Generic;
using WebExpress.WebModule;

namespace WebExpress.WebApp.WebIdentity
{
    /// <summary>
    /// 
    /// </summary>
    internal class IdentityDictionary 
    {
        /// <summary>
        /// Das Verzeichnis der Identitäten.
        /// Modul -> Id -> Identität
        /// </summary>
        public static Dictionary<IModuleContext, Dictionary<string, IdentityItem>> Identities { get; } = new Dictionary<IModuleContext, Dictionary<string, IdentityItem>>();

        /// <summary>
        /// Das Verzeichnis der Rollen.
        /// Modul -> Id -> Rolle
        /// </summary>
        public static Dictionary<IModuleContext, Dictionary<string, IdentityRoleItem>> Roles { get; } = new Dictionary<IModuleContext, Dictionary<string, IdentityRoleItem>>();

        /// <summary>
        /// Das Verzeichnis der Ressourcen.
        /// Modul -> Id -> Ressource
        /// </summary>
        public static Dictionary<IModuleContext, Dictionary<string, IdentityResourceItem>> Resources { get; } = new Dictionary<IModuleContext, Dictionary<string, IdentityResourceItem>>();
    }
}
