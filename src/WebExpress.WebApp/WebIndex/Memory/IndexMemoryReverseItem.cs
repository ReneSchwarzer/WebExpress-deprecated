using System.Collections.Concurrent;

namespace WebExpress.WebApp.WebIndex.Memory
{
    /// <summary>
    /// Saves the referenc to the items.
    /// Key: The id of the item.
    /// Value: The position of the term in the input value.
    /// </summary>
    public class IndexMemoryReverseItem<T> : ConcurrentDictionary<int, IndexMemoryReversePosition> where T : IIndexItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="position">The position of the term in the input value.</param>
        public IndexMemoryReverseItem(T item, uint position)
        {
            GetOrAdd(item.Id, new IndexMemoryReversePosition() { position });
        }

        /// <summary>
        /// Added an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="position">The position of the term in the input value.</param>
        public void Add(T item, uint position)
        {
            GetOrAdd(item.Id, new IndexMemoryReversePosition() { position });
        }

        /// <summary>
        /// Returns the item by id.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>An enumeration of the position data.</returns>
        public IndexMemoryReversePosition GetIndexItem(int id)
        {
            return ContainsKey(id) ? this[id] : null;
        }
    }
}
