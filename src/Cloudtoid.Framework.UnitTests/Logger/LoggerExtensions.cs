using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Exceptions;
using static Cloudtoid.Contract;

namespace Cloudtoid.Framework.UnitTests
{
    public static class LoggerExtensions
    {
        private static readonly MethodInfo LogMethodInfo = typeof(ILogger).GetMethod("Log")
            ?? throw new NotSupportedException("Expected a single method called Log on ILogger");

        public static ILogger<TCategory> ReceivedLog<TCategory>(
            this ILogger<TCategory> substitute,
            int requiredNumberOfCalls = 1,
            LogLevel level = LogLevel.None)
        {
            CheckValue(substitute, nameof(substitute));
            return substitute.AssertLogReceived(requiredNumberOfCalls, level);
        }

        public static ILogger<TCategory> LogReceivedThatContains<TCategory>(
            this ILogger<TCategory> substitute,
            string searchString,
            int requiredNumberOfCalls = 1,
            LogLevel level = LogLevel.None)
        {
            CheckValue(substitute, nameof(substitute));
            CheckNonEmpty(searchString, nameof(searchString));

            return substitute.AssertLogReceived(requiredNumberOfCalls, level, searchString);
        }

        private static ILogger<TCategory> AssertLogReceived<TCategory>(
            this ILogger<TCategory> substitute,
            int requiredNumberOfCalls,
            LogLevel level,
            string? searchString = null)
        {
            var count = 0;
            foreach (var call in substitute.ReceivedCalls())
            {
                var methodInfo = call.GetMethodInfo();
                if (!methodInfo.IsGenericMethod || !ReferenceEquals(methodInfo.GetGenericMethodDefinition(), LogMethodInfo))
                    continue;

                var arguments = call.GetArguments();
                if (level != LogLevel.None && level != (LogLevel)arguments[0]!)
                    continue;

                if (searchString is null)
                {
                    count++;
                    continue;
                }

                var value = arguments[2]?.ToString();
                if (value is null)
                    continue;

                if (!value.ContainsOrdinal(searchString))
                    continue;

                count++;
            }

            if (count != requiredNumberOfCalls)
            {
                if (searchString is null)
                    throw new ReceivedCallsException($"Expected to receive {requiredNumberOfCalls} call(s) but received {count}");

                throw new ReceivedCallsException($"Expected to receive {requiredNumberOfCalls} call(s) that included '{searchString}' substring but received {count}");
            }

            return substitute;
        }
    }
}
