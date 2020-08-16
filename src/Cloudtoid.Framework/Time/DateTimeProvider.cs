using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cloudtoid
{
    [SuppressMessage("BannedApiAnalyzer", "RS0030", Justification = "Instead of the banned APIs, we should use the following extension methods.")]
    [DebuggerStepThrough]
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
