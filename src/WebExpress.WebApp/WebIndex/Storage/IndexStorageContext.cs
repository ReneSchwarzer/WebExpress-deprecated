namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageContext
    {
        private IIndexStorage Index { get; set; }

        /// <summary>
        /// Returns or sets the reverse index file.
        /// </summary>
        public IndexStorageFile IndexFile => Index.IndexFile;

        /// <summary>
        /// Returns or sets the header.
        /// </summary>
        public IndexStorageSegmentHeader Header => Index.Header;

        /// <summary>
        /// Returns or sets the memory manager.
        /// </summary>
        public IndexStorageSegmentAllocator Allocator => Index.Allocator;

        /// <summary>
        /// Returns the statistical values that can be help to optimize the index.
        /// </summary>
        public IndexStorageSegmentStatistic Statistic => Index.Statistic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">The index.</param>
        public IndexStorageContext(IIndexStorage index)
        {
            Index = index;
        }
    }
}
