// // ---------------------------------------------------------------------
// // <copyright file=“StringExtension.cs” company="Ishkul">
// //   Copyright (c) Ishkul. All rights reserved.
// // </copyright>
// // --------------------------------------------------------------------

namespace ZeroMemoryAllocationLibrary
{
    using System;

    public class StringExtensions
    {
        public static ReadOnlySpan<char> ParseNext(ref ReadOnlySpan<char> inputStringSpan, char separator)
        {
            if (inputStringSpan.IsEmpty)
            {
                return ReadOnlySpan<char>.Empty;
            }

            var separatorIndex = inputStringSpan.IndexOf(separator);

            // if string starts with separator then separatorIndex will be 0. e.g: separator= ',' inputStringSpan = ","
            if (separatorIndex == 0)
            {
                inputStringSpan = inputStringSpan.Length == 1 ? ReadOnlySpan<char>.Empty : inputStringSpan.Slice(1);
                return ReadOnlySpan<char>.Empty;
            }

            /*  if separatorIndex is -1 that means seperator is not found
             *     then originalString will be currentFieldSpan and make originalString empty
             */
            var currentSpanLen = separatorIndex == -1 ? inputStringSpan.Length : separatorIndex;
            var currentFieldSpan = inputStringSpan.Slice(0, currentSpanLen);
            inputStringSpan = inputStringSpan.Length <= currentSpanLen + 1 ? ReadOnlySpan<char>.Empty : inputStringSpan.Slice(currentSpanLen + 1);

            return currentFieldSpan;
        }

        public static void SplitOnce(ref ReadOnlySpan<char> inputStringSpan, char separator, ref ReadOnlySpan<char> part1, ref ReadOnlySpan<char> part2)
        {
            part1 = ReadOnlySpan<char>.Empty;
            part2 = ReadOnlySpan<char>.Empty;

            if (!inputStringSpan.IsEmpty)
            {
                var separatorIndex = inputStringSpan.IndexOf(separator);
                if (separatorIndex == 0)
                {
                    part2 = inputStringSpan.Length == 1 ? ReadOnlySpan<char>.Empty : inputStringSpan.Slice(1);
                }
                else
                {
                    var currentSpanLen = separatorIndex == -1 ? inputStringSpan.Length : separatorIndex;
                    part1 = inputStringSpan.Slice(0, currentSpanLen);
                    part2 = inputStringSpan.Length <= currentSpanLen + 1 ? ReadOnlySpan<char>.Empty : inputStringSpan.Slice(currentSpanLen + 1);
                }
            }
        }
    }
}

