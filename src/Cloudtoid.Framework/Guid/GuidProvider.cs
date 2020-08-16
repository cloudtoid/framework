using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cloudtoid
{
    [SuppressMessage("BannedApiAnalyzer", "RS0030", Justification = "Instead of the banned API Guid.NewGuid, everyone must be using this class.")]
    [DebuggerStepThrough]
    internal sealed class GuidProvider : IGuidProvider
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
