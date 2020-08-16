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
    public sealed class ConcurrencyExtensionsTests
    {
        [TestMethod]
        public void TraceOnFaulted_WhenTaskThrows_ErrorIsLogged()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => throw new InvalidCastException())
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            Invoking(async () => await task).Should().ThrowExactly<InvalidCastException>();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", level: LogLevel.Error);
        }

        [TestMethod]
        public void TraceOnFaulted_WhenNoExceptionThrown_NoErrorIsLogged()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => { })
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            Invoking(() => task).Should().NotThrow();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", 0, LogLevel.Error);

            task.IsCompleted.Should().BeTrue();
        }

        [TestMethod]
        public void TraceOnFaultedWithResult_WhenTaskThrows_ErrorIsLogged()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run((Func<int>)(() => throw new InvalidCastException()))
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            Invoking(() => task).Should().ThrowExactly<InvalidCastException>();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", level: LogLevel.Error);
        }

        [TestMethod]
        public void TraceOnFaultedWithResult_WhenNoExceptionThrown_NoErrorIsLogged()
        {
            var logger = Substitute.For<ILogger<object>>();

            var task = Task
                .Run(() => 10)
                .TraceOnFaulted(logger, "TraceOnFaultedWithException", default);

            int result = 0;
            Invoking(async () => result = await task).Should().NotThrow();

            logger.LogReceivedThatContains("TraceOnFaultedWithException", 0, LogLevel.Error);

            task.IsCompleted.Should().BeTrue();
            result.Should().Be(10);
        }
    }
}
