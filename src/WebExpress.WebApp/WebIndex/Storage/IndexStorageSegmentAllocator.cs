using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// The Allocator is a mechanism for reserving and freeing up space. 
    /// </summary>
    public class IndexStorageSegmentAllocator : IndexStorageSegment
    {
        /// <summary>
        /// Returns or sets the next free memory address.
        /// </summary>
        private ulong NextFreeAddr { get; set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint Size => sizeof(ulong);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageSegmentAllocator(IndexStorageContext context)
            : base(context)
        {
            Addr = IndexStorageSegmentHeader.SegmentSize;
            NextFreeAddr = Addr + Size;
        }

        /// <summary>
        /// Allocate the memory.
        /// </summary>
        /// <param name="segment">The segment determines how much memory should be reserved.</param>
        /// <returns>The start address of the reserved storage area.</returns>
        public ulong Alloc(IIndexStorageSegment segment)
        {
            if (segment.Addr != 0)
            {
                // address has already been assigned.
                return segment.Addr;
            }

            var addr = NextFreeAddr;

            segment.OnAllocated(addr);

            NextFreeAddr += segment.Size;

            return addr;
        }

        /// <summary>
        /// Allocate the memory.
        /// </summary>
        /// <param name="segment">The segment determines how much memory should be reserved.</param>
        public void Free(IIndexStorageSegment segment)
        {
            segment.OnAllocated(0);
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        /// <param name="addr">The address of the segment.</param>
        public override void Read(BinaryReader reader, ulong addr)
        {
            Addr = addr;
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
