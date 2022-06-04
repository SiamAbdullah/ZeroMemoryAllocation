// // ---------------------------------------------------------------------
// // <copyright file=“GuidExtension.cs” company="Ishkul">
// //   Copyright (c) Ishkul. All rights reserved.
// // </copyright>
// // --------------------------------------------------------------------

namespace ZeroMemoryAllocationLibrary
{
    using System;
    using System.Buffers.Text;
    using System.Runtime.InteropServices;

    public class GuidExtension
	{
        private const byte ForwardSlashByte = (byte)'/';
        private const char ForwardSlashChar = '/';
        private const char Hyphen = '-';
        private const byte PlusByte = (byte)'+';
        private const char PlusChar = '+';
        private const char Underscore = '_';
        private const char EqualChar = '=';

	    
        public static string GuidToBase64String(Guid guid)
        {
            Span<byte> guidBytes = stackalloc byte[16];
            Span<byte> base64Bytes = stackalloc byte[24];

            MemoryMarshal.TryWrite(guidBytes, ref guid);
            Base64.EncodeToUtf8(guidBytes, base64Bytes, out _, out _ );

            Span<char> finalChar = stackalloc char[22];
            for (var i=0; i<22; i++)
            {
                finalChar[i] = base64Bytes[i] switch
                {
                    ForwardSlashByte => Hyphen,
                    PlusByte => Underscore,
                    _ => (char)base64Bytes[i]
                };
            }

            return new string(finalChar); // allocate the heap memory
        }

        public static Guid Base64StringToGuid(ReadOnlySpan<char> id)
        {
            Span<char> idChars = stackalloc char[24];
            for(var i=0; i<22; i++)
            {
                idChars[i] = id[i] switch 
		                     { 
                                 Hyphen => ForwardSlashChar,
                                 Underscore => PlusChar,
                                 _ => id[i]
		                     }; 
	        }

            idChars[22] = EqualChar;
            idChars[23] = EqualChar;

	        Span<byte> idBytes = stackalloc byte[16];
	        Convert.TryFromBase64Chars(idChars, idBytes, out _);
            return new Guid(idBytes);
	    }
    }
}

