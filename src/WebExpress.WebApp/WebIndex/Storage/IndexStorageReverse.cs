using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// Implementation of the web reverse index, which stores the key-value pairs on disk.
    /// </summary>
    /// <typeparam name="T">The data type. This must have the IIndexItem interface.</typeparam>
    public class IndexStorageReverse<T> : IIndexReverse<T>, IIndexStorage where T : IIndexItem
    {
        /// <summary>
        /// The property that makes up the index.
        /// </summary>
        private PropertyInfo Property { get; set; }

        /// <summary>
        /// Returns the file name for the reverse index.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Returns or sets the reverse index file.
        /// </summary>
        public IndexStorageFile IndexFile { get; private set; }

        /// <summary>
        /// Returns or sets the header.
        /// </summary>
        public IndexStorageSegmentHeader Header { get; private set; }

        /// <summary>
        /// Returns or sets the hash map.
        /// </summary>
        public IndexStorageSegmentHashMap<IndexStorageSegmentTerm> HashMap { get; private set; }

        /// <summary>
        /// Returns or sets the memory manager.
        /// </summary>
        public IndexStorageSegmentAllocator Allocator { get; private set; }

        /// <summary>
        /// Returns the statistical values that can be help to optimize the index.
        /// </summary>
        public IndexStorageSegmentStatistic Statistic { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">The property that makes up the index.</param>
        /// <param name="capacity">The predicted capacity (number of items to store) of the reverse index.</param>
        public IndexStorageReverse(PropertyInfo property, uint capacity)
        {
            Property = property;
            FileName = Path.Combine(Environment.CurrentDirectory, "index", $"{typeof(T).Name}.{property.Name}.wri");

            // todo delelte this bevor commit
            File.Delete(FileName);

            var exists = File.Exists(FileName);
            IndexFile = new IndexStorageFile(FileName);
            Header = new IndexStorageSegmentHeader(new IndexStorageContext(this));
            Allocator = new IndexStorageSegmentAllocator(new IndexStorageContext(this));
            Statistic = new IndexStorageSegmentStatistic(new IndexStorageContext(this));
            HashMap = new IndexStorageSegmentHashMap<IndexStorageSegmentTerm>(new IndexStorageContext(this), capacity);

            Allocator.Alloc(Statistic);
            Allocator.Alloc(HashMap);

            if (exists)
            {
                Header = IndexFile.Read(Header);
                Allocator = IndexFile.Read(Allocator);
                Statistic = IndexFile.Read(Statistic);
                HashMap = IndexFile.Read(HashMap);
            }
            else
            {
                IndexFile.Write(Header);
                IndexFile.Write(Allocator);
                IndexFile.Write(Statistic);
                IndexFile.Write(HashMap);
            }

            IndexFile.Flush();
        }

        /// <summary>
        /// Adds a item to the field.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexItem interface.</typeparam>
        /// <param name="item">The data to be added to the index.</param>
        public void Add(T item)
        {
            var value = Property?.GetValue(item)?.ToString();
            var tokens = IndexTermTokenizer.Tokenize(value);
            var terms = IndexTermNormalizer.Normalize(tokens);

            foreach (var term in terms)
            {
                HashMap[term.Value]
                    .Add(new IndexStorageSegmentTerm(new IndexStorageContext(this)) { Term = term.Value, Fequency = 1 })
                    .Postings[item.Id]
                    .Add(new IndexStorageSegmentPosting(new IndexStorageContext(this)) { DocumentID = item.Id })
                    .Positions
                    .Add(new IndexStorageSegmentPosition(new IndexStorageContext(this)) { Position = term.Position });
            }
        }

        /// <summary>
        /// The data to be removed from the field.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexData interface.</typeparam>
        /// <param name="item">The data to be removed from the field.</param>
        public void Remove(T item)
        {

        }

        /// <summary>
        /// Return all items for a given term.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>An enumeration of the data ids.</returns>
        public IEnumerable<int> Collect(object term)
        {
            return Enumerable.Empty<int>();
        }
    }
}
