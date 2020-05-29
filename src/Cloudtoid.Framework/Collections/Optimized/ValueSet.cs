namespace Cloudtoid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This struct avoids an object allocation if none of the write methods are called. Once the first write operation is called then
    /// an instance of the inner <see cref="HashSet{TValue}"/> is instantiated.
    /// </summary>
    public struct ValueSet<TValue> : ISet<TValue>
    {
        public static readonly ValueSet<TValue> Empty = default;
        private static readonly ISet<TValue> EmptySet = ImmutableHashSet<TValue>.Empty;
        private ISet<TValue>? items;

        public ValueSet(ISet<TValue>? items)
        {
            this.items = items;
        }

        public readonly int Count
            => items is null ? 0 : items.Count;

        public readonly bool IsReadOnly
            => items != null && items.IsReadOnly;

        public bool Add(TValue item)
            => EnsureItems().Add(item);

        void ICollection<TValue>.Add(TValue item)
            => Add(item);

        public readonly void Clear()
            => items?.Clear();

        public readonly bool Contains(TValue item)
            => items != null && items.Contains(item);

        public readonly void CopyTo(TValue[] array, int arrayIndex)
            => items?.CopyTo(array, arrayIndex);

        public readonly void ExceptWith(IEnumerable<TValue> other)
            => items?.ExceptWith(other);

        public readonly void IntersectWith(IEnumerable<TValue> other)
            => items?.IntersectWith(other);

        public readonly bool IsProperSubsetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsProperSubsetOf(other);

        public readonly bool IsProperSupersetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsProperSupersetOf(other);

        public readonly bool IsSubsetOf(IEnumerable<TValue> other)
            => items is null || items.IsSubsetOf(other);

        public readonly bool IsSupersetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsSupersetOf(other);

        public readonly bool Overlaps(IEnumerable<TValue> other)
            => items != null && items.Overlaps(other);

        public readonly bool Remove(TValue item)
            => items != null && items.Remove(item);

        public readonly bool SetEquals(IEnumerable<TValue> other)
            => (items ?? EmptySet).SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<TValue> other)
            => EnsureItems().SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<TValue> other)
            => EnsureItems().UnionWith(other);

        public readonly IEnumerator<TValue> GetEnumerator()
            => items is null ? EmptyEnumerator<TValue>.Instance : items.GetEnumerator();

        [ExcludeFromCodeCoverage]
        readonly IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        // This is only used for testing
        internal readonly ISet<TValue>? GetInner()
            => items;

        private ISet<TValue> EnsureItems()
            => items ??= new HashSet<TValue>();
    }
}
