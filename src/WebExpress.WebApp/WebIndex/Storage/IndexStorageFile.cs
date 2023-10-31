using System;
using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageFile : IDisposable
    {
        /// <summary>
        /// Returns the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Returns a stream for the index file.
        /// </summary>
        private FileStream FileStream { get; set; }

        /// <summary>
        /// Returns a buffered stream to improve the read and write performance.
        /// </summary>
        private BufferedStream BufferedStream { get; set; }

        /// <summary>
        /// Returns a reader to read the stream.
        /// </summary>
        internal BinaryReader Reader { get; private set; }

        /// <summary>
        /// Returns a writer to write data to the stream.
        /// </summary>
        internal BinaryWriter Writer { get; private set; }

        /// <summary>
        /// Returns a buffer for caching segments.
        /// </summary>
        private IndexStorageRingBuffer Buffer { get; } = new IndexStorageRingBuffer(10000);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public IndexStorageFile(string fileName)
        {
            FileName = fileName;

            Directory.CreateDirectory(Path.GetDirectoryName(FileName));

            if (File.Exists(FileName))
            {
                FileStream = File.Open(FileName, FileMode.OpenOrCreate);
            }
            else
            {
                FileStream = File.Open(FileName, FileMode.CreateNew);
            }

            BufferedStream = new BufferedStream(FileStream);

            Reader = new BinaryReader(BufferedStream);
            Writer = new BinaryWriter(BufferedStream);

            Buffer.DataOverwritten += (s, e) =>
            {
                Write(e);
            };
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="addr">The segment address.</param>
        /// <param name="context">The reference to the context of the index.</param>
        /// <typeparam name="T">The type to be read.</typeparam>
        /// <returns>The segment, how it was read by the storage medium.</returns>
        public T Read<T>(ulong addr, IndexStorageContext context) where T : IIndexStorageSegment
        {
            //if (Buffer.Contains(addr))
            //{
            //    return (T)Buffer[addr];
            //}

            T segment = (T)Activator.CreateInstance(typeof(T), context);
            segment.Read(Reader, addr);

            return segment;
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public T Read<T>(T segment) where T : IIndexStorageSegment
        {
            //if (Buffer.Contains(segment.Addr))
            //{
            //    return (T)Buffer[segment.Addr];
            //}

            segment.Read(Reader, segment.Addr);

            return segment;
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public void Write(IIndexStorageSegment segment)
        {
            //if (!Buffer.ContainsKey(segment.Addr))
            //{
            //    Buffer.Add(segment.Addr, segment);
            //}

            segment.Write(Writer);
        }

        /// <summary>
        /// Ensures that all data in the buffer is written to the storage device.
        /// </summary>
        public void Flush()
        {
            //var item = default(IIndexStoragesegment);

            //while ((item = Buffer.Dequeue()) != null)
            //{
            //    Write(item);
            //}

            FileStream.Flush();
        }

        /// <summary>
        /// Is called to free up resources.
        /// </summary>
        public void Dispose()
        {
            Flush();

            Reader.Close();
            Writer.Close();
            BufferedStream.Close();
            FileStream.Close();
        }
    }
}
