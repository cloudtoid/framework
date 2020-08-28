using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class MultipleValueTypeItems
    {
        [Benchmark(Description = "Creation of list with 2 value-type items", Baseline = true)]
        public List<int> Baseline()
            => new List<int>(2) { 10, 20 };

        // This should be faster and consume less memory than baseline
        [Benchmark(Description = "Creation of value list with 2 value-type items")]
        public ReadOnlyValueList<int> New()
            => new ReadOnlyValueList<int>(new int[] { 10, 20 });
    }
}
