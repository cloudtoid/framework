namespace Cloudtoid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is a lightweight value type that wraps an existing list or a single item and lazily makes it a mutable <see cref="IList{TValue}"/>.
    /// If the list is NOT mutable, it is lazily copied into a new mutable list. This happens only on operations such as
    /// <see cref="Add(TValue)"/> and <see cref="Insert(int, TValue)"/> that require a mutable list.
    /// </summary>
    public struct LazyValueList<TValue> : IList<TValue>
    {
        private static readonly TValue[] EmptyArray = Array.Empty<TValue>();
        private IList<TValue>? items;

        public LazyValueList(IEnumerable<TValue> items)
        {
            this.items = items.AsList();
        }

        public LazyValueList(TValue item)
        {
            items = new ReadOnlyValueList<TValue>(item);
        }

        public int Count
            => items is null ? 0 : items.Count;

        public bool IsReadOnly
            => false;

        public TValue this[int index]
        {
            get => items is null ? throw new IndexOutOfRangeException() : items[index];
            set => EnsureNotReadOnly()[index] = value;
        }

        public void Insert(int index, TValue item)
            => EnsureNotReadOnly().Insert(index, item);

        public void Add(TValue item)
            => EnsureNotReadOnly().Add(item);

        public void RemoveAt(int index)
        {
            if (items is null)
                throw new ArgumentOutOfRangeException();

            EnsureNotReadOnly().RemoveAt(index);
        }

        public bool Remove(TValue item)
            => items != null && items.Count != 0 && EnsureNotReadOnly().Remove(item);

        public int IndexOf(TValue item)
            => items is null ? -1 : items.IndexOf(item);

        public void Clear()
        {
            if (items?.Count > 0)
                items.Clear();
        }

        public bool Contains(TValue item)
            => items != null && items.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            if (items != null)
                items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TValue> GetEnumerator()
            => (items ?? EmptyArray).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private IList<TValue> EnsureNotReadOnly()
        {
            if (items is null)
                return items = new List<TValue>();

            if (items is TValue[] || items.IsReadOnly)
                return items = items.ToList();

            return items;
        }
    }
}
