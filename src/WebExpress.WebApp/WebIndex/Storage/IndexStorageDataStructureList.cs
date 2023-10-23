using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// A simple concatenated sorted list.
    /// </summary>
    /// <typeparam name="T">The list item type.</typeparam>
    public class IndexStorageDataStructureList<T> : IndexStorageDataStructure, IEnumerable<T>
        where T : IIndexStorageDataStructureListItem
    {
        /// <summary>
        /// Returns or sets the address of the first item in the list, or null if the 
        /// list is empty.
        /// </summary>
        public ulong FirstItemAddr { get; set; }
        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint SizeOf => sizeof(ulong);

        /// <summary>
        /// Checks if the list is empty.
        /// </summary>
        public bool IsEmpty => FirstItemAddr == 0;

        /// <summary>
        /// Returns or sets the sort order of the list.
        /// </summary>
        public ListSortDirection SortDirection { get; private set; }

        /// <summary>
        /// Gets the number of elements contained in the list. Determining 
        /// the number of stored items has an effort of o(n).
        /// </summary>
        public int Count
        {
            get
            {
                var index = 0;

                foreach (var i in this)
                {
                    index++;
                }

                return index;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        /// <param name="sortDirection">The sort order of the list.</param>
        public IndexStorageDataStructureList(IndexStorageContext context, ListSortDirection sortDirection = ListSortDirection.Ascending)
            : base(context)
        {
            SortDirection = sortDirection;
            Context.Allocator.Alloc(this);
        }

        /// <summary>
        /// Adds an item to the list if it doesn't already exist.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <returns>The new item, if it is not already in the list, otherwise the existing item.</returns>
        public T Add(T item)
        {
            if (FirstItemAddr == 0)
            {
                FirstItemAddr = Context.Allocator.Alloc(item);

                Context.IndexFile.Write(this);
                Context.IndexFile.Write(item);
            }
            else
            {
                var last = default(T);
                var count = 0U;

                foreach (var i in this)
                {
                    var compare = i.CompareTo(item);

                    if (compare > 0 && SortDirection == ListSortDirection.Ascending)
                    {
                        break;
                    }
                    else if (compare < 0 && SortDirection == ListSortDirection.Descending)
                    {
                        break;
                    }
                    else if (compare == 0)
                    {
                        return i;
                    }

                    last = i;

                    count++;
                }

                if (last == null)
                {
                    var tempAddr = FirstItemAddr;
                    FirstItemAddr = Context.Allocator.Alloc(item);
                    item.SuccessorAddr = tempAddr;

                    Context.IndexFile.Write(this);
                    Context.IndexFile.Write(item);
                }
                else
                {
                    var tempAddr = last.SuccessorAddr;
                    last.SuccessorAddr = Context.Allocator.Alloc(item);
                    item.SuccessorAddr = tempAddr;

                    Context.IndexFile.Write(this);
                    Context.IndexFile.Write(last);
                    Context.IndexFile.Write(item);

                    Context.Statistic.AddCollision(item, count);
                }
            }

            return item;
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            FirstItemAddr = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(FirstItemAddr);
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            foreach (var item in this)
            {
                Context.Allocator.Free(item);
            }

            FirstItemAddr = 0;
            Context.IndexFile.Write(this);
        }

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the list..</param>
        /// <returns>True if item is found in the list, otherwise false.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Contains(T item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list.
        /// </summary>
        /// <param name="item">The object to remove from the list.</param>
        /// <returns>True if item was successfully removed from the list, 
        /// otherwise false. This method also returns false if item is not 
        /// found in the list.</returns>
        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty)
            {
                yield break;
            }

            var addr = FirstItemAddr;
            var item = default(T);

            do
            {
                item = Context.IndexFile.Read<T>(addr, Context);

                yield return item;

            } while ((addr = item.SuccessorAddr) != 0);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the list.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
