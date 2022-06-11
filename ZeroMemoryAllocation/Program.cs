// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ZeroMemoryAllocation.Benchmark;
using ZeroMemoryAllocationLibrary;

Console.WriteLine("Hello!!! Allocation free library");

/*BenchmarkRunner.Run<GuidBenchmark>();
new GuidBenchmark();
*/

BenchmarkRunner.Run<StringExtensionBenchmark>();
/*var sb = new StringExtensionBenchmark();
sb.ReadOnlySpanParser(); */
