using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// The Allocator is a mechanism for reserving and freeing up space. 
    /// </summary>
    public class IndexStorageDataStructureAllocator : IndexStorageDataStructure
    {
        /// <summary>
        /// Returns or sets the next free memory address.
        /// </summary>
        private ulong NextFreeAddr { get; set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint SizeOf => sizeof(ulong);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageDataStructureAllocator(IndexStorageContext context)
            : base(context)
        {
            Addr = Context.Header.SizeOf;
            NextFreeAddr = Addr + SizeOf;
        }

        /// <summary>
        /// Allocate the memory.
        /// </summary>
        /// <param name="dataStructure">The data structure determines how much memory should be reserved.</param>
        /// <returns>The start address of the reserved storage area.</returns>
        public ulong Alloc(IIndexStorageDataStructure dataStructure)
        {
            if (dataStructure.Addr != 0)
            {
                // address has already been assigned.
                return dataStructure.Addr;
            }

            var addr = NextFreeAddr;

            dataStructure.SetAddress(addr);

            NextFreeAddr += dataStructure.SizeOf;

            return addr;
        }

        /// <summary>
        /// Allocate the memory.
        /// </summary>
        /// <param name="dataStructure">The data structure determines how much memory should be reserved.</param>
        public void Free(IIndexStorageDataStructure dataStructure)
        {
            dataStructure.SetAddress(0);
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            NextFreeAddr = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(NextFreeAddr);
        }
    }
}
