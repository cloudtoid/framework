using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class ReadOnlyValueListBenchmark
    {
        [Benchmark(Description = "Creation of list with 1 value-type item")]
        public List<int> OneValueTypeItemListBaseline()
            => new List<int>(1) { 10 };

        [Benchmark(Description = "Creation of value list with 1 value-type item - requires boxing")]
        public ReadOnlyValueList<int> OneValueTypeItemList()
            => new ReadOnlyValueList<int>(10);
    }
}
