using System.Collections.Generic;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Exposes a method that compares two keys by the length.
    /// </summary>
    public class WqlParserLengthComparer : IComparer<string>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether 
        /// one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first key to compare.</param>
        /// <param name="y">The second key to compare.</param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            int lengthComparison = x.Length.CompareTo(y.Length);
            if (lengthComparison == 0)
            {
                return x.CompareTo(y);
            }
            else
            {
                return lengthComparison * -1;
            }
        }
    }
}
