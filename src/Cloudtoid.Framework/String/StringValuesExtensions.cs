namespace Cloudtoid
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.Extensions.Primitives;

    [DebuggerStepThrough]
    public static class StringValuesExtensions
    {
        /// <summary>
        /// Creates an instance of <see cref="StringValues"/> from <see cref="IEnumerable{String}"/>.
        /// <list type="bullet">
        /// <item>If the source is <see langword="null"/>, it returns an empty <see cref="StringValues"/>.</item>
        /// <item>If the source is a <see cref="string"/> array, then the array is wrapped inside of an instance of <see cref="StringValues"/>.</item>
        /// <item>If the source contains a single instance of <see cref="string"/>, then it is wrapped inside of an instance of <see cref="StringValues"/> saving on a creation of an array instance.</item>
        /// <item>In all other cases, it behaves similar to <see cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/> wrapped in an instance of <see cref="StringValues"/>.</item>
        /// </list>
        /// </summary>
        public static StringValues AsStringValues(this IEnumerable<string>? source)
        {
            if (source is StringValues sv)
                return sv;

            (var value, var values) = ValueListUtil.GetOptimizedValues(source);
            if (value != null)
                return new StringValues(value);

            if (values != null)
                return new StringValues(values.AsArray());

            return StringValues.Empty;
        }
    }
}