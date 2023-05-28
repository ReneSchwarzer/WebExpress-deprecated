﻿using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Class for reading the configuration file.
    /// </summary>
    [XmlRoot("config", IsNullable = false)]
    public sealed class PluginConfig
    {
        /// <summary>
        /// The configuration version.
        /// </summary>
        [XmlAttribute("version", DataType = "int")]
        public int Version { get; set; }

        /// <summary>
        /// The uri base path of the plugin.
        /// </summary>
        [XmlElement("contextpath")]
        public string ContextPath { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PluginConfig()
        {
        }
    }
}