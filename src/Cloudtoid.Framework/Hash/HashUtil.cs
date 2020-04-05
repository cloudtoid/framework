namespace Cloudtoid
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using static Contract;

    /// <summary>
    /// Utility methods for generating and combining hash codes.
    /// </summary>
    [DebuggerStepThrough]
    public static class HashUtil
    {
        // A fall-back hash code to use for null values.  Avoid zero just to give a better distribution.
        public const int NullHashCode = -0xBEEF;

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Combine(uint u1, uint u2)
            => ((u1 << 7) | (u1 >> 25)) ^ u2;

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int n1, int n2)
            => (int)Combine((uint)n1, (uint)n2);

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int n1, int n2, int n3)
        {
            var hash = Combine((uint)n1, (uint)n2);
            return (int)Combine(hash, (uint)n3);
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4, int n5)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            hash = Combine(hash, (uint)n5);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4, int n5, int n6)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            hash = Combine(hash, (uint)n5);
            hash = Combine(hash, (uint)n6);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4, int n5, int n6, int n7)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            hash = Combine(hash, (uint)n5);
            hash = Combine(hash, (uint)n6);
            hash = Combine(hash, (uint)n7);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            hash = Combine(hash, (uint)n5);
            hash = Combine(hash, (uint)n6);
            hash = Combine(hash, (uint)n7);
            hash = Combine(hash, (uint)n8);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash codes and creates a new hash code
        /// </summary>
        public static int Combine(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9)
        {
            uint hash;
            hash = Combine((uint)n1, (uint)n2);
            hash = Combine(hash, (uint)n3);
            hash = Combine(hash, (uint)n4);
            hash = Combine(hash, (uint)n5);
            hash = Combine(hash, (uint)n6);
            hash = Combine(hash, (uint)n7);
            hash = Combine(hash, (uint)n8);
            hash = Combine(hash, (uint)n9);
            return (int)hash;
        }

        /// <summary>
        /// Combines the hash code from each object in the specified list.  The specified comparer
        /// is used to generate the hash code.  The position of each value is reflected in the resulting
        /// hash code.
        /// </summary>
        public static int Combine<T>(IList<T> values, IEqualityComparer<T>? comparer = null)
        {
            CheckValue(values, nameof(values));

            var hash = values.Count;
            comparer ??= EqualityComparer<T>.Default;

            for (var i = 0; i < values.Count; i++)
            {
                var value = values[i];
                var valueHash = 0;
                if (value != null)
                    valueHash = comparer.GetHashCode(value);

                // This does a couple of interesting things:
                // 1) By shifting, we account for the position of the value within the
                //    overall sequence
                // 2) By adding back the hash, we ensure we don't just shift all the
                //    meaningful bytes off the left edge of the value.
                hash = ((hash << 5) + hash) ^ valueHash;
            }

            return hash;
        }

        /// <summary>
        /// Combines the hash code from each object in the specified enumeration.  The specified comparer
        /// is used to generate the hash code.  The position of each value is reflected in the resulting
        /// hash code.
        /// </summary>
        public static int Combine<T>(IEnumerable<T> values, int count, IEqualityComparer<T>? comparer = null)
        {
            CheckValue(values, nameof(values));

            var hash = count;
            comparer ??= EqualityComparer<T>.Default;

            foreach (var value in values)
            {
                var valueHash = 0;
                if (value != null)
                    valueHash = comparer.GetHashCode(value);

                // This does a couple of interesting things:
                // 1) By shifting, we account for the position of the value within the
                //    overall sequence
                // 2) By adding back the hash, we ensure we don't just shift all the
                //    meaningful bytes off the left edge of the value.
                hash = ((hash << 5) + hash) ^ valueHash;
            }

            return hash;
        }

        /// <summary>
        /// Computes a unique hash code for a set
        /// </summary>
        /// <remarks>
        /// This implementation is found in System.Collections.Generic.HashSetEqualityComparer implementation in .NET framework
        /// </remarks>
        public static int Combine<T>(ISet<T> values, IEqualityComparer<T>? comparer = null)
        {
            CheckValue(values, nameof(values));

            comparer ??= EqualityComparer<T>.Default;

            int hash = 0;
            foreach (var value in values)
                hash ^= CombineCommutative(GetHashCode(value, comparer));

            return hash;
        }

        /// <summary>
        /// Computes a unique hash code for a dictionary
        /// </summary>
        /// <remarks>
        /// Please note:
        /// 1) this implementation is slow, please be aware while using it with Big dictionaries
        /// 2) always pass the same key comparer that is used in the dictionary.
        /// </remarks>
        public static int Combine<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null) where TKey : notnull
        {
            CheckValue(dictionary, nameof(dictionary));

            keyComparer ??= EqualityComparer<TKey>.Default;
            valueComparer ??= EqualityComparer<TValue>.Default;

            var hash = dictionary.Count;
            foreach (var pair in dictionary)
                hash ^= CombineCommutative(Combine(GetHashCode(pair.Key, keyComparer), GetHashCode(pair.Value, valueComparer)));

            return hash;
        }

        /// <summary>
        /// This method used to compute the impact of one element's hash code on the parent collection hash code
        /// The order of the element in the collection is irrelevant
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineCommutative(int elementHashcode)
            => elementHashcode & 0x7FFFFFFF;

        /// <summary>
        /// Uses the <paramref name="comparer"/> to get the hash code of the <paramref name="value"/>.
        /// Avoids calling <paramref name="comparer"/> if <paramref name="value"/> is null, because most comparers
        /// do not support this.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode<T>(T value, IEqualityComparer<T> comparer)
            => value is null ? NullHashCode : comparer.GetHashCode(value);
    }
}