namespace WebExpress.WebApp.WebIndex.Storage
{
    /// <summary>
    /// Interface for identifying an index that is kept in the file system.
    /// </summary>
    public interface IIndexStorage
    {
        /// <summary>
        /// Returns the file name for the reverse index.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Returns or sets the reverse index file.
        /// </summary>
        public IndexStorageFile IndexFile { get; }

        /// <summary>
        /// Returns or sets the header.
        /// </summary>
        public IndexStorageSegmentHeader Header { get; }

        /// <summary>
        /// Returns or sets the hash map.
        /// </summary>
        public IndexStorageSegmentHashMap<IndexStorageSegmentTerm> HashMap { get; }

        /// <summary>
        /// Returns or sets the memory manager.
        /// </summary>
        public IndexStorageSegmentAllocator Allocator { get; }

        /// <summary>
        /// Returns the statistical values that can be help to optimize the index.
        /// </summary>
        public IndexStorageSegmentStatistic Statistic { get; }
    }
}
