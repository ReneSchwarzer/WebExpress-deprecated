using System.Collections.Generic;

namespace WebExpress.WebApp.WebIndex.Memory
{
    /// <summary>
    /// The forward index.
    /// Key: The id of the item.
    /// Value: The item.
    /// </summary>
    public class IndexMemoryForward<T> : Dictionary<int, T>, IIndexForward<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns all items.
        /// </summary>
        public IEnumerable<T> All => Values;

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            if (!ContainsKey(item.Id))
            {
                Add(item.Id, item);
            }
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            if (ContainsKey(item.Id))
            {
                Remove(item.Id);
            }
        }

        /// <summary>
        /// Returns the item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>The item.</returns>
        public T GetItem(int id)
        {
            if (ContainsKey(id))
            {
                return this[id];
            }

            return default;
        }
    }
}
