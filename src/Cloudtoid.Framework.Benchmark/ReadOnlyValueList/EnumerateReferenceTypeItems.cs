using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class EnumerateReferenceTypeItems
    {
        private static readonly string Value = "value";
        private static readonly List<string> List = Enumerable.Repeat(Value, 100000).ToList();
        private static readonly ReadOnlyValueList<string> ValueList = new ReadOnlyValueList<string>(Enumerable.Repeat(Value, 100000));

        [Benchmark(Description = "Enumerate a list of ref-type items", Baseline = true)]
        public string Baseline()
        {
            var value = string.Empty;
            foreach (var v in List)
                value = v;

            return value;
        }

        [Benchmark(Description = "Enumerate a value list of ref-type items")]
        public string New()
        {
            var value = string.Empty;
            foreach (var v in ValueList)
                value = v;

            return value;
        }
    }
}
