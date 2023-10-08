using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex.Memory;

namespace WebExpress.WebApp.WebIndex
{
    /// <summary>
    /// The IndexDocument is a data structure that provides indexes (for each property of a data type).
    /// Key: The field name.
    /// Value: The reverse index.
    /// </summary>
    public class IndexDocument<T> : Dictionary<string, IIndexReverse<T>>, IIndexDocument<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the forward index.
        /// </summary>
        public IIndexForward<T> ForwardIndex { get; private set; }

        /// <summary>
        /// Returns the index type.
        /// </summary>
        public IndexType IndexType { get; private set; }

        /// <summary>
        /// Return the index field names.
        /// </summary>
        public IEnumerable<string> Fields => Keys;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="indexType">The index type.</param>
        public IndexDocument(IndexType indexType)
        {
            IndexType = indexType;

            switch (IndexType)
            {
                case IndexType.Memory:
                default:
                    {
                        ForwardIndex = new IndexMemoryForward<T>();

                        break;
                    }
            }

            foreach (var property in typeof(T).GetProperties())
            {
                var fieldName = property.Name;

                Add(fieldName);
            }
        }

        /// <summary>
        /// Adds a field name to the index.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexData interface.</typeparam>
        /// <param name="fieldName">The field name.</param>
        public void Add(string fieldName)
        {
            switch (IndexType)
            {
                case IndexType.Memory:
                default:
                    {
                        if (!ContainsKey(fieldName))
                        {
                            Add(fieldName, new IndexMemoryReverse<T>(fieldName));
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Adds a item to the index.
        /// </summary>
        /// <param name="item">The data to be added to the index.</param>
        public void Add(T item)
        {
            foreach (var fieldName in typeof(T).GetProperties().Select(x => x.Name))
            {
                if (GetReverseIndex(fieldName) is IIndexReverse<T> reverseIndex)
                {
                    reverseIndex.Add(item);
                }
            }

            ForwardIndex.Add(item);
        }

        /// <summary>
        /// The data to be removed from the index.
        /// </summary>
        /// <param name="item">The data to be removed from the index.</param>
        public void Remove(T item)
        {
            foreach (var fieldName in typeof(T).GetProperties().Select(x => x.Name))
            {
                if (GetReverseIndex(fieldName) is IIndexReverse<T> reverseIndex)
                {
                    reverseIndex.Remove(item);
                }
            }

            ForwardIndex.Remove(item);
        }

        /// <summary>
        /// Returns an index field based on its name.
        /// </summary>
        /// <param name="fieldName">The index field name.</param>
        /// <returns>The index field or null.</returns>
        public IIndexReverse<T> GetReverseIndex(string fieldName)
        {
            return ContainsKey(fieldName) ? this[fieldName] : null;
        }
    }
}
