using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cloudtoid.Framework
{
    [DebuggerStepThrough]
    public static class GuidExtensions
    {
        private static readonly char[] Base64UrlChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".ToCharArray();
        private static readonly char[] Base41UrlChars = "abcdefghijklmnopqrstuvwxyz0123456789-_.!~".ToCharArray();

        /// <summary>
        /// <a href="https://base64.guru/standards/base64url">Base64url</a> encodes <paramref name="value"/>.
        /// The length of the result is always 22 characters consisting of these characters: [A..Za..z0..9-_].
        /// This case-sensitive encoded value can be used in Query Parameters and HTTP Headers, but for the most parts,
        /// ULR paths are case-insensitive. Use <see cref="Base41UrlChars"/> instead.
        /// </summary>
        public static string Base64UrlEncode(this Guid value)
        {
            Span<byte> bytes = stackalloc byte[16];
            value.TryWriteBytes(bytes);
            Span<char> str = stackalloc char[22];

            str[0] = Base64UrlChars[bytes[0] >> 2];
            str[1] = Base64UrlChars[((bytes[0] & 0b_0000_0011) << 4) | (bytes[1] >> 4)];
            str[2] = Base64UrlChars[((bytes[1] & 0b_0000_1111) << 2) | (bytes[2] >> 6)];
            str[3] = Base64UrlChars[bytes[2] & 0b_0011_1111];

            str[4] = Base64UrlChars[bytes[3] >> 2];
            str[5] = Base64UrlChars[((bytes[3] & 0b_0000_0011) << 4) | (bytes[4] >> 4)];
            str[6] = Base64UrlChars[((bytes[4] & 0b_0000_1111) << 2) | (bytes[5] >> 6)];
            str[7] = Base64UrlChars[bytes[5] & 0b_0011_1111];

            str[8] = Base64UrlChars[bytes[6] >> 2];
            str[9] = Base64UrlChars[((bytes[6] & 0b_0000_0011) << 4) | (bytes[7] >> 4)];
            str[10] = Base64UrlChars[((bytes[7] & 0b_0000_1111) << 2) | (bytes[8] >> 6)];
            str[11] = Base64UrlChars[bytes[8] & 0b_0011_1111];

            str[12] = Base64UrlChars[bytes[9] >> 2];
            str[13] = Base64UrlChars[((bytes[9] & 0b_0000_0011) << 4) | (bytes[10] >> 4)];
            str[14] = Base64UrlChars[((bytes[10] & 0b_0000_1111) << 2) | (bytes[11] >> 6)];
            str[15] = Base64UrlChars[bytes[11] & 0b_0011_1111];

            str[16] = Base64UrlChars[bytes[12] >> 2];
            str[17] = Base64UrlChars[((bytes[12] & 0b_0000_0011) << 4) | (bytes[13] >> 4)];
            str[18] = Base64UrlChars[((bytes[13] & 0b_0000_1111) << 2) | (bytes[14] >> 6)];
            str[19] = Base64UrlChars[bytes[14] & 0b_0011_1111];

            str[20] = Base64UrlChars[bytes[15] >> 2];
            str[21] = Base64UrlChars[bytes[15] & 0b_0000_0011];

            return new string(str);
        }

        /// <summary>
        /// Encodes <paramref name="value"/> to base 41. The length of the result is 24 characters consisting of these characters: [a..z0..9-_].
        /// This case-insensitive encoded value can be used in URL Path as well as Query Parameters and HTTP Headers.
        /// </summary>
        public static string Base41UrlEncode(this Guid value)
        {
            Span<byte> bytes = stackalloc byte[16];
            value.TryWriteBytes(bytes);
            Span<ulong> ulongs = MemoryMarshal.Cast<byte, ulong>(bytes);
            Span<char> str = stackalloc char[24];

            var ul = ulongs[0];
            for (int i = 0; i < 12; i++)
            {
                str[i] = Base41UrlChars[(int)(ul % 41)];
                ul /= 41;
            }

            ul = ulongs[1];
            for (int i = 12; i < 24; i++)
            {
                str[i] = Base41UrlChars[(int)(ul % 41)];
                ul /= 41;
            }

            return new string(str);
        }
    }
}
