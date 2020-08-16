using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Cloudtoid
{
    [SuppressMessage("BannedApiAnalyzer", "RS0030", Justification = "Instead of the banned APIs, we should use the following extension methods.")]
    [DebuggerStepThrough]
    public static class ToStringExtensions
    {
        private const string DefaultTimeSpanFormat = "c";
        private const string DefaultGuidFormat = "D";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this byte value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this byte value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this short value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this short value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this ushort value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this ushort value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this int value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this int value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this uint value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this uint value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this long value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this long value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this ulong value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this ulong value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this float value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this float value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this double value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this double value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this decimal value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this decimal value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this DateTime value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this DateTime value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this DateTimeOffset value) => value.ToString(CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this DateTimeOffset value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this TimeSpan value) => value.ToString(DefaultTimeSpanFormat, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this TimeSpan value, string format) => value.ToString(format, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this Guid value) => value.ToString(DefaultGuidFormat, CultureInfo.InvariantCulture);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringInvariant(this Guid value, string format) => value.ToString(format, CultureInfo.InvariantCulture);
    }
}
