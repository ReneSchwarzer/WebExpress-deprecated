using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Attribut zur Kennzeichnun einer Klasse als Plugin-Komponente
    /// </summary>
    public class PluginComponentAttribute : Attribute, IPluginComponentAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="section">Die Sektion, indem die Komponente eingebettet wird</param>
        public PluginComponentAttribute(string section)
        {
        }

    }
}
