namespace Cloudtoid.Framework.UnitTests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class HttpVersionTests
    {
        [TestMethod]
        public void GetHttpVersion_AllHttpVersions_CorrectResults()
        {
            string? protocol = "HTTP/1.0";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version10);

            protocol = "HTTP/1.1";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version11);

            protocol = "HTTP/2.0";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version20);

            protocol = "HTTP/3.0";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version30);

            protocol = "HTTP/4.0";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version40);

            protocol = "HTTP/5.0";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version50);

            protocol = "HTTP/1";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version10);

            protocol = "HTTP/2";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version20);

            protocol = "HTTP/3";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version30);

            protocol = "HTTP/4";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version40);

            protocol = "HTTP/5";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version50);

            protocol = "/1";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version10);

            protocol = "/1.1";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version11);

            protocol = "/2";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version20);

            protocol = "/3";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version30);

            protocol = "/4";
            HttpVersion.ParseOrDefault(protocol).Should().Be(HttpVersion.Version40);

            protocol = "default";
            HttpVersion.ParseOrDefault(protocol).Should().BeNull();

            protocol = null;
            HttpVersion.ParseOrDefault(protocol).Should().BeNull();
        }
    }
}
