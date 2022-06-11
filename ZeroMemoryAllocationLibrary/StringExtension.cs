// // ---------------------------------------------------------------------
// // <copyright file=“StringExtension.cs” company="Ishkul">
// //   Copyright (c) Ishkul. All rights reserved.
// // </copyright>
// // --------------------------------------------------------------------

namespace ZeroMemoryAllocationLibrary
{
    using System;

    public class StringExtension
    {
        public static ReadOnlySpan<char> Parse(ref ReadOnlySpan<char> input, char separator)
        {
            if (input.IsEmpty)
            {
                return ReadOnlySpan<char>.Empty;
            }

            var index = input.IndexOf(separator);
            ReadOnlySpan<char> output;
            if (index == -1)
            {
                output = input;
                input = Span<char>.Empty;
            }
            else
            {
                output = input.Slice(start: 0, length: index);
                input = input.Slice(start: index + 1);
            }

            return output;
        }
    }
}

