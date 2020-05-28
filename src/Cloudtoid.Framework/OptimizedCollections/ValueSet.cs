namespace Cloudtoid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    /// <summary>
    /// This struct avoids an object allocation if none of the write methods are called. Once the first write operation is called then
    /// an instance of the inner <see cref="HashSet{TValue}"/> is instantiated.
    /// </summary>
    public struct ValueSet<TValue> : ISet<TValue>
    {
        private static readonly ISet<TValue> Empty = ImmutableHashSet<TValue>.Empty;
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
            => EnsureItems().Add(item);

        public void Clear()
        {
            if (items != null)
                items.Clear();
        }

        public bool Contains(TValue item)
            => items != null && items.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex)
            => (items ?? Empty).CopyTo(array, arrayIndex);

        public void ExceptWith(IEnumerable<TValue> other)
        {
            if (items != null)
                items.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<TValue> other)
        {
            if (items != null)
                items.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<TValue> other)
            => (items ?? Empty).IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<TValue> other)
            => (items ?? Empty).IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<TValue> other)
            => (items ?? Empty).IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<TValue> other)
            => (items ?? Empty).IsSupersetOf(other);

        public bool Overlaps(IEnumerable<TValue> other)
            => (items ?? Empty).Overlaps(other);

        public bool Remove(TValue item)
            => items != null && items.Remove(item);

        public bool SetEquals(IEnumerable<TValue> other)
            => (items ?? Empty).SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<TValue> other)
            => EnsureItems().SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<TValue> other)
            => EnsureItems().UnionWith(other);

        public IEnumerator<TValue> GetEnumerator()
            => (items ?? Empty).GetEnumerator();

        private ISet<TValue> EnsureItems()
        {
            if (items == null)
                items = new HashSet<TValue>();

            return items;
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new System.NotImplementedException();
    }
}
