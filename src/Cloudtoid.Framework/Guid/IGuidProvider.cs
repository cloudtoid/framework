using System;

namespace Cloudtoid
{
    /// <summary>
    /// This can be mocked by the test code to ensure that Guid generation is predictable.
    /// </summary>
    public interface IGuidProvider
    {
        Guid NewGuid();
    }
}
