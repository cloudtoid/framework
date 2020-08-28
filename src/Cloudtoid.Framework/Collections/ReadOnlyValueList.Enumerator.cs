using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cloudtoid
{
    public readonly partial struct ReadOnlyValueList<TValue>
    {
        /// <summary>Retrieves an object that can iterate through the individual <typeparamref name="TValue"/>s in this <see cref="ReadOnlyValueList{TValue}" />.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ReadOnlyValueList{TValue}" />.</returns>
        public Enumerator GetEnumerator()
            => new Enumerator(items);

        /// <inheritdoc cref="GetEnumerator()" />
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            => new Enumerator(items);

        /// <inheritdoc cref="GetEnumerator()" />
        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(items);

        /// <summary>
        /// Enumerates the <typeparamref name="TValue"/> values of a <see cref="ReadOnlyValueList{TValue}" />.
        /// </summary>
        public struct Enumerator : IEnumerator<TValue>
        {
            private readonly IList<TValue>? values;
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
                    values = Unsafe.As<IList<TValue>>(value);
                }

                index = 0;
            }

            public TValue Current { get; private set; }

            [ExcludeFromCodeCoverage]
            object? IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (index < 0)
                    return false;

                if (values is null)
                {
                    index = -1;
                    return Current != null;
                }

                if (index < values.Count)
                {
                    Current = values[index++];
                    return true;
                }

                index = -1;
                return false;
            }

            [ExcludeFromCodeCoverage]
            void IEnumerator.Reset()
                => throw new NotSupportedException();

            void IDisposable.Dispose()
            {
            }
        }
    }
}
