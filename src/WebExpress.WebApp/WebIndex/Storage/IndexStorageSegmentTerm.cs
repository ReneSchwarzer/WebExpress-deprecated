using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageSegmentTerm : IndexStorageSegment, IIndexStorageSegmentListItem
    {
        /// <summary>
        /// Returns or sets the address of the following term.
        /// </summary>
        public ulong SuccessorAddr { get; set; }

        /// <summary>
        /// Returns or sets the address of the first posting.
        /// </summary>
        public ulong PostingAddr { get; set; }

        /// <summary>
        /// Returns the number of characters in the term.
        /// </summary>
        public uint Length => (uint)Term.Length;

        /// <summary>
        /// Returns or sets the number of times the term is used (postings).
        /// </summary>
        public uint Fequency { get; set; }

        /// <summary>
        /// Returns or sets the term.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Returns the postings.
        /// </summary>
        public IndexStorageSegmentHashMap<IndexStorageSegmentPosting> Postings { get; private set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint Size => sizeof(ulong) + sizeof(ulong) + sizeof(uint) + sizeof(uint) + Length + Postings.Size;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageSegmentTerm(IndexStorageContext context)
            : base(context)
        {
            Postings = new IndexStorageSegmentHashMap<IndexStorageSegmentPosting>(Context, 10);
        }

        /// <summary>
        /// Assigns an address to the segment.
        /// </summary>
        /// <param name="addr">The address of the segment.</param>
        public override void OnAllocated(ulong addr)
        {
            base.OnAllocated(addr);

            Postings.OnAllocated(addr + Size - Postings.Size);
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
            PostingAddr = reader.ReadUInt64();
            Fequency = reader.ReadUInt32();
            var length = reader.ReadUInt32();
            Term = new string(reader.ReadChars((int)length));

            Postings.Read(reader, addr + Size - Postings.Size);
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(SuccessorAddr);
            writer.Write(PostingAddr);
            writer.Write(Fequency);

            writer.Write(Length);
            writer.Write(Term.ToCharArray(0, (int)Length));

            Postings.Write(writer);
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
            if (obj is IndexStorageSegmentTerm term)
            {
                return Term.CompareTo(term.Term);
            }

            throw new System.ArgumentException();
        }
    }
}
