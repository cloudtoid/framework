using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.LinkedCancellationToken
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class LinkedCancellationTokenBenchmarks
    {
        private static readonly CancellationTokenSource Source1 = new CancellationTokenSource();
        private static readonly CancellationTokenSource Source2 = new CancellationTokenSource();

        [Benchmark(Description = ".NET CancellationTokenSource", Baseline = true)]
        public CancellationToken Baseline()
        {
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(Source1.Token, Source2.Token);
            return linked.Token;
        }

        [Benchmark(Description = "New LinkedCancellationTokenSource", Baseline = false)]
        public CancellationToken LinkedCancellationTokenSource()
        {
            using var linked = new Cloudtoid.LinkedCancellationToken(Source1.Token, Source2.Token);
            return linked.Token;
        }
    }
}
