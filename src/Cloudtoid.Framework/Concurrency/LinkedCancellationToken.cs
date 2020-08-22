using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cloudtoid
{
    /// <summary>
    /// This class provides a pool of <see cref="CancellationTokenSource"/>s that are used to create short-lived
    /// linked token sources.
    /// </summary>
    public readonly struct LinkedCancellationToken : IEquatable<LinkedCancellationToken>, IDisposable
    {
        // internal for testing
        internal static readonly int Count = Math.Min(Environment.ProcessorCount * 16, 256);

        private static readonly Action<object?> CancelAction = s => ((CancellationTokenSource)s!).Cancel();
        private static readonly CancellationTokenSource?[] Items;
        private readonly CancellationTokenSource? source;
        private readonly CancellationTokenRegistration reg1;
        private readonly CancellationTokenRegistration reg2;

        static LinkedCancellationToken()
        {
            Items = new CancellationTokenSource[Count];
            for (var i = 0; i < Count; i++)
                Items[i] = new CancellationTokenSource();
        }

        public LinkedCancellationToken(CancellationToken token1, CancellationToken token2)
        {
            var source = RentSource();
            if (source is null)
            {
                source = CancellationTokenSource.CreateLinkedTokenSource(token1, token2);
                reg1 = reg2 = default;
            }
            else
            {
                reg1 = token1.Register(CancelAction, source, false);
                reg2 = token2.Register(CancelAction, source, false);
            }

            this.source = source;
        }

        public CancellationToken Token
            => source?.Token ?? default;

        public static bool operator ==(LinkedCancellationToken left, LinkedCancellationToken right)
            => left.Equals(right);
        public static bool operator !=(LinkedCancellationToken left, LinkedCancellationToken right)
            => !left.Equals(right);

        public override bool Equals(object? obj)
            => obj is LinkedCancellationToken token && Equals(token);

        public bool Equals(LinkedCancellationToken other)
            => ReferenceEquals(source, other.source);

        public override int GetHashCode()
            => source?.GetHashCode() ?? 0;

        public void Dispose()
        {
            if (source is null)
                return;

            if (reg1 == default)
            {
                source.Dispose();
            }
            else
            {
                reg1.Dispose();
                reg2.Dispose();

                if (source.IsCancellationRequested)
                {
                    source.Dispose();
                    ReturnSource(new CancellationTokenSource());
                }
                else
                {
                    ReturnSource(source);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static CancellationTokenSource? RentSource()
        {
            for (var i = 0; i < Count; i++)
            {
                while (true)
                {
                    var snapshot = Items[i];
                    if (snapshot is null)
                        break;

                    if (Interlocked.CompareExchange(ref Items[i], null, snapshot) == snapshot)
                        return snapshot;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ReturnSource(CancellationTokenSource source)
        {
            var i = 0;
            while (true)
            {
                if (Items[i] is null)
                {
                    if (Interlocked.CompareExchange(ref Items[i], source, null) == null)
                        return;
                }
                else
                {
                    if (ReferenceEquals(source, Items[i]))
                        return;
                }

                i = (i + 1) % Count;
            }
        }
    }
}
