// // ---------------------------------------------------------------------
// // <copyright file=“StringExtensionBenchmark.cs” company="Ishkul">
// //   Copyright (c) Ishkul. All rights reserved.
// // </copyright>
// // --------------------------------------------------------------------

namespace ZeroMemoryAllocation.Benchmark
{
    using System;
    using BenchmarkDotNet.Attributes;
    using ZeroMemoryAllocationLibrary;

    [MemoryDiagnoser]
    public class StringExtensionBenchmark
    {
        private const string input = "hello.Bangladesh.2022.kopa.samsu.hi";

        [Benchmark]
        public void ReadOnlySpanParser()
        {
            var inputSpan = input.AsSpan();
            while (inputSpan.Length > 0)
            {
                var parsed = StringExtensions.ParseNext(ref inputSpan, '.');
                // Console.WriteLine($"parsed value:  {parsed.ToString()} remaining: {inputSpan}");
            }
        }

        [Benchmark]
        public void RegularStringParser()
        {
            var parts = input.Split('.');
            for (var i=0; i<parts.Length;i++)
            {
                var p = parts[i];
                // Console.WriteLine($"parsed value:  {p}");
            }
        }
    }
}

