using System.Diagnostics;
using System.Net;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
        {
            var code = (int)statusCode;
            return code >= 200 && code <= 299;
        }
    }
}
