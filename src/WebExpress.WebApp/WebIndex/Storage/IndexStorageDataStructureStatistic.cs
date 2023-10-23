using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// Records statistical values that can be help to optimize the index.
    /// </summary>
    public class IndexStorageDataStructureStatistic : IndexStorageDataStructure
    {
        /// <summary>
        /// Returns or sets the maximum occurrence of collisions that occur 
        /// when terms are stored in the hashmap.
        /// </summary>
        public uint MaxTermCollisions { get; set; }

        /// <summary>
        /// Returns or sets the maximum occurrence of collisions that occur 
        /// when terms are stored in the hashmap.
        /// </summary>
        public uint MaxPostingCollisions { get; set; }

        /// <summary>
        /// Returns or sets the term that occurs most frequently.
        /// </summary>
        public object MaxTerm { get; set; }

        /// <summary>
        /// Returns or sets the maxima number of times a term exists.
        /// </summary>
        public uint MaxTermFrequency { get; set; }

        /// <summary>
        /// Returns the number of terms stored.
        /// </summary>
        public uint TermCount { get; internal set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint SizeOf => sizeof(ulong);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageDataStructureStatistic(IndexStorageContext context)
            : base(context)
        {
            Context.Allocator.Alloc(this);
        }

        /// <summary>
        /// Invoked when a new collision is detected.
        /// </summary>
        /// <param name="item">The data structure.</param>
        /// <param name="collisions">The number of collisions.</param>
        internal void AddCollision<T>(T item, uint collisions) where T : IIndexStorageDataStructureListItem
        {
            if (item is IndexStorageDataStructureTerm term)
            {
                if (MaxTermCollisions < collisions)
                {
                    MaxTermCollisions = collisions;

                    Context.IndexFile.Write(this);
                }
            }
            else if (item is IndexStorageDataStructurePosting posting)
            {
                if (MaxPostingCollisions < collisions)
                {
                    MaxPostingCollisions = collisions;

                    Context.IndexFile.Write(this);
                }
            }
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            TermCount = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(TermCount);
        }
    }
}
