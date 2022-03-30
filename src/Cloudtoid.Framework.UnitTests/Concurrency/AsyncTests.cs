using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using static FluentAssertions.FluentActions;

namespace Cloudtoid.Framework.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.VisualStudio.Threading.Analyzers", "VSTHRD003", Justification = "Reviewed.")]
    public sealed class AsyncTests
    {
        [TestMethod]
        public async Task TraceOnFaulted_WhenTaskThrows_ErrorIsLoggedAsync()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => throw new InvalidCastException())
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            await Invoking(async () => await task).Should().ThrowExactlyAsync<InvalidCastException>();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", level: LogLevel.Error);
        }

        [TestMethod]
        public async Task TraceOnFaulted_WhenNoExceptionThrown_NoErrorIsLoggedAsync()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => { })
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            await Invoking(() => task).Should().NotThrowAsync();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", 0, LogLevel.Error);

            task.IsCompleted.Should().BeTrue();
        }

        [TestMethod]
        public async Task TraceOnFaultedWithResult_WhenTaskThrows_ErrorIsLoggedAsync()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run((Func<int>)(() => throw new InvalidCastException()))
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            await Invoking(() => task).Should().ThrowExactlyAsync<InvalidCastException>();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", level: LogLevel.Error);
        }

        [TestMethod]
        public async Task TraceOnFaultedWithResult_WhenNoExceptionThrown_NoErrorIsLoggedAsync()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => 10)
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            var result = 0;
            await Invoking(async () => result = await task).Should().NotThrowAsync();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", 0, LogLevel.Error);

            task.IsCompleted.Should().BeTrue();
            result.Should().Be(10);
        }
    }
}
