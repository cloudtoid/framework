using System;

namespace Cloudtoid
{
    /// <summary>
    /// This can be mocked by the test code to ensure that unique identifier generation is predictable.
    /// </summary>
    public interface IUniqueIdentifierProvider
    {
        Guid CreateGuid();
        string CreateCaseSensitveIdentifier();
        string CreateCaseInsensitveIdentifier();
    }
}
