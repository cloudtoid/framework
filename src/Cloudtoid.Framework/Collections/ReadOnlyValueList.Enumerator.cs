namespace Cloudtoid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public readonly partial struct ReadOnlyValueList<TValue>
    {
        /// <summary>Retrieves an object that can iterate through the individual <typeparamref name="TValue"/>s in this <see cref="ReadOnlyValueList{TValue}" />.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ReadOnlyValueList{TValue}" />.</returns>
        public IEnumerator<TValue> GetEnumerator()
            => new Enumerator(values);

        /// <inheritdoc cref="GetEnumerator()" />
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc cref="GetEnumerator()" />
        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Enumerates the <typeparamref name="TValue"/> values of a <see cref="ReadOnlyValueList{TValue}" />.
        /// </summary>
        private struct Enumerator : IEnumerator<TValue>
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
                var index = this.index;
                if (index < 0)
                    return false;

                var vs = values;
                if (vs != null)
                {
                    if ((uint)index < (uint)vs.Count)
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
            void IEnumerator.Reset()
                => throw new NotSupportedException();

            void IDisposable.Dispose()
            {
            }
        }
    }
}
