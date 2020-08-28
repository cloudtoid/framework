using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class EnumerateValueTypeItems
    {
        private static readonly List<int> List = Enumerable.Range(1, 100000).ToList();
        private static readonly ReadOnlyValueList<int> ValueList = new ReadOnlyValueList<int>(Enumerable.Range(1, 100000));

        [Benchmark(Description = "Enumerate a list of value-type items", Baseline = true)]
        public int Baseline()
        {
            var value = 0;
            foreach (var v in List)
                value = v;

            return value;
        }

        [Benchmark(Description = "Enumerate a value list of value-type items")]
        public int New()
        {
            var value = 0;
            foreach (var v in ValueList)
                value = v;

            return value;
        }
    }
}
