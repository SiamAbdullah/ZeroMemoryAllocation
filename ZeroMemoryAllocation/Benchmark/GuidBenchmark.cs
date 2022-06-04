// // ---------------------------------------------------------------------
// // <copyright file=“GuidBenchmark.cs” company="Ishkul">
// //   Copyright (c) Ishkul. All rights reserved.
// // </copyright>
// // --------------------------------------------------------------------

namespace ZeroMemoryAllocationLibrary
{
    using System;
    using BenchmarkDotNet.Attributes;

    [MemoryDiagnoser]
    public class GuidBenchmark
    {
        private readonly Guid guid;
	    private readonly string guid64Base;
        public GuidBenchmark()
        {
            this.guid = Guid.NewGuid();
	        this.guid64Base = ConverTo64BaseString();

	        Console.WriteLine($"original = {this.guid.ToString()}, base64Conversion = {GuidExtension.GuidToBase64String(this.guid)}");
            Console.WriteLine($"original64BaseString = {this.guid64Base}, GuidConversion = {GuidExtension.Base64StringToGuid(this.guid64Base).ToString()}");
        }
	    
	    [Benchmark]
        public void TestGuidExtensionTo64BaseString()
        {
            GuidExtension.GuidToBase64String(this.guid);
        }

        [Benchmark]
        public void TestGuidExtensionStringToGuid()
        {
            GuidExtension.Base64StringToGuid(this.guid64Base);
        }

	    private string ConverTo64BaseString()
	    {
            return Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
        }
    }
}

