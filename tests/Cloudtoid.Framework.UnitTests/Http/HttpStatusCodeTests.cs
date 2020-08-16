using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cloudtoid.Framework.UnitTests
{
    [TestClass]
    public sealed class HttpStatusCodeTests
    {
        [TestMethod]
        public void IsSuccessStatusCode_Between200And299_IsSuccess()
        {
            ((int[])Enum.GetValues(typeof(HttpStatusCode)))
                .Where(v => v >= 200 && v < 300)
                .Cast<HttpStatusCode>()
                .All(c => c.IsSuccessStatusCode())
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void IsSuccessStatusCode_NotBetween200And299_IsNotSuccess()
        {
            ((int[])Enum.GetValues(typeof(HttpStatusCode)))
                .Where(v => v < 200 && v >= 300)
                .Cast<HttpStatusCode>()
                .All(c => !c.IsSuccessStatusCode())
                .Should()
                .BeTrue();
        }
    }
}
