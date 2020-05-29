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

        public int Count
            => items is null ? 0 : items.Count;

        public bool IsReadOnly
            => items != null && items.IsReadOnly;

        public bool Add(TValue item)
            => EnsureItems().Add(item);

        void ICollection<TValue>.Add(TValue item)
            => Add(item);

        public void Clear()
            => items?.Clear();

        public bool Contains(TValue item)
            => items != null && items.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex)
            => items?.CopyTo(array, arrayIndex);

        public void ExceptWith(IEnumerable<TValue> other)
            => items?.ExceptWith(other);

        public void IntersectWith(IEnumerable<TValue> other)
            => items?.IntersectWith(other);

        public bool IsProperSubsetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<TValue> other)
            => items is null || items.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<TValue> other)
            => (items ?? EmptySet).IsSupersetOf(other);

        public bool Overlaps(IEnumerable<TValue> other)
            => items != null && items.Overlaps(other);

        public bool Remove(TValue item)
            => items != null && items.Remove(item);

        public bool SetEquals(IEnumerable<TValue> other)
            => (items ?? EmptySet).SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<TValue> other)
            => EnsureItems().SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<TValue> other)
            => EnsureItems().UnionWith(other);

        public IEnumerator<TValue> GetEnumerator()
            => (items ?? EmptySet).GetEnumerator();

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        // This is only used for testing
        internal ISet<TValue>? GetInner()
            => items;

        private ISet<TValue> EnsureItems()
            => items ??= new HashSet<TValue>();
    }
}
