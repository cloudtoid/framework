using BenchmarkDotNet.Running;

namespace Cloudtoid.Framework.Benchmark
{
    public sealed class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
