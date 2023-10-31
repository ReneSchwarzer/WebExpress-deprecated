using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public interface IIndexStorageSegment
    {
        /// <summary>
        /// Returns the address of the segment.
        /// </summary>
        public ulong Addr { get; }

        /// <summary>
        /// Returns the the context of the index.
        /// </summary>
        public IndexStorageContext Context { get; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public uint Size { get; }

        /// <summary>
        /// Assigns an address to the segment.
        /// </summary>
        /// <<param name="addr">The address of the segment.</param>
        public void OnAllocated(ulong addr);

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        /// <param name="addr">The address of the segment.</param>
        public void Read(BinaryReader reader, ulong addr);

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public void Write(BinaryWriter writer);
    }
}
