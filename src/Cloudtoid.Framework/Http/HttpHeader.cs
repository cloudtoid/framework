using System.Diagnostics;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    public static class HttpHeader
    {
        private static readonly bool[] TokenChars = CreateTokenChars();

        private static bool[] CreateTokenChars()
        {
            // token = 1*<any CHAR except CTLs or separators>
            // CTL = <any US-ASCII control character (octets 0 - 31) and DEL (127)>

            var tokenChars = new bool[128];

            // Skip Space (32) & DEL (127).
            for (var i = 33; i < 127; i++)
                tokenChars[i] = true;

            // Remove separators: these are not valid token characters.
            tokenChars['('] = false;
            tokenChars[')'] = false;
            tokenChars['<'] = false;
            tokenChars['>'] = false;
            tokenChars['@'] = false;
            tokenChars[','] = false;
            tokenChars[';'] = false;
            tokenChars[':'] = false;
            tokenChars['\\'] = false;
            tokenChars['"'] = false;
            tokenChars['/'] = false;
            tokenChars['['] = false;
            tokenChars[']'] = false;
            tokenChars['?'] = false;
            tokenChars['='] = false;
            tokenChars['{'] = false;
            tokenChars['}'] = false;
            return tokenChars;
        }

        // Must be between 'space' (32) and 'DEL' (127).
        private static bool IsValidNameChar(char character)
            => character <= 127 && TokenChars[character];

        public static bool IsValidName(string name)
        {
            CheckValue(name, nameof(name));

            for (var i = 0; i < name.Length; i++)
            {
                if (!IsValidNameChar(name[i]))
                    return false;
            }

            return true;
        }
    }
}
