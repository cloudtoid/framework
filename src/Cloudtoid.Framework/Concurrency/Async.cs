using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    [SuppressMessage("Microsoft.VisualStudio.Threading.Analyzers", "VSTHRD200", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.VisualStudio.Threading.Analyzers", "VSTHRD105", Justification = "Reviewed.")]
    [DebuggerStepThrough]
    public static class Async
    {
        public static async Task<TResult> WithTimeout<TResult>(
            this Func<CancellationToken, Task<TResult>> funcAsync,
            TimeSpan timeout)
        {
            CheckValue(funcAsync, nameof(funcAsync));

            using (var source = new CancellationTokenSource(timeout))
            {
                return await funcAsync(source.Token);
            }
        }

        public static async Task<TResult> WithTimeout<TResult>(
            this Func<CancellationToken, Task<TResult>> funcAsync,
            TimeSpan timeout,
            CancellationToken externalToken)
        {
            CheckValue(funcAsync, nameof(funcAsync));

            externalToken.ThrowIfCancellationRequested();

            using (var source = new CancellationTokenSource(timeout))
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(source.Token, externalToken))
            {
                return await funcAsync(linked.Token);
            }
        }

        public static async Task<TResult> WithTimeout<TInput, TResult>(
            this Func<TInput, CancellationToken, Task<TResult>> funcAsync,
            TInput input,
            TimeSpan timeout,
            CancellationToken externalToken)
        {
            CheckValue(funcAsync, nameof(funcAsync));

            externalToken.ThrowIfCancellationRequested();

            using (var source = new CancellationTokenSource(timeout))
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(source.Token, externalToken))
            {
                return await funcAsync(input, linked.Token);
            }
        }

        public static Task TraceOnFaulted<TLoggerCategoryName>(
            this Task task,
            ILogger<TLoggerCategoryName> logger,
            string message,
            CancellationToken cancellationToken)
        {
            CheckValue(task, nameof(task));
            CheckValue(logger, nameof(logger));

            cancellationToken.ThrowIfCancellationRequested();

            return task.ContinueWith(
                t =>
                {
                    var ex = t.Exception;
                    if (ex is null)
                        return;

                    if (ex.IsFatal())
                    {
                        logger.LogCritical(ex, message);
                        return;
                    }

                    logger.LogError(ex, message);
                    ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
                },
                cancellationToken);
        }

        public static async Task<TResult> TraceOnFaulted<TLoggerCategoryName, TResult>(
            this Task<TResult> task,
            ILogger<TLoggerCategoryName> logger,
            string message,
            CancellationToken cancellationToken)
        {
            CheckValue(task, nameof(task));
            CheckValue(logger, nameof(logger));

            cancellationToken.ThrowIfCancellationRequested();

            await task.ContinueWith(
                t =>
                {
                    var ex = t.Exception;
                    if (ex is null)
                        return;

                    if (ex.IsFatal())
                    {
                        logger.LogCritical(ex, message);
                        return;
                    }

                    logger.LogError(ex, message);
                    ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
                },
                cancellationToken);

#pragma warning disable VSTHRD003 // Avoid awaiting foreign Tasks
            return await task;
#pragma warning restore VSTHRD003 // Avoid awaiting foreign Tasks
        }

        public static void FireAndForget<TLoggerCategoryName>(
            this Task task,
            ILogger<TLoggerCategoryName> logger,
            string faultedMessage,
            CancellationToken cancellationToken)
        {
            CheckValue(task, nameof(task));
            CheckValue(logger, nameof(logger));
            _ = task.TraceOnFaulted(logger, faultedMessage, cancellationToken);
        }

        // It creates a task that is completed if the cancellationToken is cancelled.
        public static Task WhenCancelled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s!).SetResult(true), tcs);
            return tcs.Task;
        }
    }
}
