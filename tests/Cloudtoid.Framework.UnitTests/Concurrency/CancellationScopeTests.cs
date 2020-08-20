using System.Threading;
using System.Threading.Tasks;
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

        [TestMethod]
        public async ValueTask Execute_WhenTask_SuccessAsync()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();

            await CancellationScope.Execute(
                source1.Token,
                source2.Token,
                ct =>
                {
                    ct.IsCancellationRequested.Should().BeFalse();
                    source1.Cancel();
                    ct.IsCancellationRequested.Should().BeTrue();
                    return Task.CompletedTask;
                });
        }

        [TestMethod]
        public async ValueTask Execute_WhenValueTask_SuccessAsync()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();

            await CancellationScope.Execute(
                source1.Token,
                source2.Token,
                ct =>
                {
                    ct.IsCancellationRequested.Should().BeFalse();
                    source1.Cancel();
                    ct.IsCancellationRequested.Should().BeTrue();
                    return new ValueTask(Task.CompletedTask);
                });
        }

        [TestMethod]
        public async ValueTask Execute_WhenTaskWithAsyncAwait_SuccessAsync()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();

            await CancellationScope.Execute(
                source1.Token,
                source2.Token,
                async (state, ct) =>
                {
                    state.Should().Be("state");
                    ct.IsCancellationRequested.Should().BeFalse();
                    source1.Cancel();
                    ct.IsCancellationRequested.Should().BeTrue();
                    await Task.CompletedTask;
                },
                "state");
        }
    }
}
