using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.WebApp.WebIndex.Memory;
using WebExpress.WebApp.WebIndex.Storage;

namespace WebExpress.WebApp.WebIndex
{
    /// <summary>
    /// The IndexDocument is a data structure that provides indexes (for each property of a data type).
    /// Key: The field name.
    /// Value: The reverse index.
    /// </summary>
    public class IndexDocument<T> : Dictionary<PropertyInfo, IIndexReverse<T>>, IIndexDocument<T> where T : IIndexItem
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
        public IEnumerable<string> Fields => Keys.Select(x => x.Name);

        /// <summary>
        /// Returns the predicted capacity (number of items to store) of the index.
        /// </summary>
        public uint Capacity { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="indexType">The index type.</param>
        /// <param name="capacity">The predicted capacity (number of items to store) of the index.</param>
        public IndexDocument(IndexType indexType, uint capacity)
        {
            IndexType = indexType;
            Capacity = capacity;

            switch (IndexType)
            {
                case IndexType.Memory:
                    {
                        ForwardIndex = new IndexMemoryForward<T>();

                        break;
                    }
                default:
                    {
                        ForwardIndex = new IndexMemoryForward<T>();

                        break;
                    }
            }

            foreach (var property in typeof(T).GetProperties())
            {
                Add(property);
            }
        }

        /// <summary>
        /// Adds a field name to the index.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexData interface.</typeparam>
        /// <param name="property">The property that makes up the index.</param>
        public void Add(PropertyInfo property)
        {
            switch (IndexType)
            {
                case IndexType.Memory:
                    {
                        if (!ContainsKey(property))
                        {
                            Add(property, new IndexMemoryReverse<T>(property, Capacity));
                        }

                        break;
                    }
                default:
                    {
                        if (!ContainsKey(property))
                        {
                            Add(property, new IndexStorageReverse<T>(property, Capacity));
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
            foreach (var property in typeof(T).GetProperties().Where(x => x.Name != "Id"))
            {
                if (GetReverseIndex(property) is IIndexReverse<T> reverseIndex)
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
            foreach (var property in typeof(T).GetProperties())
            {
                if (GetReverseIndex(property) is IIndexReverse<T> reverseIndex)
                {
                    reverseIndex.Remove(item);
                }
            }

            ForwardIndex.Remove(item);
        }

        /// <summary>
        /// Returns an index field based on its name.
        /// </summary>
        /// <param name="property">The property that makes up the index.</param>
        /// <returns>The index field or null.</returns>
        public IIndexReverse<T> GetReverseIndex(PropertyInfo property)
        {
            return ContainsKey(property) ? this[property] : null;
        }
    }
}
