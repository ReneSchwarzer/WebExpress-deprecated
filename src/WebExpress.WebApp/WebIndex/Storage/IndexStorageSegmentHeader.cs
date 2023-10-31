using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageSegmentHeader : IndexStorageSegment
    {
        /// <summary>
        /// Returns or sets the file identifire.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint Size => SegmentSize;

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public static uint SegmentSize => 3;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageSegmentHeader(IndexStorageContext context)
            : base(context)
        {
            Addr = 0;
            Identifier = "wri";
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

            Identifier = new string(reader.ReadChars(3));
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(Identifier.ToCharArray(0, 3));
        }
    }
}
