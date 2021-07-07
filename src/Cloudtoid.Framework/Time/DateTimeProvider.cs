using System;
using System.Diagnostics;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
