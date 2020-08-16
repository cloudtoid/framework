using System.Collections.Generic;

namespace Cloudtoid
{
    public readonly partial struct ReadOnlyValueList<TValue>
    {
        /// <summary>
        /// Determines whether two specified <see cref="ReadOnlyValueList{TValue}"/> have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ReadOnlyValueList<TValue> left, ReadOnlyValueList<TValue> right)
            => Equals(left, right);

        /// <summary>
        /// Determines whether two specified <see cref="ReadOnlyValueList{TValue}"/> have different values.
        /// </summary>
        /// <param name="left">The first <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ReadOnlyValueList<TValue> left, ReadOnlyValueList<TValue> right)
            => !Equals(left, right);

        /// <inheritdoc cref="Equals(ReadOnlyValueList{TValue}, TValue)" />
        public static bool operator ==(ReadOnlyValueList<TValue> left, TValue right)
            => Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <typeparamref name="TValue"/> objects have different values.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ReadOnlyValueList<TValue> left, TValue right)
            => !Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <inheritdoc cref="Equals(TValue, ReadOnlyValueList{TValue})" />
        public static bool operator ==(TValue left, ReadOnlyValueList<TValue> right)
            => Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> and <see cref="ReadOnlyValueList{TValue}"/> objects have different values.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> to compare.</param>
        /// <param name="right">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(TValue left, ReadOnlyValueList<TValue> right)
            => !Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <inheritdoc cref="Equals(ReadOnlyValueList{TValue}, IList{TValue})" />
        public static bool operator ==(ReadOnlyValueList<TValue> left, IList<TValue>? right)
            => Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <see cref="IList{TValue}"/> instances have different values.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="IList{TValue}"/> instance to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ReadOnlyValueList<TValue> left, IList<TValue> right)
            => !Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <inheritdoc cref="Equals(IList{TValue}, ReadOnlyValueList{TValue})" />
        public static bool operator ==(IList<TValue> left, ReadOnlyValueList<TValue> right)
            => Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="IList{TValue}"/> and <see cref="ReadOnlyValueList{TValue}"/> instances have different values.
        /// </summary>
        /// <param name="left">The <see cref="IList{TValue}"/> instance to compare.</param>
        /// <param name="right">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(IList<TValue> left, ReadOnlyValueList<TValue> right)
            => !Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <see cref="object"/>, which must be a
        /// <see cref="ReadOnlyValueList{TValue}"/>, <typeparamref name="TValue"/>, or <see cref="IList{TValue}"/>, have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ReadOnlyValueList<TValue> left, object right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <see cref="object"/>, which must be a
        /// <see cref="ReadOnlyValueList{TValue}"/>, <typeparamref name="TValue"/>, or <see cref="IList{TValue}"/>, have different values.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ReadOnlyValueList<TValue> left, object right)
            => !left.Equals(right);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, which must be a
        /// <see cref="ReadOnlyValueList{TValue}"/>, <typeparamref name="TValue"/>, or <see cref="IList{TValue}"/>, and specified <see cref="ReadOnlyValueList{TValue}"/>,  have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(object left, ReadOnlyValueList<TValue> right)
            => right.Equals(left);

        /// <summary>
        /// Determines whether the specified <see cref="object"/> and <see cref="ReadOnlyValueList{TValue}"/> object have the same value.
        /// </summary>
        /// <param name="left">The <see cref="object"/> to compare.</param>
        /// <param name="right">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(object left, ReadOnlyValueList<TValue> right)
            => !right.Equals(left);

        /// <summary>
        /// Determines whether two specified <see cref="ReadOnlyValueList{TValue}"/> objects have the same value in the same order.
        /// </summary>
        /// <param name="left">The first <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The second <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(ReadOnlyValueList<TValue> left, ReadOnlyValueList<TValue> right)
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
        /// Determines whether this instance and another specified <see cref="ReadOnlyValueList{TValue}"/> object have the same value.
        /// </summary>
        /// <param name="other">The <typeparamref name="TValue"/> to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as the value of this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(ReadOnlyValueList<TValue> other)
            => Equals(this, other);

        /// <summary>
        /// Determines whether the specified <typeparamref name="TValue"/> and <see cref="ReadOnlyValueList{TValue}"/> objects have the same value.
        /// </summary>
        /// <param name="left">The <typeparamref name="TValue"/> to compare.</param>
        /// <param name="right">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="left"/> is <c>null</c>, the method returns <c>false</c>.</returns>
        public static bool Equals(TValue left, ReadOnlyValueList<TValue> right)
            => Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <typeparamref name="TValue"/> objects have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <typeparamref name="TValue"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="right"/> is <c>null</c>, the method returns <c>false</c>.</returns>
        public static bool Equals(ReadOnlyValueList<TValue> left, TValue right)
            => Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <summary>
        /// Determines whether the specified <see cref="IList{TValue}"/> and <see cref="ReadOnlyValueList{TValue}"/> instances have the same value.
        /// </summary>
        /// <param name="left">The <see cref="IList{TValue}"/> instance to compare.</param>
        /// <param name="right">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(IList<TValue> left, ReadOnlyValueList<TValue> right)
            => Equals(new ReadOnlyValueList<TValue>(left), right);

        /// <summary>
        /// Determines whether the specified <see cref="ReadOnlyValueList{TValue}"/> and <see cref="IList{TValue}"/> instances have the same value.
        /// </summary>
        /// <param name="left">The <see cref="ReadOnlyValueList{TValue}"/> to compare.</param>
        /// <param name="right">The <see cref="IList{TValue}"/> instance to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool Equals(ReadOnlyValueList<TValue> left, IList<TValue>? right)
            => Equals(left, new ReadOnlyValueList<TValue>(right));

        /// <summary>
        /// Determines whether this instance and a specified <typeparamref name="TValue"/>, have the same value.
        /// </summary>
        /// <param name="other">The <typeparamref name="TValue"/> to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>. If <paramref name="other"/> is <c>null</c>, returns <c>false</c>.</returns>
        public bool Equals(TValue other) => Equals(this, new ReadOnlyValueList<TValue>(other));

        /// <summary>
        /// Determines whether this instance and <paramref name="other"/> have the same value.
        /// </summary>
        /// <param name="other">The <see cref="IList{TValue}"/> instance to compare to this instance.</param>
        /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(IList<TValue>? other)
            => Equals(this, new ReadOnlyValueList<TValue>(other));

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to <paramref name="obj"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj switch
            {
                ReadOnlyValueList<TValue> vl => Equals(this, vl),
                TValue v => Equals(this, v),
                IList<TValue> list => Equals(this, new ReadOnlyValueList<TValue>(list)),
                null => Equals(this, ReadOnlyValueList<TValue>.Empty),
                _ => false,
            };
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var value = items;
            if (value is null)
                return HashUtil.NullHashCode;

            if (value is IList<TValue> list)
                return HashUtil.Combine(list);

            return value.GetHashCode();
        }
    }
}
