using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebExpress.WebApp.WebIndex.Memory
{
    /// <summary>
    /// Provides a reverse index that manages the data in the main memory.
    /// Key: The terms.
    /// Value: The index item.
    /// </summary>
    public class IndexMemoryReverse<T> : Dictionary<object, IndexMemoryReverseItem<T>>, IIndexReverse<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the field name for the reverse index.
        /// </summary>
        public string Field => Property?.Name;

        /// <summary>
        /// A delegate that determines the value of the current property.
        /// </summary>
        private Func<T, object> GetValueDelegate { get; set; }

        /// <summary>
        /// The property that makes up the index.
        /// </summary>
        private PropertyInfo Property { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">The property that makes up the index.</param>
        /// <param name="capacity">The predicted capacity (number of items to store) of the reverse index.</param>
        public IndexMemoryReverse(PropertyInfo property, uint capacity)
            : base((int)capacity)
        {
            Property = property;
            GetValueDelegate = CreateDelegate(property);
        }

        /// <summary>
        /// Adds a item to the field.
        /// </summary>
        /// <typeparam name="T">The data type. This must have the IIndexItem interface.</typeparam>
        /// <param name="item">The data to be added to the index.</param>
        public void Add(T item)
        {
            var value = GetValueDelegate(item);

            if (value is string str)
            {
                var terms = IndexTermTokenizer.Tokenize(str);

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
            else if (value is int v)
            {
                if (GetIndexItems(v) is IndexMemoryReverseItem<T> itemStore)
                {
                    itemStore.Add(item, 0);
                }
                else
                {
                    Add(v, new IndexMemoryReverseItem<T>(item, 0));
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
        public IEnumerable<int> Collect(object term)
        {
            var offset = 1;
            var terms = IndexTermTokenizer.Tokenize(term?.ToString());

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
                            if (postionsNext.Contains(posItem + (uint)offset))
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

        /// <summary>
        /// Returns an enumeration of the data contained for a term.
        /// </summary>
        /// <param name="key">The index term.</param>
        /// <returns>An enumeration of the data contained for a term.</returns>
        private IndexMemoryReverseItem<T> GetIndexItems(object key)
        {
            return ContainsKey(key) ? this[key] : null;
        }

        /// <summary>
        /// Creates a delegate for faster access to the value of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>A delegate that determines the value of the current property.</returns>
        private static Func<T, object> CreateDelegate(PropertyInfo property)
        {
            var helper = typeof(IndexMemoryReverse<T>).GetMethod("CreateDelegateInternal", BindingFlags.Static | BindingFlags.NonPublic);
            var method = helper.MakeGenericMethod(typeof(T), property.PropertyType);

            return (Func<T, object>)method.Invoke(null, new object[] { property.GetGetMethod() });
        }

        /// <summary>
        /// An auxiliary function used to determine the value of a property.
        /// </summary>
        /// <param name="methodInfo">The method Info.</param>
        /// <returns>A delegate that determines the value of the current property.</returns>
        private static Func<X, object> CreateDelegateInternal<X, TReturn>(MethodInfo methodInfo)
        {
            var f = (Func<X, TReturn>)System.Delegate.CreateDelegate(typeof(Func<X, TReturn>), methodInfo);
            return t => (object)f(t);
        }
    }
}
