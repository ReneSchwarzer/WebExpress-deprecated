using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Message;
using WebExpress.WebPlugin;

namespace WebExpress.Internationalization
{
    /// <summary>
    /// Internationalisierung
    /// </summary>
    public class InternationalizationManager
    {
        /// <summary>
        /// Liefert oder setzt die Standardsprache
        /// </summary>
        public static CultureInfo DefaultCulture { get; private set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Internationalisierungs-Schlüssel-Wert-Paare gelistet sind
        /// </summary>
        private static InternationalizationDictionary Dictionary { get; } = new InternationalizationDictionary();

        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;
            DefaultCulture = Context.Culture;

            Context.Log.Info(message: I18N("webexpress:internationalizationmanager.initialization"));
        }

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        public static void Register()
        {
            foreach (var plugin in PluginManager.Plugins)
            {
                Register(plugin);
            }
        }

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        public static void Register(IPluginContext plugin)
        {
            var pluginID = plugin.PluginID;
            Register(plugin.Assembly, pluginID);

            Context.Log.Info(message: I18N("webexpress:internationalizationmanager.register"), args: pluginID);
        }

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="pluginID">Das Plufin, welche die Internationalisierungsdaten zugewiesen werden</param>
        internal static void Register(Assembly assembly, string pluginID)
        {
            var assemblyName = assembly.GetName().Name.ToLower();
            var name = assemblyName + ".internationalization.";
            var resources = assembly.GetManifestResourceNames().Where(x => x.ToLower().Contains(name));

            foreach (var languageResource in resources)
            {
                var language = languageResource.Split('.').LastOrDefault()?.ToLower();

                if (!Dictionary.ContainsKey(language))
                {
                    Dictionary.Add(language, new InternationalizationItem());
                }

                var dictItem = Dictionary[language];

                using var stream = assembly.GetManifestResourceStream(languageResource);
                using var streamReader = new StreamReader(stream);
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (!line.StartsWith('#') && !string.IsNullOrWhiteSpace(line))
                    {
                        var split = line.Split('=');
                        var key = pluginID?.ToLower() + ":" + split[0]?.Trim().ToLower();

                        if (!dictItem.ContainsKey(key))
                        {
                            dictItem.Add(key, string.Join("=", split.Skip(1)));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        public static void Remove(IPlugin plugin)
        {
            //Remove(plugin.GetType().Assembly, plugin.Context.ApplicationID);
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        internal static void Remove(Assembly assembly, string application)
        {
            throw new NotImplementedException("todo");
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="obj">Das Objekt, welches erweitert wird</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(II18N obj, string key)
        {
            return I18N(obj.Culture, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(Request request, string key)
        {
            return I18N(request.Culture, null, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="culture">Die Kultur</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(CultureInfo culture, string key)
        {
            return I18N(culture, null, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="culture">Die Kultur</param>
        /// <param name="pluginID">Die PluginID</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(CultureInfo culture, string pluginID, string key)
        {
            var language = culture?.TwoLetterISOLanguageName;
            var k = string.IsNullOrWhiteSpace(pluginID) ? key : $"{pluginID}:{key}";

            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(language) || language == "*")
            {
                language = DefaultCulture.TwoLetterISOLanguageName;
            }

            var item = Dictionary[language];

            if (item.ContainsKey(k))
            {
                return item[k];
            }

            return key;
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(string key)
        {
            return I18N(DefaultCulture, null, key);
        }
    }
}
