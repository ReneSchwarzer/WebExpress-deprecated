using System;
using System.Collections.Generic;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// A ring buffer for circular buffering of data structures.
    /// </summary>
    public class IndexStorageRingBuffer
    {
        /// <summary>
        /// The ring buffer.
        /// </summary>
        private IIndexStorageDataStructure[] buffer;

        /// <summary>
        /// Buffer for random access.
        /// </summary>
        private Dictionary<ulong, IIndexStorageDataStructure> dict;

        /// <summary>
        /// The beginning of the ring buffer.
        /// </summary>
        private int head;

        /// <summary>
        /// The end of the ring buffer.
        /// </summary>
        private int tail;

        /// <summary>
        /// Event is triggered when the insertion of new data displaces existing data.
        /// </summary>
        public event EventHandler<IIndexStorageDataStructure> DataOverwritten;

        /// <summary>
        /// Returns a data structure if sored.
        /// </summary>
        /// <param name="addr">The address of th data structure.</param>
        public IIndexStorageDataStructure this[ulong addr]
        {
            get => dict.ContainsKey(addr) ? dict[addr] : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capacity">The number of elements to be stored in the ring buffer.</param>
        public IndexStorageRingBuffer(uint capacity)
        {
            buffer = new IIndexStorageDataStructure[capacity];
            dict = new Dictionary<ulong, IIndexStorageDataStructure>((int)capacity);
            head = 0;
            tail = 0;
        }

        /// <summary>
        /// Adds an item to the end of the ring buffer.
        /// </summary>
        /// <param name="item">The data structure.</param>
        public void Enqueue(IIndexStorageDataStructure item)
        {
            if (dict.ContainsKey(item.Addr))
            {
                return;
            }

            if (tail == head && buffer[tail] != null)
            {
                OnDataOverwritten(buffer[tail]);
            }

            buffer[tail] = item;
            tail = (tail + 1) % buffer.Length;

            dict.Add(item.Addr, item);

            if (tail == head)
            {
                head = (head + 1) % buffer.Length;
            }
        }

        /// <summary>
        /// Removes an element at the beginning of the ring buffer.
        /// </summary>
        /// <returns>The data structure.</returns>
        public IIndexStorageDataStructure Dequeue()
        {
            if (head == tail)
            {
                return null;
            }

            var item = buffer[head];
            head = (head + 1) % buffer.Length;

            return item;
        }

        /// <summary>
        /// Checks whether a data structure exists at the given address.
        /// </summary>
        /// <param name="addr"></param>
        /// <returns>The address of th data structure.</returns>
        public bool Contains(ulong addr)
        {
            return dict.ContainsKey(addr);
        }

        /// <summary>
        /// Triggers when the event DataOverwritten is to be triggered.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnDataOverwritten(IIndexStorageDataStructure e)
        {
            DataOverwritten?.Invoke(this, e);
        }
    }
}
