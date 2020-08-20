using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cloudtoid.Framework.UnitTests
{
    [TestClass]
    public sealed class CancellationScopeTests
    {
        [TestMethod]
        public void Execute_WhenSource1Cancelled_TokenCancelled()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();

            CancellationScope.Execute(
                source1.Token,
                source2.Token,
                ct =>
                {
                    ct.IsCancellationRequested.Should().BeFalse();
                    source1.Cancel();
                    ct.IsCancellationRequested.Should().BeTrue();
                });
        }

        [TestMethod]
        public void Execute_WhenSource2Cancelled_TokenCancelled()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();

            CancellationScope.Execute(
                source1.Token,
                source2.Token,
                ct =>
                {
                    ct.IsCancellationRequested.Should().BeFalse();
                    source2.Cancel();
                    ct.IsCancellationRequested.Should().BeTrue();
                });
        }
    }
}
