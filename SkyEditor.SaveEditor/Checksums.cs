﻿using SkyEditor.IO;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    /// <summary>
    /// Utility class to calculate checksums
    /// </summary>
    public class Checksums
    {
        public static uint Calculate32BitChecksum(BitBlock bits, int startIndex, int endIndex)
        {
            ulong sum = 0;
            for (int i = startIndex; i <= endIndex; i += 4)
            {
                sum += bits.GetUInt(i, 0, 32) & 0xFFFFFFFF;
            }
            return (uint)(sum & 0xFFFFFFFF);
        }

        public static uint Calculate32BitChecksum(IReadOnlyBinaryDataAccessor data, int startIndex, int endIndex)
        {
            ulong sum = 0;
            for (int i = startIndex; i <= endIndex; i += 4)
            {
                sum += data.ReadUInt32(i) & 0xFFFFFFFF;
            }
            return (uint)(sum & 0xFFFFFFFF);
        }

        public static async Task<uint> Calculate32BitChecksumAsync(IReadOnlyBinaryDataAccessor data, int startIndex, int endIndex)
        {
            ulong sum = 0;
            for (int i = startIndex; i <= endIndex; i += 4)
            {
                sum += await data.ReadUInt32Async(i) & 0xFFFFFFFF;
            }
            return (uint)(sum & 0xFFFFFFFF);
        }
    }
}
