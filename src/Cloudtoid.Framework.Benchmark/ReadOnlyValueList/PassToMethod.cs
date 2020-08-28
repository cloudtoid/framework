using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Cloudtoid.Framework.Benchmark.ReadOnlyValueList
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class PassToMethod
    {
        private static readonly string Value = "test";
        private static readonly List<string> List = new List<string>(3) { Value, Value, Value };
        private static ReadOnlyValueList<string> valueList = new ReadOnlyValueList<string>(new string[] { Value, Value, Value });

        [Benchmark(Description = "Pass a list", Baseline = true)]
        public List<string> Baseline()
        {
            var value = List;
            for (var i = 0; i < 10000; i++)
                value = Callee(value);

            return value;
        }

        [Benchmark(Description = "Pass a value list by value")]
        public ReadOnlyValueList<string> ByValue()
        {
            var value = valueList;
            for (var i = 0; i < 10000; i++)
                value = Callee(value);

            return value;
        }

        [Benchmark(Description = "Pass a value list by ref")]
        public ref ReadOnlyValueList<string> ByReference()
        {
            ref var value = ref valueList;
            for (var i = 0; i < 10000; i++)
                value = CalleeRef(ref value);

            return ref value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static List<string> Callee(List<string> value)
            => value;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static ReadOnlyValueList<string> Callee(ReadOnlyValueList<string> value)
            => value;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static ref ReadOnlyValueList<string> CalleeRef(ref ReadOnlyValueList<string> value)
            => ref value;
    }
}
