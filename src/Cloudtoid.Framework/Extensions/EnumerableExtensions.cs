namespace Cloudtoid
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public static class EnumerableExtensions
    {
        public static IEnumerable<TItem> Concat<TItem>(this IEnumerable<TItem>? items, TItem item)
            => items.ConcatOrEmpty(new[] { item });

        /// <summary>
        /// It safely concatenates two enumerables. If the enumerables are null, they are treated as empty.
        /// </summary>
        public static IEnumerable<TItem> ConcatOrEmpty<TItem>(this IEnumerable<TItem>? first, IEnumerable<TItem>? second)
        {
            if (second is null)
                return first ?? Array.Empty<TItem>();

            if (first is null)
                return second;

            return Enumerable.Concat(first, second);
        }

        public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string?> items)
            => items.Where(s => !string.IsNullOrEmpty(s))!;

        public static IEnumerable<TItem> WhereNotNull<TItem>(this IEnumerable<TItem?> items) where TItem : class
            => items.Where(i => !(i is null))!;

        public static IEnumerable<TItem> WhereNotNull<TItem>(this IEnumerable<TItem?> items) where TItem : struct
            => items.Where(i => i.HasValue).Select(i => i!.Value);

        public static int IndexOf<TItem>(this IEnumerable<TItem> items, TItem item, IEqualityComparer<TItem>? comparer = null)
        {
            comparer ??= EqualityComparer<TItem>.Default;

            int i = 0;
            foreach (var it in items)
            {
                if (comparer.Equals(item, it))
                    return i;

                i++;
            }

            return -1;
        }
    }
}
