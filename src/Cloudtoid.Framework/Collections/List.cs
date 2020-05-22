namespace Cloudtoid
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerStepThrough]
    public static class List
    {
        public static IReadOnlyList<T> Empty<T>() => Array.Empty<T>();
    }
}
