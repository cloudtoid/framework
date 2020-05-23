namespace Cloudtoid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents zero/<see langword="null"/>, one, or many items of type <typeparamref name="TValue"/> in an efficient way.
    /// </summary>
    public readonly struct ValueList<TValue> : IList<TValue>, IReadOnlyList<TValue>
    {
        /// <summary>
        /// A read-only instance of the <see cref="ValueList{TValue}"/> struct whose value is an empty <typeparamref name="TValue"/> array.
        /// </summary>
        /// <remarks>
        /// In application code, this field is most commonly used to safely represent a <see cref="ValueList{TValue}"/> that has no values.
        /// </remarks>
        public static readonly ValueList<TValue> Empty = new ValueList<TValue>(Array.Empty<TValue>());

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "Only used for testing")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:Non-private readonly fields should begin with upper-case letter", Justification = "Only used for testing")]
        internal readonly object? values;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueList{TValue}"/> structure using the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value or <see langword="null"/></param>
        public ValueList(TValue value)
        {
            values = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueList{TValue}"/> structure using the specified <typeparamref name="TValue"/> array.
        /// </summary>
        /// <param name="values">A <typeparamref name="TValue"/> array or <see langword="null"/>.</param>
        public ValueList(TValue[]? values)
        {
            this.values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueList{TValue}"/> structure using the specified <typeparamref name="TValue"/> enumerable.
        /// </summary>
        /// <param name="source">A <typeparamref name="TValue"/> enumerable or <see langword="null"/>.</param>
        public ValueList(IEnumerable<TValue>? source)
        {
            (var value, var values) = ValueListUtil.GetOptimizedValues(source);
            this.values = values is null ? value : (object)values;
        }

        /// <summary>
        /// Gets the number of <typeparamref name="TValue"/> elements contained in this <see cref="ValueList{TValue}" />.
        /// </summary>
        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                // Take local copy of values so type checks remain valid even if the ValueList is overwritten in memory
                var value = values;
                if (value is TValue)
                    return 1;

                if (value is null)
                    return 0;

                // Not TValue, not null, can only be TValue[]
                return Unsafe.As<TValue[]>(value).Length;
            }
        }

        bool ICollection<TValue>.IsReadOnly => true;

        /// <summary>
        /// Gets the <typeparamref name="TValue"/> at index.
        /// </summary>
        /// <value>The <typeparamref name="TValue"/> at the specified index.</value>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <exception cref="NotSupportedException">Set operations are not supported on read-only <see cref="ValueList{TValue}"/>.</exception>
        TValue IList<TValue>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
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
                // Take local copy of values so type checks remain valid even if the ValueList is overwritten in memory
                var value = values;
                if (value is TValue v)
                {
                    if (index == 0)
                        return v;
                }
                else if (value != null)
                {
                    // Not TValue, not null, can only be TValue[]
                    return Unsafe.As<TValue[]>(value)[index]; // may throw
                }

                return OutOfBounds(); // throws
            }
        }

        /// <summary>
        /// Determines whether two specified <see cref="ValueList{TValue}"/> have the same values.
        /// </summary>
        /// <param name="left">The first <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ValueList<TValue> left, ValueList<TValue> right) => Equals(left, right);

        /// <summary>
        /// Determines whether two specified <see cref="ValueList{TValue}"/> have different values.
        /// </summary>
        /// <param name="left">The first <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ValueList<TValue> left, ValueList<TValue> right) => !Equals(left, right);

        /// <inheritdoc cref="Equals(ValueList{TValue}, TValue)" />
        public static bool operator ==(ValueList<TValue> left, TValue right) => Equals(left, new ValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <typeparamref name="TValue"/> objects have different values.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ValueList<TValue> left, TValue right) => !Equals(left, new ValueList<TValue>(right));

        /// <inheritdoc cref="Equals(TValue, ValueList{TValue})" />
        public static bool operator ==(TValue left, ValueList<TValue> right) => Equals(new ValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> and <see cref="ValueList{TValue}"/> objects have different values.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> to compare.</param>
        /// <param name="right">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(TValue left, ValueList<TValue> right) => !Equals(new ValueList<TValue>(left), right);

        /// <inheritdoc cref="Equals(ValueList{TValue}, TValue[])" />
        public static bool operator ==(ValueList<TValue> left, TValue[]? right) => Equals(left, new ValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <typeparamref name="TValue"/> array have different values.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> array to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ValueList<TValue> left, TValue[] right) => !Equals(left, new ValueList<TValue>(right));

        /// <inheritdoc cref="Equals(TValue[], ValueList{TValue})" />
        public static bool operator ==(TValue[] left, ValueList<TValue> right) => Equals(new ValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> array and <see cref="ValueList{TValue}"/> have different values.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> array to compare.</param>
        /// <param name="right">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(TValue[] left, ValueList<TValue> right) => !Equals(new ValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <see cref="object"/>, which must be a
        /// <see cref="ValueList{TValue}"/>, <typeparamref name="TValue"/>, or array of <typeparamref name="TValue"/>, have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ValueList<TValue> left, object right) => left.Equals(right);

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <see cref="object"/>, which must be a
        /// <see cref="ValueList{TValue}"/>, <typeparamref name="TValue"/>, or array of <typeparamref name="TValue"/>, have different values.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ValueList<TValue> left, object right) => !left.Equals(right);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, which must be a
        /// <see cref="ValueList{TValue}"/>, <typeparamref name="TValue"/>, or array of <typeparamref name="TValue"/>, and specified <see cref="ValueList{TValue}"/>,  have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(object left, ValueList<TValue> right) => right.Equals(left);

        /// <summary>
        /// Determines whether the specified <see cref="object"/> and <see cref="ValueList{TValue}"/> object have the same values.
        /// </summary>
        /// <param name="left">The <see cref="object"/> to compare.</param>
        /// <param name="right">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(object left, ValueList<TValue> right) => !right.Equals(left);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static TValue OutOfBounds() => Array.Empty<TValue>()[0]; // throws

        /// <summary>
        /// Creates a <typeparamref name="TValue"/> array from the current <see cref="ValueList{TValue}"/> object.
        /// </summary>
        /// <returns>A <typeparamref name="TValue"/> array represented by this instance.</returns>
        /// <remarks>
        /// <para>If the <see cref="ValueList{TValue}"/> contains a single <typeparamref name="TValue"/> internally, it is copied to a new array.</para>
        /// <para>If the <see cref="ValueList{TValue}"/> contains an array internally it returns that array instance.</para>
        /// </remarks>
        public TValue[] ToArray()
        {
            // Take local copy of values so type checks remain valid even if the ValueList is overwritten in memory
            var value = this.values;
            if (value is TValue[] values)
                return values;

            if (value is null)
                return Array.Empty<TValue>();

            // value not array, can only be TValue
            return new[] { (TValue)value };
        }

        /// <summary>
        /// Returns the zero-based index of the first occurrence of an item in the <see cref="ValueList{TValue}" />.
        /// </summary>
        /// <param name="item">The <typeparamref name="TValue"/> to locate in the <see cref="ValueList{TValue}" />.</param>
        /// <returns>the zero-based index of the first occurrence of <paramref name="item" /> within the <see cref="ValueList{TValue}" />, if found; otherwise, –1.</returns>
        int IList<TValue>.IndexOf(TValue item) => IndexOf(item);

        private int IndexOf(TValue item)
        {
            // Take local copy of values so type checks remain valid even if the ValueList is overwritten in memory
            var value = this.values;
            if (value is TValue[] values)
                return Array.IndexOf(values, item);

            if (value is null)
                return -1;

            // value not array, can only be TValue
            return Equals((TValue)value, item) ? 0 : -1;
        }

        /// <summary>Determines whether a <typeparamref name="TValue"/> is in the <see cref="ValueList{TValue}" />.</summary>
        /// <param name="item">The <typeparamref name="TValue"/> to locate in the <see cref="ValueList{TValue}" />.</param>
        /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="ValueList{TValue}" />; otherwise, false.</returns>
        bool ICollection<TValue>.Contains(TValue item) => IndexOf(item) >= 0;

        /// <summary>
        /// Copies the entire <see cref="ValueList{TValue}" />to a <typeparamref name="TValue"/> array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="ValueList{TValue}" /> is greater than the available space from <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.</exception>
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex) => CopyTo(array, arrayIndex);

        private void CopyTo(TValue[] array, int arrayIndex)
        {
            // Take local copy of values so type checks remain valid even if the ValueList is overwritten in memory
            var value = values;

            var ar = Contract.CheckValue(array, nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (ar.Length - arrayIndex < 0)
                throw new ArgumentException($"'{nameof(ar)}' is not long enough to copy all the items in the collection. Check '{nameof(arrayIndex)}' and '{nameof(array)}' length.");

            if (value is null)
                return;

            if (value is TValue[] vs)
            {
                Array.Copy(vs, 0, array, arrayIndex, vs.Length);
                return;
            }

            // value not array, can only be TValue
            ar[arrayIndex] = (TValue)value;
        }

        void ICollection<TValue>.Add(TValue item) => throw new NotSupportedException();

        void IList<TValue>.Insert(int index, TValue item) => throw new NotSupportedException();

        bool ICollection<TValue>.Remove(TValue item) => throw new NotSupportedException();

        void IList<TValue>.RemoveAt(int index) => throw new NotSupportedException();

        void ICollection<TValue>.Clear() => throw new NotSupportedException();

        /// <summary>Retrieves an object that can iterate through the individual <typeparamref name="TValue"/>s in this <see cref="ValueList{TValue}" />.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ValueList{TValue}" />.</returns>
        public IEnumerator<TValue> GetEnumerator() => new Enumerator(values);

        /// <inheritdoc cref="GetEnumerator()" />
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="GetEnumerator()" />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Determines whether two specified <see cref="ValueList{TValue}"/> objects have the same values in the same order.
        /// </summary>
        /// <param name="left">The first <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(ValueList<TValue> left, ValueList<TValue> right)
        {
            var count = left.Count;
            if (count != right.Count)
                return false;

            for (var i = 0; i < count; i++)
            {
                if (!Equals(left[i], right[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance and another specified <see cref="ValueList{TValue}"/> object have the same values.
        /// </summary>
        /// <param name="other">The <typeparamref name="TValue"/> to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as the value of this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(ValueList<TValue> other) => Equals(this, other);

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> and <see cref="ValueList{TValue}"/> objects have the same values.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> to compare.</param>
        /// <param name="right">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="left"/> is <c>null</c>, the method returns <c>false</c>.</returns>
        public static bool Equals(TValue left, ValueList<TValue> right) => Equals(new ValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <typeparamref name="TValue"/> objects have the same values.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="right"/> is <c>null</c>, the method returns <c>false</c>.</returns>
        public static bool Equals(ValueList<TValue> left, TValue right) => Equals(left, new ValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> array and <see cref="ValueList{TValue}"/> objects have the same values.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> array to compare.</param>
        /// <param name="right">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(TValue[] left, ValueList<TValue> right) => Equals(new ValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ValueList{TValue}"/> and <typeparamref name="TValue"/> array objects have the same values.
        /// </summary>
        /// <param name="left">The <see cref="ValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> array to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(ValueList<TValue> left, TValue[]? right) => Equals(left, new ValueList<TValue>(right));

        /// <summary>
        /// Determines whether this instance and a specified <typeparamref name="TValue"/>, have the same value.
        /// </summary>
        /// <param name="other">The <typeparamref name="TValue"/> to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>. If <paramref name="other"/> is <c>null</c>, returns <c>false</c>.</returns>
        public bool Equals(TValue other) => Equals(this, new ValueList<TValue>(other));

        /// <summary>
        /// Determines whether this instance and a specified <typeparamref name="TValue"/> array have the same values.
        /// </summary>
        /// <param name="other">The <typeparamref name="TValue"/> array to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(TValue[]? other) => Equals(this, new ValueList<TValue>(other));

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to <paramref name="obj"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj switch
            {
                ValueList<TValue> vl => Equals(this, vl),
                TValue v => Equals(this, v),
                TValue[] vs => Equals(this, new ValueList<TValue>(vs)),
                null => Equals(this, ValueList<TValue>.Empty),
                _ => false,
            };
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var value = this.values;
            if (value is null)
                return HashUtil.NullHashCode;

            if (value is TValue[] values)
                return HashUtil.Combine(values);

            return value.GetHashCode();
        }

        /// <summary>
        /// Enumerates the <typeparamref name="TValue"/> values of a <see cref="ValueList{TValue}" />.
        /// </summary>
        private struct Enumerator : IEnumerator<TValue>
        {
            private readonly TValue[]? values;
            private int index;

            internal Enumerator(object? value)
            {
                if (value is TValue v)
                {
                    values = null;
                    Current = v;
                }
                else
                {
                    Current = default!;
                    values = Unsafe.As<TValue[]>(value);
                }

                index = 0;
            }

            public TValue Current { get; private set; }

            [ExcludeFromCodeCoverage]
            object? IEnumerator.Current => Current;

            public bool MoveNext()
            {
                var index = this.index;
                if (index < 0)
                    return false;

                var vs = values;
                if (vs != null)
                {
                    if ((uint)index < (uint)vs.Length)
                    {
                        this.index = index + 1;
                        Current = vs[index];
                        return true;
                    }

                    this.index = -1;
                    return false;
                }

                this.index = -1;
                return Current != null;
            }

            [ExcludeFromCodeCoverage]
            void IEnumerator.Reset() => throw new NotSupportedException();

            void IDisposable.Dispose()
            {
            }
        }
    }
}
