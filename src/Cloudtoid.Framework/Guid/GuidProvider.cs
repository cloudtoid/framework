using System;
using System.Diagnostics;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    internal sealed class GuidProvider : IGuidProvider
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
