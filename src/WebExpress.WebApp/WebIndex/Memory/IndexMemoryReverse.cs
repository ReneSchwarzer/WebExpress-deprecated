using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.WebIndex.Memory
{
    /// <summary>
    /// Provides a reverse index that manages the data in the main memory.
    /// Key: The terms.
    /// Value: The index item.
    /// </summary>
    public class IndexMemoryReverse<T> : Dictionary<string, IndexMemoryReverseItem<T>>, IIndexReverse<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the field name for the reverse index.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        public IndexMemoryReverse(string fieldName)
        {
            Field = fieldName;
        }

        /// <summary>
        /// Adds a item to the field.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexItem interface.</typeparam>
        /// <param name="item">The data to be added to the index.</param>
        public void Add(T item)
        {
            var value = typeof(T).GetProperty(Field)?.GetValue(item)?.ToString();
            var tokens = IndexTermTokenizer.Tokenize(value);
            var terms = IndexTermNormalizer.Normalize(tokens);

            foreach (var term in terms)
            {
                if (GetIndexItems(term) is IndexMemoryReverseItem<T> itemStore)
                {
                    if (itemStore.GetIndexItem(item.Id) is IndexMemoryReversePosition itemPosition)
                    {
                        itemPosition.Add(term.Position);
                    }
                    else
                    {
                        itemStore.Add(item, term.Position);
                    }
                }
                else
                {
                    Add(term.Value, new IndexMemoryReverseItem<T>(item, term.Position));
                }
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
        public IEnumerable<int> Collect(string term)
        {
            var offset = 1;
            var token = IndexTermTokenizer.Tokenize(term?.ToString());
            var terms = IndexTermNormalizer.Normalize(token);

            // processing the first token
            var items = GetIndexItems(terms.FirstOrDefault());
            var hashSet = new HashSet<int>(items.Keys);

            // processing the next token
            foreach (var normalizedToken in terms.Skip(1))
            {
                var next = GetIndexItems(normalizedToken);

                foreach (var hash in hashSet.ToList())
                {
                    // check if an item is also available in the next term
                    if (next.ContainsKey(hash))
                    {
                        var hasSuccessor = false;
                        var postionsItem = items.ContainsKey(hash) ? items[hash] : null;
                        var postionsNext = next.ContainsKey(hash) ? next[hash] : null;

                        // compare the positions of the terms
                        foreach (var posItem in postionsItem)
                        {
                            if (postionsNext.Contains(posItem + offset))
                            {
                                hasSuccessor = true;

                                break;
                            }
                        }

                        if (!hasSuccessor)
                        {
                            hashSet.Remove(hash);
                        }
                    }
                    else
                    {
                        hashSet.Remove(hash);
                    }
                }

                offset++;
            }

            return hashSet;
        }

        /// <summary>
        /// Returns an enumeration of the data contained for a term.
        /// </summary>
        /// <param name="term">The index term.</param>
        /// <returns>An enumeration of the data contained for a term.</returns>
        private IndexMemoryReverseItem<T> GetIndexItems(IndexTermToken term)
        {
            return ContainsKey(term?.Value) ? this[term?.Value] : null;
        }
    }
}
