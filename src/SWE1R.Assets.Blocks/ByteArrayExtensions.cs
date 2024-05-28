// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Security.Cryptography;

namespace SWE1R.Assets.Blocks
{
    public static class ByteArrayExtensions
    {
        /// <summary>Looks for the next occurrence of a sequence in a byte array</summary>
        /// <param name="bytes">Array that will be scanned</param>
        /// <param name="start">Index in the array at which scanning will begin</param>
        /// <param name="sequence">Sequence the array will be scanned for</param>
        /// <returns>
        ///   The index of the next occurrence of the sequence of -1 if not found
        /// </returns>
        public static int IndexOf(this byte[] bytes, int start, byte[] sequence)
        {
            // https://stackoverflow.com/a/39021296

            int end = bytes.Length - sequence.Length; // past here no match is possible
            byte firstByte = sequence[0]; // cached to tell compiler there's no aliasing

            while (start < end)
            {
                // scan for first byte only. compiler-friendly.
                if (bytes[start] == firstByte)
                    // scan for rest of sequence
                    for (int offset = 1; offset < sequence.Length; ++offset)
                        if (bytes[start + offset] != sequence[offset])
                            break; // mismatch? continue scanning with next byte
                        else if (offset == sequence.Length - 1)
                            return start; // all bytes matched!
                ++start;
            }

            // end of array reached without match
            return -1;
        }

        public static byte[] GetSha1(this byte[] bytes)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
                return sha1.ComputeHash(bytes);
        }
    }
}
