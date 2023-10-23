using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public abstract class IndexStorageDataStructure : IIndexStorageDataStructure
    {
        /// <summary>
        /// Returns the address of the data structure.
        /// </summary>
        public virtual ulong Addr { get; protected set; }

        /// <summary>
        /// Returns the the context of the index.
        /// </summary>
        public IndexStorageContext Context { get; private set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public abstract uint SizeOf { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageDataStructure(IndexStorageContext context)
        {
            Context = context;
        }

        ///// <summary>
        ///// Initialization
        ///// </summary>
        ///// <param name="index">The underlying index.</param>
        ///// <param name="addr">The address of the data structure.</param>
        //public virtual void Initialization(IIndexStorage index, ulong addr = 0UL)
        //{
        //    Index = index;
        //}

        ///// <summary>
        ///// Assigns an index to the data structure.
        ///// </summary>
        ///// <<param name="index">The underlying index.</param>
        //public void SetIndex(IIndexStorage index)
        //{
        //    Index = index;
        //}

        /// <summary>
        /// Assigns an address to the data structure.
        /// </summary>
        /// <param name="addr">The address of the data structure.</param>
        public void SetAddress(ulong addr)
        {
            Addr = addr;
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public abstract void Read(BinaryReader reader);

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public abstract void Write(BinaryWriter writer);
    }
}
