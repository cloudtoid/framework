using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace Cloudtoid
{
    /// <summary>
    /// Provides a super efficient and mostly heap-memory allocation-free linked cancellation token source.
    /// A source is retrieved from an object pool and is returned once the action is complete if the source has not been cancelled.
    /// </summary>
    public static class CancellationScope
    {
        private static readonly Action<object?> CancelAction =
            s => ((CancellationTokenSource)s!).Cancel();

        private static readonly ObjectPool<CancellationTokenSource> Pool =
            new DefaultObjectPool<CancellationTokenSource>(new Policy(), 1024);

        public static void Execute(
            CancellationToken token1,
            CancellationToken token2,
            Action<CancellationToken> action)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, token1))
                using (token2.Register(CancelAction, token2))
                    action(source.Token);
            }
            finally
            {
                if (!source.IsCancellationRequested)
                    Pool.Return(source);
            }
        }

        public static async Task ExecuteAsync(
            CancellationToken token1,
            CancellationToken token2,
            Func<CancellationToken, Task> action)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, token1))
                using (token2.Register(CancelAction, token2))
                    await action(source.Token);
            }
            finally
            {
                if (!source.IsCancellationRequested)
                    Pool.Return(source);
            }
        }

        public static async Task<TResult> ExecuteAsync<TResult>(
            CancellationToken token1,
            CancellationToken token2,
            Func<CancellationToken, Task<TResult>> action)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, token1))
                using (token2.Register(CancelAction, token2))
                    return await action(source.Token);
            }
            finally
            {
                if (!source.IsCancellationRequested)
                    Pool.Return(source);
            }
        }

        public static async ValueTask<TResult> ExecuteAsync<TResult>(
            CancellationToken token1,
            CancellationToken token2,
            Func<CancellationToken, ValueTask<TResult>> func)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, token1))
                using (token2.Register(CancelAction, token2))
                    return await func(source.Token);
            }
            finally
            {
                if (!source.IsCancellationRequested)
                    Pool.Return(source);
            }
        }

        private sealed class Policy : PooledObjectPolicy<CancellationTokenSource>
        {
            public override CancellationTokenSource Create()
                => new CancellationTokenSource();

            public override bool Return(CancellationTokenSource source)
                => !source.IsCancellationRequested;
        }
    }
}
