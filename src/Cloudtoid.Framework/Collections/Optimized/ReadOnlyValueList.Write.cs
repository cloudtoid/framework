namespace Cloudtoid
{
    using System;
    using System.Collections.Generic;

    public readonly partial struct ReadOnlyValueList<TValue>
    {
        // This is a read only value

        void ICollection<TValue>.Add(TValue item)
            => throw new NotSupportedException();

        void IList<TValue>.Insert(int index, TValue item)
            => throw new NotSupportedException();

        bool ICollection<TValue>.Remove(TValue item)
            => throw new NotSupportedException();

        void IList<TValue>.RemoveAt(int index)
            => throw new NotSupportedException();

        void ICollection<TValue>.Clear()
            => throw new NotSupportedException();
    }
}
