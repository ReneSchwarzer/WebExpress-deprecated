using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebExpress.UI.WebComponent
{
    public class ComponentComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Testet auf Gleichheit 
        /// </summary>
        /// <param name="x">Das erste zu vergleichende Objekt</param>
        /// <param name="y">Das zweite zu vergleichende Objekt</param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x.GetType() == y.GetType())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gibt einen Hashcode für das angegebene Objekt zurück.
        /// </summary>
        /// <param name="obj">Das Object, für das ein Hashcode zurückgegeben werden soll.</param>
        /// <returns>Der Hashcode</returns>
        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}
