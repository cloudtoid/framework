﻿using BenchmarkDotNet.Running;
using Cloudtoid.Framework.Benchmark.ReadOnlyValueList;

namespace Cloudtoid.Framework.Benchmark
{
    public sealed class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run<EnumerateValueTypeItems>();
            //// _ = BenchmarkRunner.Run(typeof(Program).Assembly);
            System.Console.ReadLine();
        }
    }
}
