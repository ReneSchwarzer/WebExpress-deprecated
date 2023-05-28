using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace WebExpress.WebPackage
{
    /// <summary>
    /// Class for generating webexpress packages from a build environment.
    /// </summary>
    public static class PackageBuilder
    {
        /// <summary>
        /// Creates a webex package.
        /// </summary>
        /// <param name="specFile">The spec file (*.spec).</param>
        /// <param name="outputDirectory">The directory.</param>
        public static void Create(string specFile, string outputDirectory)
        {
            Console.WriteLine($"*** PackageBuilder: specFile '{specFile}'.");
            Console.WriteLine($"*** PackageBuilder: outputDirectory '{outputDirectory}'.");

            using var fileStream = File.OpenRead(specFile);
            var serializer = new XmlSerializer(typeof(PackageItemSpec));
            var package = (PackageItemSpec)serializer.Deserialize(fileStream);

            Console.WriteLine($"*** PackageBuilder: Creates a webex package '{package.Id}' in directory '{outputDirectory}'.");

            using var zipFileStream = new FileStream(Path.Combine(outputDirectory, $"{package.Id}.{package.Version}.wxp"), FileMode.Create);
            using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, true);

            // find readme
            if (!string.IsNullOrWhiteSpace(package.Readme))
            {
                var path = Path.GetDirectoryName(specFile);

                do
                {
                    if (File.Exists(Path.Combine(path, package.Readme)))
                    {
                        ReadmeToZip
                        (
                            archive,
                            File.ReadAllBytes(Path.Combine(path, package.Readme))
                        );

                        break;
                    }

                    path = Directory.GetParent(path)?.FullName;

                }
                while (Directory.Exists(path));
            }

            // find icon
            if (!string.IsNullOrWhiteSpace(package.Icon))
            {
                var path = Path.GetDirectoryName(specFile);

                do
                {
                    if (File.Exists(Path.Combine(path, package.Icon)))
                    {
                        IconToZip
                        (
                            archive,
                            Path.GetFileName(package.Icon),
                            File.ReadAllBytes(Path.Combine(path, package.Icon))
                        );

                        break;
                    }

                    path = Directory.GetParent(path)?.FullName;

                }
                while (path != null && Directory.Exists(path));
            }

            // find licenses 
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(specFile), "*.lic", SearchOption.AllDirectories))
            {
                if (File.Exists(file))
                {
                    LicensesToZip
                    (
                        archive,
                        Path.GetFileName(file),
                        File.ReadAllBytes(file)
                    );
                }
            }

            SpecToZip(archive, package);
            ArtifactsToZip(archive, package, Path.GetDirectoryName(specFile));
            DependenciesToZip(archive, package, Path.GetDirectoryName(specFile));
        }

        /// <summary>
        /// Create the readme file.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="readme">The readme content.</param>
        private static void ReadmeToZip(ZipArchive archive, byte[] readme)
        {
            var zipArchiveEntry = archive.CreateEntry("readme.md", CompressionLevel.Fastest);
            using var zipStream = zipArchiveEntry.Open();
            zipStream.Write(readme, 0, readme.Length);

            Console.WriteLine($"*** PackageBuilder: Create the readme file.");
        }

        /// <summary>
        /// Create the icon file.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="fileName">The icon file name.</param>
        /// <param name="icon">The icon content.</param>
        private static void IconToZip(ZipArchive archive, string fileName, byte[] icon)
        {
            var zipArchiveEntry = archive.CreateEntry($"assets/{fileName}", CompressionLevel.Fastest);
            using var zipStream = zipArchiveEntry.Open();
            zipStream.Write(icon, 0, icon.Length);

            Console.WriteLine($"*** PackageBuilder: Create the icon file.");
        }

        /// <summary>
        /// Create the licenses file.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="fileName">The licenses file name.</param>
        /// <param name="icon">The licenses content.</param>
        private static void LicensesToZip(ZipArchive archive, string fileName, byte[] icon)
        {
            var zipArchiveEntry = archive.CreateEntry($"licenses/{Path.GetFileNameWithoutExtension(fileName)}.txt", CompressionLevel.Fastest);
            using var zipStream = zipArchiveEntry.Open();
            zipStream.Write(icon, 0, icon.Length);

            Console.WriteLine($"*** PackageBuilder: Create the licenses file.");
        }

        /// <summary>
        /// Create the spec file.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="package">The package.</param>
        private static void SpecToZip(ZipArchive archive, PackageItemSpec package)
        {
            var zipArchiveEntry = archive.CreateEntry($"{package.Id}.spec", CompressionLevel.Fastest);
            var serializer = new XmlSerializer(typeof(PackageItemSpec));
            using var zipStream = zipArchiveEntry.Open();

            package.Icon = $"assets/{Path.GetFileName(package.Icon)}";
            package.Readme = $"readme.md";

            serializer.Serialize(zipStream, package);

            Console.WriteLine($"*** PackageBuilder: Create the spec file.");
        }

        /// <summary>
        /// Create the artifact files.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="package">The package.</param>
        /// <param name="path">The root path.</param>
        private static void ArtifactsToZip(ZipArchive archive, PackageItemSpec package, string path)
        {
            foreach (var item in package.Artifacts)
            {
                var fileName = Find(path, item);

                if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                {
                    var fileData = File.ReadAllBytes(fileName);
                    var zipArchiveEntry = archive.CreateEntry($"lib/{item}", CompressionLevel.Fastest);
                    using var zipStream = zipArchiveEntry.Open();
                    zipStream.Write(fileData, 0, fileData.Length);

                    Console.WriteLine($"*** PackageBuilder: Create the artifact file '{fileName}'.");
                }
            }
        }

        /// <summary>
        /// Create the spec file.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="package">The package.</param>
        /// <param name="path">The root path.</param>
        private static void DependenciesToZip(ZipArchive archive, PackageItemSpec package, string path)
        {
            foreach (var item in package.Dependencies?.Libs)
            {
                var fileName = Find(path, item);

                if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                {
                    var fileData = File.ReadAllBytes(fileName);
                    var zipArchiveEntry = archive.CreateEntry($"lib/{item}", CompressionLevel.Fastest);
                    using var zipStream = zipArchiveEntry.Open();
                    zipStream.Write(fileData, 0, fileData.Length);

                    Console.WriteLine($"*** PackageBuilder: Create the lib file '{fileName}'.");
                }
            }

            foreach (var item in package.Dependencies?.Runtimes)
            {
                var fileName = Find(path, item);

                if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                {
                    var fileData = File.ReadAllBytes(fileName);
                    var zipArchiveEntry = archive.CreateEntry($"lib/runtimes/{item}", CompressionLevel.Fastest);
                    using var zipStream = zipArchiveEntry.Open();
                    zipStream.Write(fileData, 0, fileData.Length);

                    Console.WriteLine($"*** PackageBuilder: Create the runtime file '{fileName}'.");
                }
            }
        }

        /// <summary>
        /// Find a file
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>The file name, if found or null.</returns>
        private static string Find(string path, string fileName)
        {
            try
            {
                do
                {
                    foreach (var f in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                        .Where(x => x.Replace('\\', '/').EndsWith(fileName.Replace('\\', '/'), StringComparison.OrdinalIgnoreCase)))
                    {
                        return f;
                    }

                    path = Directory.GetParent(path)?.FullName;

                }
                while (path != null && Directory.Exists(path));
            }
            catch
            {
            }

            return null;
        }
    }
}
