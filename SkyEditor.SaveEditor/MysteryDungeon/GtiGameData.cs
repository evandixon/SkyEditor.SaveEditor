using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SkyEditor.Core.IO;
using SkyEditor.IO.FileSystem;

namespace SkyEditor.SaveEditor.MysteryDungeon
{
    public class GtiGameData : BitBlockFile
    {
        public class GtiOffsets
        {
            public virtual int HeldItemsOffset => 0x20A * 8 + 2;
        }

        public GtiGameData()
        {
            Offsets = new GtiOffsets();
        }

        public override async Task OpenFile(string filename, IFileSystem provider)
        {
            await base.OpenFile(filename, provider);
            OriginalChecksum = CalculateChecksum();
        }

        protected GtiOffsets Offsets { get; set; }

        protected byte OriginalChecksum { get; set; }

        private int FindChecksumBitOffset()
        {
            int offset = -1;
            for (int i = Bits.Count - 120 - 1; i <= Bits.Count - 100 - 1; i++)
            {
                if (Bits[i])
                {
                    offset = i + 96;
                    break;
                }
            }
            if (offset > -1)
            {
                return offset;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        public byte StoredChecksum
        {
            get { return (byte)Bits.GetInt(0, FindChecksumBitOffset(), 8); }
            set { Bits.SetInt(0, FindChecksumBitOffset(), 8, value); }
        }
        public byte CalculateChecksum()
        {
            long sum = 0;
            for (int i = 0; i <= FindChecksumBitOffset() - 1; i++)
            {
                if (Bits[i])
                {
                    sum += 1;
                }
            }
            var sum1 = sum & 0xff;
            var sum2 = sum >> 8 & 0xff;
            switch (sum2)
            {
                case 0x9b:
                    if (sum1 + 0x22 <= 255)
                    {
                        sum1 += 0x22;
                    }
                    break;
                case 0x99:
                    if (sum1 + 5 <= 255)
                    {
                        sum1 += 5;
                    }
                    break;
                case 0x97:
                    if (sum1 > 32)
                    {
                        sum1 -= 32;
                    }
                    break;
            }
            //If sum = 182 Then
            //    sum += 5
            //ElseIf sum = 93 Then
            //    sum += 125
            //ElseIf sum + 189 < 255 Then
            //    sum += 189
            //End If
            return (byte)sum1;
        }
        protected override void PreSave()
        {
            base.PreSave();
            StoredChecksum = (byte)(StoredChecksum + (CalculateChecksum() - OriginalChecksum) & 0xff);
        }
        public int HeldMoney
        {
            get { return Bits.GetInt(5, 0, 16); }
            set { Bits.SetInt(5, 0, 16, value); }
        }
        public int CompanionHeldMoney
        {
            get { return Bits.GetInt(9, 0, 16); }
            set { Bits.SetInt(9, 0, 16, value); }
        }
    }
}
