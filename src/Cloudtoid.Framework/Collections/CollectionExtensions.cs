namespace Cloudtoid
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using static Contract;

    [DebuggerStepThrough]
    public static class CollectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this List<T> value)
            => value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this ICollection<T> value)
            => value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> value)
            => value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this T[] value)
            => value.Length == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this List<T>? value)
            => value is null || value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this ICollection<T>? value)
            => value is null || value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IReadOnlyCollection<T>? value)
            => value is null || value.Count == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this T[]? value)
            => value is null || value.Length == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T>? AsReadOnlyListOrDefault<T>(this IEnumerable<T>? items)
            => items?.AsReadOnlyList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T>? AsListOrDefault<T>(this IEnumerable<T>? items)
            => items?.AsList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> items)
        {
            // Do NOT type check with ICollection<T>. ToArray<T>() already does that type check internally.
            // Also, arrays and lists implement both ICollection<T> and IReadOnlyCollection<T>. Given these are the most common
            // implementations of these interfaces, we don't need to check IReadOnlyCollection<T> explicitly.

            return items as IReadOnlyList<T> ?? items.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T>? AsSetOrDefault<T>(this IEnumerable<T>? items)
            => items?.AsSet();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T>? AsSetOrDefault<T>(this IEnumerable<T>? items, IEqualityComparer<T> comparer)
            => items?.AsSet(comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> AsList<T>(this IEnumerable<T> items)
            => items as IList<T> ?? items.ToList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> AsMutableList<T>(this IEnumerable<T> items)
            => items is IList<T> list && !list.IsReadOnly ? list : items.ToList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] AsArray<T>(this IEnumerable<T> items)
            => items as T[] ?? items.ToArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsSet<T>(this IEnumerable<T> items)
            => items as ISet<T> ?? items.ToHashSet();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer)
            => items as ISet<T> ?? items.ToHashSet(comparer);

        public static void AddRange<TKey, TValue>(
            this ICollection<KeyValuePair<TKey, TValue>> destination,
            IEnumerable<KeyValuePair<TKey, TValue>>? source)
        {
            CheckValue(destination, nameof(destination));

            if (source is null)
                return;

            foreach (var kvp in source)
                destination.Add(kvp);
        }
    }
}