using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Rescue
{
    public class RBHeldItem : IClonable
    {

        public const int Length = 23;

        public const string MimeType = "application/x-rb-item";

        public static object FromStoredItemParts(int itemID, int parameter)
        {
            var output = new RBHeldItem();
            output.IsValid = true;
            output.ID = itemID;
            output.Parameter = parameter;
            return output;
        }

        public RBHeldItem()
        {
            IsValid = true;
            ID = 1;
            Parameter = 1;
        }

        public RBHeldItem(BitBlock bits)
        {
            Initialize(bits);
        }

        public void Initialize(BitBlock bits)
        {
            IsValid = bits.Bits[0];
            Flag1 = bits.Bits[1];
            Flag2 = bits.Bits[2];
            Flag3 = bits.Bits[3];
            Flag4 = bits.Bits[4];
            Flag5 = bits.Bits[5];
            Flag6 = bits.Bits[6];
            Flag7 = bits.Bits[7];
            Parameter = bits.GetInt(0, 8, 7);
            ID = bits.GetInt(0, 15, 8);
        }

        public bool IsValid { get; set; }
        private bool Flag1 { get; set; }
        private bool Flag2 { get; set; }
        private bool Flag3 { get; set; }
        private bool Flag4 { get; set; }
        private bool Flag5 { get; set; }
        private bool Flag6 { get; set; }
        private bool Flag7 { get; set; }
        public int ID { get; set; }

        /// <remarks>For sticks and other stackable items, this is the number in the stack.  For used TMs, this is the contained move</remarks>
        public int Parameter { get; set; }

        public override string ToString()
        {
            return Lists.RBItems[ID];
        }

        public object Clone()
        {
            var output = new RBHeldItem();
            output.IsValid = this.IsValid;
            output.Flag1 = this.Flag1;
            output.Flag2 = this.Flag2;
            output.Flag3 = this.Flag3;
            output.Flag4 = this.Flag4;
            output.Flag5 = this.Flag5;
            output.Flag6 = this.Flag6;
            output.Flag7 = this.Flag7;

            output.Parameter = this.Parameter;

            output.ID = this.ID;

            return output;
        }

        public int GetParameter()
        {
            return Parameter;
        }

        public BitBlock GetHeldItemBits()
        {
            var output = new BitBlock(Length);
            output.Bits[0] = IsValid;
            output.Bits[1] = Flag1;
            output.Bits[2] = Flag2;
            output.Bits[3] = Flag3;
            output.Bits[4] = Flag4;
            output.Bits[5] = Flag5;
            output.Bits[6] = Flag6;
            output.Bits[7] = Flag7;

            output.SetInt(0, 8, 7, this.Parameter);

            output.SetInt(0, 15, 8, ID);

            return output;
        }      
    }
}
