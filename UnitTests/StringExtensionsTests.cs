namespace Microsoft.Search.Frontend.Shared.UnitTests.Frontdoor
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ZeroMemoryAllocationLibrary;

    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void ValidateParseNextMethodWithIP()
        {
            var output = new string[4] { "12", "3", "21", "4" };
            var input = "12.3.21.4".AsSpan();
            var index = 0;
            while (input.Length > 0)
            {
                var part = StringExtensions.ParseNext(ref input, '.');
                Assert.IsTrue(part.Equals(output[index], StringComparison.Ordinal), $"parsed string shoud be {output[index]} but got {part.ToString()}");
                index++;
            }
        }

        [TestMethod]
        public void ValidateParseNextMethodWithNoSeparator()
        {
            var input = "Hello.Bing!!!".AsSpan();
            var part = StringExtensions.ParseNext(ref input, ',');
            Assert.IsTrue(part.Equals("Hello.Bing!!!", StringComparison.Ordinal), $"parsed string shoud be Hello.Bing!!! but got {part.ToString()}");
            Assert.IsTrue(input.Length == 0, "Remaing string length should be 0");
        }

        [TestMethod]
        public void ValidateParseNextMethodWithEmptyContentString()
        {
            var output = new string[2] { "Hello", "Bing" };
            var input = ".".AsSpan();
            var part = StringExtensions.ParseNext(ref input, '.');
            Assert.IsTrue(part.Length == 0, $"parsed string lengtth should be 0");
            Assert.IsTrue(input.Length == 0, $"input string lengtth should be 0");
        }

        [TestMethod]
        public void ValidateParseNextMethodWithEmptyString()
        {
            var output = new string[2] { "Hello", "Bing" };
            var input = "".AsSpan();
            var part = StringExtensions.ParseNext(ref input, '.');
            Assert.IsTrue(part.Length == 0, $"parsed string lengtth should be 0");
            Assert.IsTrue(input.Length == 0, $"input string lengtth should be 0");
        }

        [TestMethod]
        public void ValidateParseNextMethodV3()
        {
            var output = new string[3] { "", "Hello", "Bing" };
            var input = ".Hello.Bing.".AsSpan();
            var index = 0;
            while (input.Length > 0)
            {
                var part = StringExtensions.ParseNext(ref input, '.');
                Assert.IsTrue(part.Equals(output[index], StringComparison.Ordinal), $"parsed string shoud be {output[index]} but got {part.ToString()}");
                index++;
            }
        }

        [TestMethod]
        public void ValidateParseNextMethodV4()
        {
            var output = new string[5] { "", "", "", "Hello", "Bing" };
            var input = "...Hello.Bing.".AsSpan();
            var index = 0;
            while (input.Length > 0)
            {
                var part = StringExtensions.ParseNext(ref input, '.');
                Assert.IsTrue(part.Equals(output[index], StringComparison.Ordinal), $"parsed string shoud be {output[index]} but got {part.ToString()}");
                index++;
            }
        }

        [TestMethod]
        public void ValidateSplitOnce()
        {
            var input = "Hello.Bing.Awesome.".AsSpan();
            ReadOnlySpan<char> part1 = ReadOnlySpan<char>.Empty;
            ReadOnlySpan<char> part2 = ReadOnlySpan<char>.Empty;

            StringExtensions.SplitOnce(ref input, '.', ref part1, ref part2);
            Assert.IsTrue(part1.Equals("Hello", StringComparison.Ordinal), $"parsed part1 string shoud be \"Hello\" but got {part1.ToString()}");
            Assert.IsTrue(part2.Equals("Bing.Awesome.", StringComparison.Ordinal), $"parsed part2 string shoud be \"Bing.Awesome.\" but got {part1.ToString()}");
        }

        [TestMethod]
        public void ValidateSplitOnceWithOnePartEmpty()
        {
            var input = "Hello.".AsSpan();
            ReadOnlySpan<char> part1 = ReadOnlySpan<char>.Empty;
            ReadOnlySpan<char> part2 = ReadOnlySpan<char>.Empty;

            StringExtensions.SplitOnce(ref input, '.', ref part1, ref part2);
            Assert.IsTrue(part1.Equals("Hello", StringComparison.Ordinal), $"parsed part1 string shoud be \"Hello\" but got {part1.ToString()}");
            Assert.IsTrue(part2.Equals("", StringComparison.Ordinal), $"parsed part2 string shoud be \"\" but got {part1.ToString()}");
        }

        [TestMethod]
        public void ValidateSplitOnceWithNoSeparator()
        {
            var input = "Hello".AsSpan();
            ReadOnlySpan<char> part1 = ReadOnlySpan<char>.Empty;
            ReadOnlySpan<char> part2 = ReadOnlySpan<char>.Empty;

            StringExtensions.SplitOnce(ref input, '.', ref part1, ref part2);
            Assert.IsTrue(part1.Equals("Hello", StringComparison.Ordinal), $"parsed part1 string shoud be \"Hello\" but got {part1.ToString()}");
            Assert.IsTrue(part2.Equals("", StringComparison.Ordinal), $"parsed part2 string shoud be \"\" but got {part1.ToString()}");
        }

        [TestMethod]
        public void ValidateSplitOnceWithFirstPartEmpty()
        {
            var input = ".bing".AsSpan();
            ReadOnlySpan<char> part1 = ReadOnlySpan<char>.Empty;
            ReadOnlySpan<char> part2 = ReadOnlySpan<char>.Empty;

            StringExtensions.SplitOnce(ref input, '.', ref part1, ref part2);
            Assert.IsTrue(part2.Equals("bing", StringComparison.Ordinal), $"parsed part2 string shoud be \"bing\" but got {part1.ToString()}");
            Assert.IsTrue(part1.Equals("", StringComparison.Ordinal), $"parsed part1 string shoud be \"\" but got {part1.ToString()}");
        }

        [TestMethod]
        public void ValidateSplitOnceWithOnlySeparator()
        {
            var input = ".".AsSpan();
            ReadOnlySpan<char> part1 = ReadOnlySpan<char>.Empty;
            ReadOnlySpan<char> part2 = ReadOnlySpan<char>.Empty;

            StringExtensions.SplitOnce(ref input, '.', ref part1, ref part2);
            Assert.IsTrue(part2.Equals("", StringComparison.Ordinal), $"parsed part2 string shoud be \"\" but got {part1.ToString()}");
            Assert.IsTrue(part1.Equals("", StringComparison.Ordinal), $"parsed part1 string shoud be \"\" but got {part1.ToString()}");
        }
    }
}
