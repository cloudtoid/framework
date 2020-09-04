using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    /// <summary>
    /// Utility methods for generating and combining hash codes.
    /// </summary>
    [DebuggerStepThrough]
    public static class HashUtil
    {
        // A fall-back hash code to use for null values.  Avoid zero just to give a better distribution.
        public const int NullHashCode = -0xBEEF;

        /// <summary>
        /// Combines the hash code from each object in the specified list.  The specified comparer
        /// is used to generate the hash code.  The position of each value is reflected in the resulting
        /// hash code.
        /// </summary>
        public static int Combine<T>(IList<T> values, IEqualityComparer<T>? comparer = null)
        {
            CheckValue(values, nameof(values));

            comparer ??= EqualityComparer<T>.Default;
            var count = values.Count;
            var hashCode = new HashCode();
            for (var i = 0; i < count; i++)
                hashCode.Add(values[i], comparer);

            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Combines the hash code from each object in the specified enumeration.  The specified comparer
        /// is used to generate the hash code.  The position of each value is reflected in the resulting
        /// hash code.
        /// </summary>
        public static int Combine<T>(IEnumerable<T> values, IEqualityComparer<T>? comparer = null)
        {
            CheckValue(values, nameof(values));
            comparer ??= EqualityComparer<T>.Default;
            var hashCode = new HashCode();
            foreach (var value in values)
                hashCode.Add(value, comparer);

            return hashCode.ToHashCode();
        }
    }
}