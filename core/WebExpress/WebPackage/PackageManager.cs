using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebApplication;
using WebExpress.WebJob;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using WebExpress.WebSitemap;

namespace WebExpress.WebPackage
{
    /// <summary>
    /// The package manager manages packages with WebExpress extensions. The packages must be in WebExpressPackage format (*.wxp).
    /// </summary>
    public static class PackageManager
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        public static IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Thread Termination.
        /// </summary>
        private static CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Returns the catalog of installed packages.
        /// </summary>
        private static PackageCatalog Catalog { get; } = new PackageCatalog();

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.initialization"));

            // load the default plugins.
            var plugins = PluginManager.Register();

            // load internationalization
            InternationalizationManager.Register(plugins);

            // load applications
            ApplicationManager.Register(plugins);

            // load modules
            ModuleManager.Register(plugins);

            // load resources
            ResourceManager.Register(plugins);

            // load status pages
            ResponseManager.Register(plugins);

            // load jobs
            ScheduleManager.Register(plugins);

            // boot the default plugins. 
            PluginManager.Boot(plugins);
        }

        /// <summary>
        /// Starts the manager.
        /// </summary>
        internal static void Execute()
        {
            LoadCatalog();

            foreach (var package in Catalog.Packages)
            {
                var packagesFromFile = LoadPackage(Path.Combine(Context.PackagePath, package.File));

                package.Package = packagesFromFile?.Package;

                Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.existing", package.File));

                if (package.State != PackageCatalogeItemState.Disable)
                {
                    package.State = PackageCatalogeItemState.Active;
                    ExtractPackage(package);
                    RegisterPackage(package);
                    BootPackage(package);
                }
            }

            SaveCatalog();

            // build sitemap
            SitemapManager.Refresh();

            Task.Factory.StartNew(() =>
            {
                while (!TokenSource.IsCancellationRequested)
                {
                    Scan();

                    var secendsLeft = 60 - DateTime.Now.Second;
                    Thread.Sleep(secendsLeft * 1000);
                }

            }, TokenSource.Token);
        }

        /// <summary>
        /// Stop running the manager.
        /// </summary>
        public static void ShutDown()
        {
            TokenSource.Cancel();
        }

        /// <summary>
        /// Searches the package directory for new, changed or removed packages.
        /// </summary>
        public static void Scan()
        {
            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.scan", Context.PackagePath));

            // determine all WebExpress packages from the file system
            var packageFiles = Directory.GetFiles(Context.PackagePath, "*.wxp").Select(x => Path.GetFileName(x)).ToList();

            // all packages that are not yet installed
            var newPackages = packageFiles.Except(Catalog.Packages.Where(x => x != null).Select(x => x.File)).ToList();

            // all packages that are already installed
            //var existingPackages = Catalog.Packages.Select(x => x.File);

            // all packages that are no longer available
            var removePackages = Catalog.Packages.Where(x => x != null).Select(x => x.File).Except(packageFiles).ToList();

            foreach (var package in newPackages)
            {
                var packagesFromFile = LoadPackage(Path.Combine(Context.PackagePath, package));

                ExtractPackage(packagesFromFile);
                RegisterPackage(packagesFromFile);
                BootPackage(packagesFromFile);

                Catalog.Packages.Add(packagesFromFile);

                Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.add", package));
            }

            foreach (var package in removePackages)
            {
                var packagesFromFile = LoadPackage(Path.Combine(Context.PackagePath, package));

                Catalog.Packages.Add(packagesFromFile);

                Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.remove", package));
            }

            // 2. alle WebExpress-Pakete aus dem Filesystem ermitteln 


            // 2. Installiere Pakete ermitteln
            var packagesFromCatalog = Catalog.Packages;



            // 2 . DLL extrahieren
            //webExpressPackages.ForEach(x => x.Files);


            //foreach ()
            //{
            //    if (!Packages.ContainsKey(packagefile))
            //    {
            //        var package = Package.Open(packagefile);
            //        Console.WriteLine("Nuspec File: " + package.FileName);
            //        Console.WriteLine("Nuspec Id: " + package.Id);
            //        Console.WriteLine("Nuspec Version: " + package.Version);
            //        Console.WriteLine("Nuspec Autoren: " + package.Authors);
            //        Console.WriteLine("Nuspec License: " + package.License);
            //        Console.WriteLine("Nuspec LicenseUrl: " + package.LicenseUrl);
            //        Console.WriteLine("Nuspec Description: " + package.Description);
            //        Console.WriteLine("Nuspec Repository: " + package.Repository);
            //        Console.WriteLine("Nuspec Abhängigkeiten: " + string.Join(",", package.Dependencies.Select(x => x.Id)));

            //        Packages.Add(packagefile, package);
            //    }
            //}

            if (newPackages.Any() || removePackages.Any())
            {
                // build sitemap
                SitemapManager.Refresh();

                // save the catalog
                //SaveCatalog();
            }
        }

        /// <summary>
        /// Opens a package and finds the meta information.
        /// </summary>
        /// <param name="file">The path and file name.</param>
        /// <returns>The package information as a catalog entry.</returns>
        private static PackageCatalogItem LoadPackage(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    using var zip = ZipFile.Open(file, ZipArchiveMode.Read);

                    var specEntry = zip.Entries.Where(x => Path.GetExtension(x.FullName) == ".spec").FirstOrDefault();
                    var serializer = new XmlSerializer(typeof(PackageItemSpec));
                    var spec = (PackageItemSpec)serializer.Deserialize(specEntry.Open());
                    //        var files = new List<Tuple<string, byte[]>>();

                    //        foreach (ZipArchiveEntry entry in zip.Entries.Where(x => Path.GetDirectoryName(x.FullName).StartsWith("lib")))
                    //        {
                    //            Console.WriteLine("Lib: " + entry?.FullName);

                    //            using var stream = entry?.Open();
                    //            using MemoryStream ms = new MemoryStream();
                    //            stream.CopyTo(ms);
                    //            files.Add(new Tuple<string, byte[]>(entry?.FullName, ms.ToArray()));
                    //        }

                    //        foreach (ZipArchiveEntry entry in zip.Entries.Where(x => Path.GetDirectoryName(x.FullName).StartsWith("runtimes")))
                    //        {
                    //            Console.WriteLine("Runtimes: " + entry?.FullName);

                    //            using var stream = entry?.Open();
                    //            using MemoryStream ms = new MemoryStream();
                    //            stream.CopyTo(ms);
                    //            files.Add(new Tuple<string, byte[]>(entry?.FullName, ms.ToArray()));
                    //        }

                    return new PackageCatalogItem()
                    {
                        Id = spec.Id,
                        File = Path.GetFileName(file),
                        State = PackageCatalogeItemState.Available,
                        Package = new PackageItem()
                        {
                            FileName = Path.GetFileName(file),
                            Id = spec.Id,
                            Version = spec.Version,
                            Title = spec.Title,
                            Authors = spec.Authors,
                            License = spec.License,
                            LicenseUrl = spec.LicenseUrl,
                            Icon = spec.Icon,
                            Readme = spec.Readme,
                            Description = spec.Description,
                            Tags = spec.Tags,
                            //            Repository = package.Metadata.Repository?.Url,
                            //            Dependencies = package.Metadata.Dependencies != null ? package.Metadata.Dependencies?.Groups.SelectMany(x => x.Dependencies).ToList() : new List<NuPkg.Dependency>(),
                            //            Files = files
                            //
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Context.Log.Exception(ex);
            }

            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.packagenotfound", file));

            return null;
        }

        /// <summary>
        /// Load the catalog.
        /// </summary>
        private static void LoadCatalog()
        {
            var catalogeFile = Path.Combine(Context.PackagePath, "catalog.xml");
            if (File.Exists(catalogeFile))
            {
                using var catalog = new StreamReader(catalogeFile);
                var serializer = new XmlSerializer(typeof(PackageCatalog));
                var items = (PackageCatalog)serializer.Deserialize(catalog);

                Catalog.Packages.Clear();
                Catalog.Packages.AddRange(items.Packages);
            }
        }

        /// <summary>
        /// Save the catalog.
        /// </summary>
        private static void SaveCatalog()
        {
            var catalogeFile = Path.Combine(Context.PackagePath, "catalog.xml");

            using var fs = new FileStream(catalogeFile, FileMode.Create);
            using var writer = new XmlTextWriter(fs, Encoding.Unicode);
            var serializer = new XmlSerializer(typeof(PackageCatalog));

            writer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, Catalog, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") }));

            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:packagemanager.save"));
        }

        /// <summary>
        /// Extracts the specified package to the file system.
        /// </summary>
        /// <param name="package">The package.</param>
        private static void ExtractPackage(PackageCatalogItem package)
        {
            var packageFile = Path.Combine(Context.PackagePath, package?.File);

            if (File.Exists(packageFile))
            {
                using var zip = ZipFile.Open(packageFile, ZipArchiveMode.Read);

                var specEntry = zip.Entries.Where(x => Path.GetExtension(x.FullName) == ".spec").FirstOrDefault();
                var extractedPath = Path.Combine(Context.PackagePath, Path.GetFileNameWithoutExtension(package?.File));

                if (!Directory.Exists(extractedPath))
                {
                    Directory.CreateDirectory(extractedPath);
                    // Bestehendes Verzeichnis löschen
                    //Directory.Delete(extractedPath, true);
                }

                foreach (var entry in zip.Entries.Where(x => Path.GetDirectoryName(x.FullName).StartsWith(GetTFM())))
                {
                    var entryFileName = Path.Combine(extractedPath, entry?.FullName);

                    if (entryFileName.EndsWith("/"))
                    {
                        if (!Directory.Exists(entryFileName))
                        {
                            Directory.CreateDirectory(entryFileName);
                        }
                    }
                    else
                    {
                        if (!File.Exists(entryFileName))
                        {
                            entry.ExtractToFile(entryFileName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Registers the plungins included in the package.
        /// </summary>
        /// <param name="package">The package.</param>
        private static void RegisterPackage(PackageCatalogItem package)
        {
            // load plugins
            var plugins = PluginManager.Register(GetTargetPath(package));

            // load internationalization
            InternationalizationManager.Register(plugins);

            // load applications
            ApplicationManager.Register(plugins);

            // load modules
            ModuleManager.Register(plugins);

            // load resources
            ResourceManager.Register(plugins);

            // load status pages
            ResponseManager.Register(plugins);

            // load jobs
            ScheduleManager.Register(plugins);

            package.Plugins.AddRange(plugins);
        }

        /// <summary>
        /// Boots the plungins included in the package.
        /// </summary>
        /// <param name="package">The package.</param>
        private static void BootPackage(PackageCatalogItem package)
        {
            // Plugins booten
            PluginManager.Boot(package.Plugins);
        }

        /// <summary>
        /// Determines the target directory where the plug-ins of the package are located for the current target platform
        /// </summary>
        /// <param name="package">The package.</param>
        /// <returns>The directory (absolutely).</returns>
        private static string GetTargetPath(PackageCatalogItem package)
        {
            return Path.Combine(Context.PackagePath, Path.GetFileNameWithoutExtension(package?.File), GetTFM());
        }

        /// <summary>
        /// Determines the target framework.
        /// </summary>
        /// <returns>The TFM</returns>
        private static string GetTFM()
        {
            var targetFrameworkAttribute = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
                    .Select(x => x as TargetFrameworkAttribute)
                    .SingleOrDefault();

            return targetFrameworkAttribute.FrameworkDisplayName.Replace(" ", "").ToLower().Replace(".net", "net");
        }
    }
}
