using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using WebExpress.Config;
using WebExpress.Uri;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress
{
    public class Program
    {
        /// <summary>
        /// Liefert oder setzt den Namen der Webservers
        /// </summary>
        public string Name { get; set; } = "WebExpress";

        /// <summary>
        /// Der HttpServer
        /// </summary>
        private HttpServer HttpServer { get; set; }

        /// <summary>
        /// Liefert die Programmversion
        /// </summary>
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Eintrittspunkt der Anwendung
        /// </summary>
        /// <param name="args">Aufrufsargumente</param>
        public int Execution(string[] args)
        {
            var port = 80;

            // Aufrufsargumente vorbereiten 
            ArgumentParser.Current.Register(new ArgumentParserCommand() { FullName = "help", ShortName = "h" });
            ArgumentParser.Current.Register(new ArgumentParserCommand() { FullName = "config", ShortName = "c" });
            ArgumentParser.Current.Register(new ArgumentParserCommand() { FullName = "port", ShortName = "p" });

            // Aufrufsargumente parsen 
            var argumentDict = ArgumentParser.Current.Parse(args);

            if (argumentDict.ContainsKey("help"))
            {
                Console.WriteLine(Name + " [-port number | -config dateiname | -help]");
                Console.WriteLine("Version: " + Version);

                return 0;
            }
            if (argumentDict.ContainsKey("port"))
            {
                port = Convert.ToInt32(argumentDict["port"]);
            }
            if (!argumentDict.ContainsKey("config"))
            {
                // Prüfe ob eine Datei namens Config.xml vorhanden ist
                if (!File.Exists(Path.Combine(Path.Combine(Environment.CurrentDirectory, "Config"), "webexpress.config.xml")))
                {
                    Console.WriteLine("No configuration file was specified. Usage: " + Name + " -config filename");

                    return 1;
                }

                argumentDict.Add("config", "webexpress.config.xml");
            }

            // Initialisierung des WebServers
            Initialization(ArgumentParser.Current.GetValidArguments(args), port, Path.Combine(Path.Combine(Environment.CurrentDirectory, "Config"), argumentDict["config"]));

            // Start des WebServers
            Start();

            // Beenden
            Exit();

            return 0;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung mittels Ctrl+C beendet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="args">Die gültigen Argumente</param>
        /// <param name="port">Der Port</param>
        /// <param param name="configFile">Die Konfigurationsdatei</param>
        private void Initialization(string args, int port, string configFile)
        {
            // Config laden
            using var reader = new FileStream(configFile, FileMode.Open);
            var serializer = new XmlSerializer(typeof(HttpServerConfig));
            var config = serializer.Deserialize(reader) as HttpServerConfig;

            var culture = CultureInfo.CurrentCulture;

            try
            {
                culture = new CultureInfo(config.Culture);
            }
            catch
            {

            }

            var context = new HttpServerContext
            (
                !string.IsNullOrWhiteSpace(config.Uri) ? new UriAbsolute(config.Uri) : null,
                port,
                string.IsNullOrWhiteSpace(config.AssetBase) ? Environment.CurrentDirectory : config.AssetBase,
                Path.GetDirectoryName(configFile),
                new UriRelative(config.ContextPath),
                culture,
                Log.Current
            );

            HttpServer = new HttpServer(port, context)
            {
                Config = config
            };

            // Beginne mit Logging
            HttpServer.Context.Log.LogModus = (Log.Modus)Enum.Parse(typeof(Log.Modus), config.Log.Modus);
            HttpServer.Context.Log.Begin(config.Log.Path, config.Log.Filename);

            // Log Programmstart
            HttpServer.Context.Log.Seperator('/');
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.startup"));
            HttpServer.Context.Log.Info(message: "".PadRight(80, '-'));
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.version"), args: Version);
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.arguments"), args: args);
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.workingdirectory"), args: config.AssetBase);
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.configurationdirectory"), args: Path.GetDirectoryName(configFile));
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.configuration"), args: Path.GetFileName(configFile));
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.logdirectory"), args: Path.GetDirectoryName(HttpServer.Context.Log.Filename));
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.log"), args: Path.GetFileName(HttpServer.Context.Log.Filename));
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.port"), args: port);
            HttpServer.Context.Log.Seperator('=');

            Console.CancelKeyPress += OnCancel;
        }

        /// <summary>
        /// Start des WebServers
        /// </summary>
        private void Start()
        {
            HttpServer.Start();

            Thread.CurrentThread.Join();
        }

        /// <summary>
        /// Beendet die Anwendung
        /// </summary>
        private void Exit()
        {
            HttpServer.Stop();

            // Log Programmende
            HttpServer.Context.Log.Seperator('=');
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.errors"), args: HttpServer.Context.Log.ErrorCount);
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.warnings"), args: HttpServer.Context.Log.WarningCount);
            HttpServer.Context.Log.Info(message: I18N("webexpress:app.done"));
            HttpServer.Context.Log.Seperator('/');

            // Beende Logging
            HttpServer.Context.Log.Close();
        }


    }
}
