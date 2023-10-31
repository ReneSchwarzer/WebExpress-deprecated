using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public abstract class IndexStorageSegment : IIndexStorageSegment
    {
        /// <summary>
        /// Returns the address of the segment.
        /// </summary>
        public virtual ulong Addr { get; protected set; }

        /// <summary>
        /// Returns the the context of the index.
        /// </summary>
        public IndexStorageContext Context { get; private set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public abstract uint Size { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageSegment(IndexStorageContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Assigns an address to the segment.
        /// </summary>
        /// <param name="addr">The address of the segment.</param>
        public virtual void OnAllocated(ulong addr)
        {
            Addr = addr;
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        /// <param name="addr">The address of the segment.</param>
        public abstract void Read(BinaryReader reader, ulong addr);

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public abstract void Write(BinaryWriter writer);
    }
}
