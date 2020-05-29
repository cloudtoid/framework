namespace Cloudtoid
{
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class EmptyEnumerator<TValue> : IEnumerator<TValue>
    {
        private EmptyEnumerator()
        {
        }

        internal static IEnumerator<TValue> Instance { get; } = new EmptyEnumerator<TValue>();

        public TValue Current => throw new System.InvalidOperationException();

        object IEnumerator.Current => throw new System.InvalidOperationException();

        public void Dispose()
        {
        }

        public bool MoveNext() => false;

        public void Reset()
        {
        }
    }
}
