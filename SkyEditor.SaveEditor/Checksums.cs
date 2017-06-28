using SkyEditor.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

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
            for (int i = startIndex; i < endIndex; i += 4)
            {
                sum += bits.GetUInt(startIndex, 0, 32) & 0xFFFFFFFF;
            }
            return (uint)sum;
        }

        public static uint Calculate32BitChecksum(GenericFile file, int startIndex, int endIndex)
        {
            ulong sum = 0;
            for (int i = startIndex; i < endIndex; i += 4)
            {
                sum += file.ReadUInt32(startIndex) & 0xFFFFFFFF;
            }
            return (uint)sum;
        }
    }
}
