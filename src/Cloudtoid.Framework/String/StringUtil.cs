namespace Cloudtoid
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using static Contract;

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

        /// <summary>
        /// Determines whether the specified string is a CLS-compliant identifier.
        /// </summary>
        /// <param name="s">A string which may be a CLS-compliant identifier.</param>
        /// <returns>Returns <c>true</c> if the specified string is a CLS compliant identifier; otherwise, <c>false</c>.</returns>
        public static bool IsClsCompliantIdentifier(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            if (!IsClsCompliantIdentifierChar(s![0], true))
                return false;

            for (var i = 1; i < s.Length; i++)
            {
                if (!IsClsCompliantIdentifierChar(s[i], false))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified character is a valid CLS identifier character.
        /// </summary>
        /// <param name="c">A character value.</param>
        /// <param name="firstChar">Indicates whether the character is the first character in an identifier. Different rules govern CLS-compliance for the initial character in an identifier.</param>
        /// <returns>Returns <c>true</c> if the specified character is a CLS-compliant identifier character; otherwise, <c>false</c>.</returns>
        public static bool IsClsCompliantIdentifierChar(char c, bool firstChar)
        {
            // CLS-compliant language compilers must follow the rules of Annex 7 of Technical Report 15 of the
            // Unicode Standard 3.0, which governs the set of characters that can start and be included in identifiers.
            // This standard is available at http://www.unicode.org/unicode/reports/tr15/tr15-18.html.
            // <identifier> ::= <identifier_start> ( <identifier_start> | <identifier_extend> )*
            switch (CharUnicodeInfo.GetUnicodeCategory(c))
            {
                // These characters can occur in any position
                // <identifier_start> ::= [{Lu}{Ll}{Lt}{Lm}{Lo}{Nl}]
                case UnicodeCategory.UppercaseLetter: // Lu
                case UnicodeCategory.LowercaseLetter: // Ll
                case UnicodeCategory.TitlecaseLetter: // Lt
                case UnicodeCategory.ModifierLetter: // Lm
                case UnicodeCategory.OtherLetter: // Lo
                case UnicodeCategory.LetterNumber: // Nl
                    return true;

                // These characters can occur in any position but the first
                // <identifier_extend> ::= [{Mn}{Mc}{Nd}{Pc}{Cf}]
                case UnicodeCategory.NonSpacingMark: // Mn
                case UnicodeCategory.SpacingCombiningMark: // Mc
                case UnicodeCategory.DecimalDigitNumber: // Nd
                case UnicodeCategory.ConnectorPunctuation: // Pc
                case UnicodeCategory.Format: // Cf
                    return !firstChar;

                // Other characters are invalid everywhere
                case UnicodeCategory.ClosePunctuation:
                case UnicodeCategory.Control:
                case UnicodeCategory.CurrencySymbol:
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.EnclosingMark:
                case UnicodeCategory.FinalQuotePunctuation:
                case UnicodeCategory.InitialQuotePunctuation:
                case UnicodeCategory.LineSeparator:
                case UnicodeCategory.MathSymbol:
                case UnicodeCategory.ModifierSymbol:
                case UnicodeCategory.OpenPunctuation:
                case UnicodeCategory.OtherNotAssigned:
                case UnicodeCategory.OtherNumber:
                case UnicodeCategory.OtherPunctuation:
                case UnicodeCategory.OtherSymbol:
                case UnicodeCategory.ParagraphSeparator:
                case UnicodeCategory.PrivateUse:
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.Surrogate:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Derives a CLS-compliant name from the specified string.
        /// </summary>
        /// <param name="s">Input string which may include any characters.</param>
        /// <param name="fallbackName">CLS-compliant fall-back name to use if no allowed characters present in <paramref name="s"/>.</param>
        public static string DeriveClsCompliantName(string s, string fallbackName)
        {
            CheckNonEmpty(s, nameof(s));
            CheckNonEmpty(fallbackName, nameof(fallbackName));
            Check(IsClsCompliantIdentifier(fallbackName), "{0} must be CLS compliant", nameof(fallbackName));

            var builder = new StringBuilder(s.Length);

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (builder.Length > 0 && (char.IsSeparator(c) || c == '.' || c == '/'))
                {
                    builder.Append('_');
                }
                else if (IsClsCompliantIdentifierChar(c, builder.Length == 0))
                {
                    builder.Append(c);
                }
                else if (builder.Length == 0 && IsClsCompliantIdentifierChar(c, firstChar: false))
                {
                    // Prefix with fallbackName if first char would be valid as non-first char
                    builder.Append(fallbackName);
                    builder.Append(c);
                }
            }

            return builder.Length > 0 ? builder.ToString() : fallbackName;
        }

        /// <summary>
        /// Makes a unique name based on the <paramref name="candidateName"/> by appending a numeric suffix if necessary.
        /// </summary>
        /// <param name="candidateName">Candidate name to use.</param>
        /// <param name="namesInUse">List of all the names that are used now.</param>
        /// <returns>Candidate name or a derived name which is not currently in use.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MakeUniqueName(string candidateName, ISet<string> namesInUse) => MakeUniqueName(candidateName, CheckValue(namesInUse, nameof(namesInUse)).Contains);

        /// <summary>
        /// Makes a unique name based on the <paramref name="candidateName"/> by appending a numeric suffix if necessary.
        /// </summary>
        /// <param name="candidateName">Candidate name to use.</param>
        /// <param name="isUsedPredicate">A predicate indicating if the name is in use.</param>
        /// <returns>Candidate name or a derived name which is not currently in use.</returns>
        public static string MakeUniqueName(string candidateName, Func<string, bool> isUsedPredicate)
        {
            CheckValue(isUsedPredicate, nameof(isUsedPredicate));

            var baseName = candidateName;
            var i = 1;
            while (isUsedPredicate(candidateName))
            {
                candidateName = string.Concat(baseName, i.ToStringInvariant());
                i++;
            }

            return candidateName;
        }

        public static string ToCommaDelimitedText<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            string? pairsDelimiter = null,
            string? keyValueDelimiter = null) where TKey : notnull
        {
            CheckValue(dictionary, nameof(dictionary));

            var builder = new StringBuilder();
            foreach (var pair in dictionary)
            {
                if (builder.Length > 0)
                {
                    if (pairsDelimiter is null)
                    {
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.Append(pairsDelimiter);
                    }
                }

                builder.Append(pair.Key);
                builder.Append(keyValueDelimiter ?? ", ");
                builder.Append(pair.Value);
            }

            return builder.ToString();
        }

        public static string Trim(this string text, ref int startIndex, ref int endIndex)
        {
            CheckValue(text, nameof(text));
            Check(startIndex >= 0 && startIndex <= endIndex && endIndex < text.Length, "Invalid startIndex and/or endIndex");

            // trim left spaces
            while (startIndex < endIndex && char.IsWhiteSpace(text[startIndex]))
                startIndex++;

            // trim right spaces
            while (endIndex >= startIndex && char.IsWhiteSpace(text[endIndex]))
                endIndex--;

            return text.Substring(startIndex, endIndex - startIndex + 1);
        }
    }
}
