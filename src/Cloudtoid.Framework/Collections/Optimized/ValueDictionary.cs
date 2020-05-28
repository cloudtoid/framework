namespace Cloudtoid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    /// <summary>
    /// This struct avoids an object allocation if none of the write methods are called. Once the first write operation is called then
    /// an instance of the inner <see cref="Dictionary{TKey, TValue}"/> is instantiated.
    /// </summary>
    public struct ValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public static readonly ValueDictionary<TKey, TValue> Empty = default;
        private static readonly IDictionary<TKey, TValue> EmptyDic = ImmutableDictionary<TKey, TValue>.Empty;
        private IDictionary<TKey, TValue>? items;

        public ValueDictionary(IDictionary<TKey, TValue>? items)
        {
            this.items = items;
        }

        public ICollection<TKey> Keys
            => (items ?? EmptyDic).Keys;

        public ICollection<TValue> Values
            => (items ?? EmptyDic).Values;

        public int Count
            => items is null ? 0 : items.Count;

        public bool IsReadOnly
            => items != null && items.IsReadOnly;

        public TValue this[TKey key]
        {
            get => (items ?? EmptyDic)[key];
            set => EnsureItems()[key] = value;
        }

        public void Add(TKey key, TValue value)
            => EnsureItems().Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item)
            => EnsureItems().Add(item);

        public void Clear()
        {
            if (items != null)
                items.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
            => items != null && items.Contains(item);

        public bool ContainsKey(TKey key)
            => items != null && items.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (items != null)
                items.CopyTo(array, arrayIndex);
        }

        public bool Remove(TKey key)
            => items != null && items.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item)
            => items != null && items.Remove(item);

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (items is null)
            {
                value = default!;
                return false;
            }

            return items.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => (items ?? EmptyDic).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        // This is only used for testing
        internal IDictionary<TKey, TValue>? GetInner()
            => items;

        private IDictionary<TKey, TValue> EnsureItems()
        {
            if (items == null)
                items = new Dictionary<TKey, TValue>();

            return items;
        }
    }
}
