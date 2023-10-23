using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public interface IIndexStorageDataStructure
    {
        /// <summary>
        /// Returns the address of the data structure.
        /// </summary>
        public ulong Addr { get; }

        /// <summary>
        /// Returns the the context of the index.
        /// </summary>
        public IndexStorageContext Context { get; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public uint SizeOf { get; }

        ///// <summary>
        ///// Initialization
        ///// </summary>
        ///// <param name="index">The underlying index.</param>
        ///// <param name="addr">The address of the data structure.</param>
        //public void Initialization(IIndexStorage index, ulong addr = 0UL);

        ///// <summary>
        ///// Assigns an index to the data structure.
        ///// </summary>
        ///// <<param name="index">The underlying index.</param>
        //public void SetIndex(IIndexStorage index);

        /// <summary>
        /// Assigns an address to the data structure.
        /// </summary>
        /// <<param name="addr">The address of the data structure.</param>
        public void SetAddress(ulong addr);

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public void Read(BinaryReader reader);

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public void Write(BinaryWriter writer);
    }
}
