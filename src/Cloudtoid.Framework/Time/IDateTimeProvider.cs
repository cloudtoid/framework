using System;

namespace Cloudtoid
{
    /// <summary>
    /// This can be mocked by the test code to ensure there are no race conditions between what the product
    /// thinks is Now and what Test would check for.
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}
