namespace Cloudtoid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    /// <summary>
    /// This struct avoids an object allocation if none of the write methods are called. Once the first write operation is called then
    /// an instance of the inner dictionary is instantiated.
    /// </summary>
    public struct ValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private static readonly IDictionary<TKey, TValue> Empty = ImmutableDictionary<TKey, TValue>.Empty;
        private IDictionary<TKey, TValue>? items;

        public ValueDictionary(IDictionary<TKey, TValue>? items)
        {
            this.items = items;
        }

        public ICollection<TKey> Keys
            => (items ?? Empty).Keys;

        public ICollection<TValue> Values
            => (items ?? Empty).Values;

        public int Count
            => items is null ? 0 : items.Count;

        public bool IsReadOnly
            => items != null && items.IsReadOnly;

        public TValue this[TKey key]
        {
            get => (items ?? Empty)[key];
            set => EnsureItems()[key] = value;
        }

        public void Add(TKey key, TValue value) => EnsureItems().Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item) => EnsureItems().Add(item);

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
            => (items ?? Empty).CopyTo(array, arrayIndex);

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
            => (items ?? Empty).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => (items ?? Empty).GetEnumerator();

        private IDictionary<TKey, TValue> EnsureItems()
        {
            if (items == null)
                items = new Dictionary<TKey, TValue>();

            return items;
        }
    }
}
