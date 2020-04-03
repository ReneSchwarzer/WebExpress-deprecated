using System;
using System.IO;
using System.Reflection;
using System.Threading;
using WebExpress;
using WebExpress.Config;

namespace WebExpress.App
{
    public class Program
    { 
		/// <summary>
		/// Der HttpServer
		/// </summary>
        private static HttpServer HttpServer { get; set; }

        /// <summary>
        /// Eintrittspunkt der Anwendung
        /// </summary>
        /// <param name="args">Aufrufsargumente</param>
        public static int Main(string[] args)
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
                Console.WriteLine("WebExpress [-port number | -config dateiname | -help]");
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
                if (!File.Exists(Path.Combine(Path.Combine(Environment.CurrentDirectory, "Config"), "config.xml")))
                {
                    Console.WriteLine("Es wurde keine Konfigurationsdatei angegeben. Verwendung: WebExpress -config dateiname");

                    return 1;
                }

                argumentDict.Add("config", "config.xml");
            }

            // Initialisierung des WebServers
            Init(ArgumentParser.Current.GetValidArguments(args), port, Path.Combine(Path.Combine(Environment.CurrentDirectory, "Config"), argumentDict["config"]));

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
        private static void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="args">Die gültigen Argumente</param>
        /// <param name="port">Der Port</param>
        /// <param param name="configFile">Die Konfigurationsdatei</param>
        private static void Init(string args, int port, string configFile)
        {
            // Config laden
            var config = new HttpServerConfig(configFile);

            var context = new HttpServerContext(config.AssetBase, Path.GetDirectoryName(configFile), Log.Current);
            HttpServer = new HttpServer(port, context)
            {
                Config = config
            };

            // Beginne mit Logging
            HttpServer.Context.Log.LogModus = (Log.Modus)Enum.Parse(typeof(Log.Modus), config.Log.Modus);
            HttpServer.Context.Log.Begin(config.Log.Path, config.Log.Filename);

            // Log Programmstart
            HttpServer.Context.Log.Seperator('/');
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Programmstart");
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "".PadRight(80, '-'));
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Programmversion: " + Version);
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Argumente: " + args);
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Arbeitsverzeichnis: " + config.AssetBase);
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Konfigurationsverzeichnis: " + Path.GetDirectoryName(configFile));
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Konfiguration: " + Path.GetFileName(configFile));
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Logverzeichnis: " + Path.GetDirectoryName(HttpServer.Context.Log.Filename));
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Log: " + Path.GetFileName(HttpServer.Context.Log.Filename));
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), "Port: " + port);
            HttpServer.Context.Log.Seperator('=');

            Console.CancelKeyPress += OnCancel;
        }

        /// <summary>
        /// Start des WebServers
        /// </summary>
        private static void Start()
        {
            HttpServer.Start();

            Thread.CurrentThread.Join();
        }

        /// <summary>
        /// Beendet die Anwendung
        /// </summary>
        private static void Exit()
        {
            HttpServer.Stop();

            // Log Programmende
            HttpServer.Context.Log.Seperator('=');
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), HttpServer.Context.Log.ErrorCount.ToString() + " Fehler");
            HttpServer.Context.Log.Info(MethodBase.GetCurrentMethod(), HttpServer.Context.Log.WarningCount.ToString() + " Warnung(en)");
            HttpServer.Context.Log.Info(MethodInfo.GetCurrentMethod(), "Programmende");
            HttpServer.Context.Log.Seperator('/');

            // Beende Logging
            HttpServer.Context.Log.Close();
        }

        /// <summary>
        /// Liefert die Programmversion
        /// </summary>
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
