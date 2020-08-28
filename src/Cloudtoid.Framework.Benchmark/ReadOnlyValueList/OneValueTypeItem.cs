using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class OneValueTypeItem
    {
        [Benchmark(Description = "Creation of list with 1 value-type item", Baseline = true)]
        public List<int> Baseline()
            => new List<int>(1) { 10 };

        // This should be faster and consume less memory than baseline
        [Benchmark(Description = "Creation of value list with 1 value-type item - requires boxing")]
        public ReadOnlyValueList<int> New()
            => new ReadOnlyValueList<int>(10);
    }
}
