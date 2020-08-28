using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class OneReferenceTypeItem
    {
        private static readonly string Value = "test";

        [Benchmark(Description = "Creation of list with 1 ref-type item", Baseline = true)]
        public List<string> Baseline()
            => new List<string>(1) { Value };

        // This should be faster and allocate no heap-memory
        [Benchmark(Description = "Creation of value list with 1 ref-type item")]
        public ReadOnlyValueList<string> New()
            => new ReadOnlyValueList<string>(Value);
    }
}
