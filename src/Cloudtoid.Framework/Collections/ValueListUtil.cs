using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloudtoid
{
    internal static class ValueListUtil
    {
        /// <summary>
        /// Gets an optimized value from <see cref="IEnumerable{TValue}"/>.
        /// <list type="bullet">
        /// <item>If the source is <see langword="null"/>, it returns <see langword="null"/>.</item>
        /// <item>If the source is a <see cref="IList{TValue}"/>, then the same exact instance of the list is returned.</item>
        /// <item>If the source has a single instance of <typeparamref name="TValue"/>, then the same exact instance of <typeparamref name="TValue"/> is returned.</item>
        /// <item>In all other cases, it behaves similar to <see cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/>.</item>
        /// </list>
        /// </summary>
        internal static (TValue Value, IList<TValue>? Values) GetOptimizedValues<TValue>(IEnumerable<TValue>? source)
        {
            switch (source)
            {
                case IList<TValue> list:
                    return (default, list);

                case null:
                    return (default, default);

                default:
                    break;
            }

            using (var en = source.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    var first = en.Current;
                    if (!en.MoveNext())
                        return (first, default);

                    const int DefaultCapacity = 4;
                    var arr = new TValue[DefaultCapacity];
                    arr[0] = first;
                    arr[1] = en.Current;
                    var count = 2;

                    while (en.MoveNext())
                    {
                        if (count == arr.Length)
                        {
                            // MaxArrayLength is defined in Array.MaxArrayLength and in gchelpers in CoreCLR.
                            // It represents the maximum number of elements that can be in an array where
                            // the size of the element is greater than one byte; a separate, slightly larger constant,
                            // is used when the size of the element is one.
                            const int MaxArrayLength = 0x7FEFFFFF;

                            // This is the same growth logic as in List<T>:
                            // If the array is currently empty, we make it a default size.  Otherwise, we attempt to
                            // double the size of the array.  Doubling will overflow once the size of the array reaches
                            // 2^30, since doubling to 2^31 is 1 larger than Int32.MaxValue.  In that case, we instead
                            // constrain the length to be MaxArrayLength (this overflow check works because of the
                            // cast to uint).  Because a slightly larger constant is used when T is one byte in size, we
                            // could then end up in a situation where arr.Length is MaxArrayLength or slightly larger, such
                            // that we constrain newLength to be MaxArrayLength but the needed number of elements is actually
                            // larger than that.  For that case, we then ensure that the newLength is large enough to hold
                            // the desired capacity.  This does mean that in the very rare case where we've grown to such a
                            // large size, each new element added after MaxArrayLength will end up doing a resize.
                            var newLength = count << 1;
                            if ((uint)newLength > MaxArrayLength)
                                newLength = count >= MaxArrayLength ? count + 1 : MaxArrayLength;

                            Array.Resize(ref arr, newLength);
                        }

                        arr[count++] = en.Current;
                    }

                    Array.Resize(ref arr, count);
                    return (default, arr);
                }
            }

            return default;
        }
    }
}