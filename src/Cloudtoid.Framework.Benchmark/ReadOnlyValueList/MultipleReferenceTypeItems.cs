using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class MultipleReferenceTypeItems
    {
        private static readonly string Value = "test";

        [Benchmark(Description = "Creation of list with 2 ref-type items", Baseline = true)]
        public List<string> Baseline()
            => new List<string>(2) { Value, Value };

        // This should be faster and consume less memory than baseline
        [Benchmark(Description = "Creation of value list with 2 ref-type items")]
        public ReadOnlyValueList<string> New()
            => new ReadOnlyValueList<string>(new string[] { Value, Value });
    }
}
