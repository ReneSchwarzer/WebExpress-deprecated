using System;

namespace WebExpress.Pages
{
    public class UriSegmentID : IEquatable<UriSegmentID>
    {
        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        protected string Value { get; set; }

        /// <summary>
        /// Vergleichsoperator
        /// </summary>
        /// <param name="id1">Die ID</param>
        /// <param name="id2">Die ID</param>
        /// <returns></returns>
        public static bool operator ==(UriSegmentID id1, UriSegmentID id2) => id1.Equals(id2);

        /// <summary>
        /// Vergleichsoperator
        /// </summary>
        /// <param name="id1">Die ID</param>
        /// <param name="id2">Die ID</param>
        /// <returns></returns>
        public static bool operator !=(UriSegmentID id1, UriSegmentID id2) => !id1.Equals(id2);

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public UriSegmentID(string id)
        {
            Value = id.ToLower();
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public UriSegmentID(UriSegmentID id)
        {
            Value = id.Value;
        }

        /// <summary>
        /// Vergleichen
        /// </summary>
        /// <param name="obj">Das zu vergleichende Objekt</param>
        /// <returns>true, wenn Gleichheit besteht</returns>
        public override bool Equals(object obj)
        {
            if (obj is UriSegmentID id)
            {
                return id.Value.Equals(Value);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Vergleichen
        /// </summary>
        /// <param name="id">Die zu vergleichende PageID</param>
        /// <returns>true, wenn Gleichheit besteht</returns>
        public virtual bool Equals(UriSegmentID id)
        {
            return id.Value.Equals(Value);
        }

        /// <summary>
        /// Vergleichen
        /// </summary>
        /// <param name="id">Die zu vergleichende PageID</param>
        /// <returns>true, wenn Gleichheit besteht</returns>
        public virtual bool Equals(string id)
        {
            return Value.Equals(id?.ToLower());
        }

        /// <summary>
        /// Liefert den Hash-Code
        /// </summary>
        /// <returns>Der HashCode</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Wandelt die ID in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
