﻿using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebResource
{
    public interface IResourceContext
    {
        /// <summary>
        /// Das Assembly, welches das Modul enthällt
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Liefert das zugehörige Plugins
        /// </summary>
        IPluginContext Plugin { get; }

        /// <summary>
        /// Liefert den Kontext der zugehörigen Anwendung 
        /// </summary>
        IApplicationContext Application { get; }

        /// <summary>
        /// Liefert das zugehörige Modul. 
        /// </summary>
        IModuleContext Module { get; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        IUri ContextPath { get; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        IReadOnlyList<string> Context { get; }

        /// <summary>
        /// Liefert die Bedingungen, die erfüllt sein müssen, damit die Ressource aktiv ist
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Bestimmt, ob die Ressource einmalig erstellt und bei jedem Aufruf wiederverwendet wird.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
