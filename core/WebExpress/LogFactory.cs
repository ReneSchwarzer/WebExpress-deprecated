using Microsoft.Extensions.Logging;

namespace WebExpress
{
    public class LogFactory : ILoggerFactory
    {
        /// <summary>
        /// Fügt dem Protokollierungssystem einen ILoggerProvider hinzu.
        /// </summary>
        /// <param name="provider">Der ILoggerProvider.</param>
        public void AddProvider(ILoggerProvider provider)
        {

        }

        /// <summary>
        /// Erstellt eine neue ILogger-Instanz.
        /// </summary>
        /// <param name="categoryName">Der Kategoriename für Nachrichten, die von der Protokollierung generiert werden.</param>
        /// <returns>Eine neue ILogger-Instanz.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            // Bestehende Logging-Instanz verwenden
            return Log.Current;
        }

        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen 
        /// von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
