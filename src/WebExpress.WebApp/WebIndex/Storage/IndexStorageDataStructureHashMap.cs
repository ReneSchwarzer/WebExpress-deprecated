using System;
using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageDataStructureHashMap<T> : IndexStorageDataStructure
        where T : IIndexStorageDataStructureListItem
    {
        /// <summary>
        /// A hash bucket is a range of memory in a hash table that is associated with a 
        /// specific hash value. A bucket provides a concatenated list by recording the 
        /// collisions (different keys with the same hash value).
        /// </summary>
        private IndexStorageDataStructureList<T>[] Buckets;

        /// <summary>
        /// The number of fields (buckets) of the hash map. This should be a 
        /// prime number so that there are fewer collisions.
        /// </summary>
        public uint BucketCount { get; private set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint SizeOf => sizeof(uint);

        /// <summary>
        /// Returns or sets the address of the first term in a bucket in the hash map.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>The address in the bucket at the index.</returns>
        public IndexStorageDataStructureList<T> this[object term]
        {
            get
            {
                var index = (uint)term.GetHashCode() % BucketCount;
                return Buckets[index];
            }
            set { Buckets[term.GetHashCode() % BucketCount] = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        /// <param name="capacity">The number of elements to be stored in the hash map.</param>
        public IndexStorageDataStructureHashMap(IndexStorageContext context, uint capacity)
            : base(context)
        {
            BucketCount = DeterminePrimeNumber(capacity);
            Context.Allocator.Alloc(this);

            Buckets = new IndexStorageDataStructureList<T>[BucketCount];
            for (uint i = 0; i < BucketCount; i++)
            {
                Buckets[i] = new IndexStorageDataStructureList<T>(Context);
            }
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            BucketCount = reader.ReadUInt32();

            for (int i = 0; i < BucketCount; i++)
            {
                Buckets[i] = Context.IndexFile.Read(Buckets[i]);
            }
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(BucketCount);

            for (int i = 0; i < BucketCount; i++)
            {
                Context.IndexFile.Write(Buckets[i]);
            }
        }

        /// <summary>
        /// Calculates the next prime number.
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>The next prime number.</returns>
        private static uint DeterminePrimeNumber(uint capacity)
        {
            for (uint i = capacity; i <= uint.MaxValue; i++)
            {
                if (i < 2)
                {
                    return 2;
                }

                var isPrimeNumber = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrimeNumber = false;
                    }
                }

                if (isPrimeNumber)
                {
                    return i;
                }
            }

            return 65537;
        }
    }
}
