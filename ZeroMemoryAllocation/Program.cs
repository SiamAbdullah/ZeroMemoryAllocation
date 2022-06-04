// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ZeroMemoryAllocationLibrary;

Console.WriteLine("Hello!!! Allocation free library");
BenchmarkRunner.Run<GuidBenchmark>();
new GuidBenchmark();
