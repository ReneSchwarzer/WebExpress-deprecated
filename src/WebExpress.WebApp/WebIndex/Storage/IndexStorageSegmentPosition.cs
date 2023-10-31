using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageSegmentPosition : IndexStorageSegment, IIndexStorageSegmentListItem
    {
        /// <summary>
        /// Returns or sets the address of the following position.
        /// </summary>
        public ulong SuccessorAddr { get; set; }

        /// <summary>
        /// Returns or sets the position.
        /// </summary>
        public uint Position { get; set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint Size => sizeof(ulong) + sizeof(int);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageSegmentPosition(IndexStorageContext context)
            : base(context)
        {
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

            SuccessorAddr = reader.ReadUInt64();
            Position = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(SuccessorAddr);
            writer.Write(Position);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        ///  an integer that indicates whether the current instance precedes, follows, or
        ///  occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of x and y.
        ///     Less than zero -> x is less than y.
        ///     Zero -> x equals y.
        ///     Greater than zero -> x is greater than y.
        /// </returns>
        /// <exception cref="System.ArgumentException">Obj is not the same type as this instance.</exception>
        public int CompareTo(object obj)
        {
            if (obj is IndexStorageSegmentPosition position)
            {
                return Position.CompareTo(position.Position);
            }

            throw new System.ArgumentException();
        }
    }
}
