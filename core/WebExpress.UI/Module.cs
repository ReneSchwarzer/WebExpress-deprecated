﻿using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace WebExpress.UI
{
    [ID("webexpress.ui")]
    [Application("*")]
    [Name("module.name")]
    [Description("module.description")]
    [Icon("/assets/img/Logo.png")]
    [AssetPath("/")]
    [ContextPath("/wxui")]
    public sealed class Module : IModule
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialisierung des Moduls. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IModuleContext context)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Modul mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
