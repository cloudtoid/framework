using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cloudtoid.Framework.UnitTests
{
    [TestClass]
    public class GuidTests
    {
        [TestMethod]
        public void Base64UrlTests()
        {
            var tests = new (Guid Guid, string Short)[]
            {
                new(Guid.Empty, "AAAAAAAAAAAAAAAAAAAAAA"),
                new(new Guid(new byte[] { 4, 16, 65, 4, 16, 65, 4, 16, 65, 4, 16, 65, 4, 16, 65, 4 }), "BBBBBBBBBBBBBBBBBBBBBA"),
                new(new Guid(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }), "_____________________D")
            };

            foreach (var (g, s) in tests)
                g.Base64UrlEncode().Should().Be(s);
        }

        [TestMethod]
        public void Base41UrlTests()
        {
            var tests = new (Guid Guid, string Short)[]
            {
                new(Guid.Empty, "aaaaaaaaaaaaaaaaaaaaaaaa")
            };

            foreach (var (g, s) in tests)
                g.Base41UrlEncode().Should().Be(s);
        }
    }
}
