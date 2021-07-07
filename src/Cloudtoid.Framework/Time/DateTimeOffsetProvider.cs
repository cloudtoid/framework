using System;
using System.Diagnostics;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
