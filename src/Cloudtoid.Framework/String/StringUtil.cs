using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    [SuppressMessage("BannedApiAnalyzer", "RS0030", Justification = "Reviewed.")]
    [DebuggerStepThrough]
    public static class StringUtil
    {
        /// <summary>
        /// Formats a string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format">Format string with a single argument.</param>
        /// <param name="arg">Format argument.</param>
        /// <returns>A string formatted using the <see cref="CultureInfo.InvariantCulture"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatInvariant<TArg>(this string format, TArg arg) => string.Format(CultureInfo.InvariantCulture, format, new object?[] { arg });

        /// <summary>
        /// Formats a string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format">Format string with 2 arguments.</param>
        /// <param name="arg0">First format argument.</param>
        /// <param name="arg1">Second format argument.</param>
        /// <returns>A string formatted using the <see cref="CultureInfo.InvariantCulture"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatInvariant<TArg0, TArg1>(this string format, TArg0 arg0, TArg1 arg1) => string.Format(CultureInfo.InvariantCulture, format, arg0, arg1);

        /// <summary>
        /// Formats a string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format">Format string with 2 arguments.</param>
        /// <param name="arg0">First format argument.</param>
        /// <param name="arg1">Second format argument.</param>
        /// <param name="arg2">Third format argument.</param>
        /// <returns>A string formatted using the <see cref="CultureInfo.InvariantCulture"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatInvariant<TArg0, TArg1, TArg2>(this string format, TArg0 arg0, TArg1 arg1, TArg2 arg2) => string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2);

        /// <summary>
        /// Formats a string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>A string formatted using the <see cref="CultureInfo.InvariantCulture"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatInvariant(this string format, params object?[] args) => string.Format(CultureInfo.InvariantCulture, format, args);

        /// <summary>
        /// Determines whether two specified String objects have the same value using the <see cref="StringComparison.OrdinalIgnoreCase"/> comparison.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsOrdinalIgnoreCase(this string? arg0, string? arg1) => string.Equals(arg0, arg1, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether two specified String objects have the same value using the <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsOrdinal(this string? arg0, string? arg1) => string.Equals(arg0, arg1, StringComparison.Ordinal);

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string when compared using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to compare.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StartsWithOrdinalIgnoreCase(this string str, string value) => CheckValue(str, nameof(str)).StartsWith(value, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string when compared using <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to compare.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StartsWithOrdinal(this string str, string value) => CheckValue(str, nameof(str)).StartsWith(value, StringComparison.Ordinal);

        /// <summary>
        /// Determines whether the end of this string instance matches the specified string when compared using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to compare.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EndsWithOrdinalIgnoreCase(this string str, string value) => CheckValue(str, nameof(str)).EndsWith(value, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the end of this string instance matches the specified string when compared using <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to compare.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EndsWithOrdinal(this string str, string value) => CheckValue(str, nameof(str)).EndsWith(value, StringComparison.Ordinal);

        /// <summary>
        /// Determines if the search string is contained in original string using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsOrdinalIgnoreCase(this string str, string value) => CheckValue(str, nameof(str)).IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1;

        /// <summary>
        /// Determines if the search string is contained in original string using <see cref="StringComparison.Ordinal"/> comparison
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsOrdinal(this string str, string value) => CheckValue(str, nameof(str)).IndexOf(value, StringComparison.Ordinal) != -1;

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="string"/> object. The search uses <see cref="StringComparison.OrdinalIgnoreCase"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to seek.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOrdinalIgnoreCase(this string str, string value) => CheckValue(str, nameof(str)).IndexOf(value, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="string"/> object. The search uses <see cref="StringComparison.OrdinalIgnoreCase"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOrdinalIgnoreCase(this string str, string value, int startIndex) => CheckValue(str, nameof(str)).IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="string"/> object. The search uses <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to seek.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOrdinal(this string str, string value) => CheckValue(str, nameof(str)).IndexOf(value, StringComparison.Ordinal);

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified char in the current <see cref="string"/> object. The search uses <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The char to seek.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOrdinal(this string str, char value) => CheckValue(str, nameof(str)).IndexOf(value, StringComparison.Ordinal);

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="string"/> object. The search uses <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOrdinal(this string str, string value, int startIndex) => CheckValue(str, nameof(str)).IndexOf(value, startIndex, StringComparison.Ordinal);

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string. The search uses <see cref="StringComparison.Ordinal"/> comparison.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReplaceOrdinal(this string str, string oldValue, string newValue) => CheckValue(str, nameof(str)).Replace(oldValue, newValue, StringComparison.Ordinal);

        /// <summary>
        /// Returns the hash code for this string using <see cref="StringComparison.InvariantCulture"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCodeInvariant(this string str) => CheckValue(str, nameof(str)).GetHashCode(StringComparison.InvariantCulture);
    }
}
