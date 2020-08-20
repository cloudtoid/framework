using System;
using System.Threading;
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

        private static readonly ObjectPool<CancellationTokenSource> Pool;

        static CancellationScope()
        {
            var provider = new DefaultObjectPoolProvider
            {
                MaximumRetained = 1024
            };
            Pool = provider.Create(new Policy());
        }

        public static void Execute(
            CancellationToken token1,
            CancellationToken token2,
            Action<CancellationToken> action)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, source, false))
                using (token2.Register(CancelAction, source, false))
                    action(source.Token);
            }
            finally
            {
                Pool.Return(source);
            }
        }

        public static void Execute<TState>(
            CancellationToken token1,
            CancellationToken token2,
            Action<TState, CancellationToken> action,
            TState state)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, source, false))
                using (token2.Register(CancelAction, source, false))
                    action(state, source.Token);
            }
            finally
            {
                Pool.Return(source);
            }
        }

        public static TResult Execute<TResult>(
            CancellationToken token1,
            CancellationToken token2,
            Func<CancellationToken, TResult> func)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, source, false))
                using (token2.Register(CancelAction, source, false))
                    return func(source.Token);
            }
            finally
            {
                Pool.Return(source);
            }
        }

        public static TResult Execute<TState, TResult>(
            CancellationToken token1,
            CancellationToken token2,
            Func<TState, CancellationToken, TResult> func,
            TState state)
        {
            var source = Pool.Get();
            try
            {
                using (token1.Register(CancelAction, source, false))
                using (token2.Register(CancelAction, source, false))
                    return func(state, source.Token);
            }
            finally
            {
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
