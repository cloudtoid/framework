using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cloudtoid
{
    /// <summary>
    /// Represents zero/<see langword="null"/>, one, or many items of type <typeparamref name="TValue"/> in an efficient way.
    /// </summary>
    public readonly partial struct ReadOnlyValueList<TValue> : IList<TValue>, IReadOnlyList<TValue>
    {
        /// <summary>
        /// A read-only instance of the <see cref="ReadOnlyValueList{TValue}"/> struct whose value is an empty <typeparamref name="TValue"/> array.
        /// </summary>
        /// <remarks>
        /// In application code, this field is most commonly used to safely represent a <see cref="ReadOnlyValueList{TValue}"/> that has no values.
        /// </remarks>
        public static readonly ReadOnlyValueList<TValue> Empty = new ReadOnlyValueList<TValue>(Array.Empty<TValue>());

        private readonly object? items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyValueList{TValue}"/> structure using the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">A value or <see langword="null"/></param>
        public ReadOnlyValueList(TValue item)
        {
            items = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyValueList{TValue}"/> structure that holds the specified <paramref name="items"/>.
        /// </summary>
        /// <param name="items">A <see cref="IList{TValue}"/> instance or <see langword="null"/>.</param>
        public ReadOnlyValueList(IList<TValue>? items)
        {
            this.items = items;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyValueList{TValue}"/> structure using the specified <typeparamref name="TValue"/> enumerable.
        /// </summary>
        /// <param name="items">A <typeparamref name="TValue"/> enumerable or <see langword="null"/>.</param>
        public ReadOnlyValueList(IEnumerable<TValue>? items)
        {
            (var value, var values) = ValueListUtil.GetOptimizedValues(items);
            this.items = values is null ? value : values;
        }

        bool ICollection<TValue>.IsReadOnly => true;

        /// <summary>
        /// Gets the number of <typeparamref name="TValue"/> elements contained in this <see cref="ReadOnlyValueList{TValue}" />.
        /// </summary>
        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                // Take local copy of values so type checks remain valid even if the ReadOnlyValueList is overwritten in memory
                var value = items;
                if (value is TValue)
                    return 1;

                if (value is null)
                    return 0;

                // Not TValue, not null, can only be IList<TValue>
                return Unsafe.As<IList<TValue>>(value).Count;
            }
        }

        /// <summary>
        /// Gets the <typeparamref name="TValue"/> at index.
        /// </summary>
        /// <value>The <typeparamref name="TValue"/> at the specified index.</value>
        /// <param name="index">The zero-based index of the element to get.</param>
        public TValue this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                // Take local copy of values so type checks remain valid even if the ReadOnlyValueList is overwritten in memory
                var value = items;
                if (value is TValue v)
                {
                    if (index == 0)
                        return v;
                }
                else if (value != null)
                {
                    // Not TValue, not null, can only be IList<TValue>
                    return Unsafe.As<IList<TValue>>(value)[index]; // may throw
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        /// <summary>
        /// Gets the <typeparamref name="TValue"/> at index.
        /// </summary>
        /// <value>The <typeparamref name="TValue"/> at the specified index.</value>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <exception cref="NotSupportedException">Set operations are not supported on read-only <see cref="ReadOnlyValueList{TValue}"/>.</exception>
        TValue IList<TValue>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException($"{nameof(ReadOnlyValueList<TValue>)} is read-only.");
        }

        /// <summary>
        /// Creates a <typeparamref name="TValue"/> array from the current <see cref="ReadOnlyValueList{TValue}"/> object.
        /// </summary>
        /// <returns>A <typeparamref name="TValue"/> array represented by this instance.</returns>
        /// <remarks>
        /// <para>If the <see cref="ReadOnlyValueList{TValue}"/> contains a single <typeparamref name="TValue"/> internally, it is copied to a new array.</para>
        /// <para>If the <see cref="ReadOnlyValueList{TValue}"/> contains an array internally it returns that array instance.</para>
        /// <para>Otherwise, it creates a new array and copies the values to it.</para>
        /// </remarks>
        public TValue[] ToArray()
        {
            // Take local copy of values so type checks remain valid even if the ReadOnlyValueList is overwritten in memory
            var value = items;
            if (value is IList<TValue> list)
                return list.AsArray();

            if (value is null)
                return Array.Empty<TValue>();

            // value not a list, can only be TValue
            return new[] { (TValue)value };
        }

        /// <summary>
        /// Returns the zero-based index of the first occurrence of an item in the <see cref="ReadOnlyValueList{TValue}" />.
        /// </summary>
        /// <param name="item">The <typeparamref name="TValue"/> to locate in the <see cref="ReadOnlyValueList{TValue}" />.</param>
        /// <returns>the zero-based index of the first occurrence of <paramref name="item" /> within the <see cref="ReadOnlyValueList{TValue}" />, if found; otherwise, –1.</returns>
        int IList<TValue>.IndexOf(TValue item)
            => IndexOf(item);

        private int IndexOf(TValue item)
        {
            // Take local copy of values so type checks remain valid even if the ReadOnlyValueList is overwritten in memory
            var value = items;
            if (value is IList<TValue> values)
                return values.IndexOf(item);

            if (value is null)
                return -1;

            // value not a list, can only be TValue
            return Equals((TValue)value, item) ? 0 : -1;
        }

        /// <summary>Determines whether a <typeparamref name="TValue"/> is in the <see cref="ReadOnlyValueList{TValue}" />.</summary>
        /// <param name="item">The <typeparamref name="TValue"/> to locate in the <see cref="ReadOnlyValueList{TValue}" />.</param>
        /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="ReadOnlyValueList{TValue}" />; otherwise, false.</returns>
        bool ICollection<TValue>.Contains(TValue item)
            => IndexOf(item) >= 0;

        /// <summary>
        /// Copies the entire <see cref="ReadOnlyValueList{TValue}" />to a <typeparamref name="TValue"/> array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:Array" /> that is the destination of the elements copied from. The <see cref="T:Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="ReadOnlyValueList{TValue}" /> is greater than the available space from <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.</exception>
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
            => CopyTo(array, arrayIndex);

        private void CopyTo(TValue[] array, int arrayIndex)
        {
            // Take local copy of values so type checks remain valid even if the ReadOnlyValueList is overwritten in memory
            var value = items;

            var ar = Contract.CheckValue(array, nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (ar.Length - arrayIndex < 0)
                throw new ArgumentException($"'{nameof(ar)}' is not long enough to copy all the items in the collection. Check '{nameof(arrayIndex)}' and '{nameof(array)}' length.");

            if (value is null)
                return;

            if (value is IList<TValue> vs)
            {
                vs.CopyTo(array, arrayIndex);
                return;
            }

            // value is not an array, can only be TValue
            ar[arrayIndex] = (TValue)value;
        }

        // This is only used for testing
        internal object? GetInner()
            => items;
    }
}
