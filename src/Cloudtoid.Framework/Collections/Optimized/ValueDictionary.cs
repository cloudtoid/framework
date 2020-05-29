namespace Cloudtoid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// This struct avoids an object allocation if none of the write methods are called. Once the first write operation is called then
    /// an instance of the inner <see cref="Dictionary{TKey, TValue}"/> is instantiated.
    /// </summary>
    public struct ValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public static readonly ValueDictionary<TKey, TValue> Empty = default;
        private IDictionary<TKey, TValue>? items;

        public ValueDictionary(IDictionary<TKey, TValue>? items)
        {
            this.items = items;
        }

        public ICollection<TKey> Keys
            => items is null ? Array.Empty<TKey>() : items.Keys;

        public ICollection<TValue> Values
            => items is null ? Array.Empty<TValue>() : items.Values;

        public int Count
            => items is null ? 0 : items.Count;

        public bool IsReadOnly
            => items != null && items.IsReadOnly;

        public TValue this[TKey key]
        {
            get => items is null ? throw new KeyNotFoundException() : items[key];
            set => EnsureItems()[key] = value;
        }

        public void Add(TKey key, TValue value)
            => EnsureItems().Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item)
            => EnsureItems().Add(item);

        public void Clear()
            => items?.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item)
            => items != null && items.Contains(item);

        public bool ContainsKey(TKey key)
            => items != null && items.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => items?.CopyTo(array, arrayIndex);

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
            => items is null ? EmptyEnumerator<KeyValuePair<TKey, TValue>>.Instance : items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        // This is only used for testing
        internal IDictionary<TKey, TValue>? GetInner()
            => items;

        private IDictionary<TKey, TValue> EnsureItems()
            => items ??= new Dictionary<TKey, TValue>();
    }
}
