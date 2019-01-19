using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.Extensions
{
    public static class BitBlockExtensions
    {
        private static DSMysteryDungeonCharacterEncoding DSMysteryDungeonCharacterEncodingInstance = new DSMysteryDungeonCharacterEncoding();
        public static string GetStringPMD(this BitBlock binary, int byteIndex, int bitIndex, int byteLength)
        {
            return binary.GetString(byteIndex * 8 + bitIndex, byteLength, DSMysteryDungeonCharacterEncodingInstance);
        }

        public static void SetStringPMD(this BitBlock binary, int byteIndex, int bitIndex, int byteLength, string value)
        {
            binary.SetString(byteIndex * 8 + bitIndex, byteLength, DSMysteryDungeonCharacterEncodingInstance, value);
        }
    }
}
