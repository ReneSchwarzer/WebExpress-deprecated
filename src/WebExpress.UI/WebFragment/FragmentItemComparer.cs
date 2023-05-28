using System.Collections.Generic;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// Test of two fragments for equality.
    /// </summary>
    internal class FragmentItemComparer : IComparer<FragmentItem>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one value is lower, equal, or greater than the other value.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that specifies the relative values of x and y. Less than 0 => x is less than y. Greater than 0 => x is greater than y. 0 (zero) => x is equal to y.</returns>
        public int Compare(FragmentItem x, FragmentItem y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order < y.Order)
            {
                return -1;
            }

            return 0;
        }
    }
}
