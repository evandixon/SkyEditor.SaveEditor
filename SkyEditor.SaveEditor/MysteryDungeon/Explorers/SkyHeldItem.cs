using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkyHeldItem : ExplorersItem
    {
        public SkyHeldItem()
        {
            IsValid = true;
        }

        public SkyHeldItem(int id, int parameter) : base(id, parameter)
        {
            IsValid = true;
        }

        public SkyHeldItem(BitBlock bits)
        { 
            bits.Position = 0;
            IsValid = bits[0];
            Flag1 = bits[1];
            Flag2 = bits[2];
            Flag3 = bits[3];
            Flag4 = bits[4];
            Flag5 = bits[5];
            Flag6 = bits[6];
            Flag7 = bits[7];
            Parameter = bits.GetInt(0, 8, 11);
            ID = bits.GetInt(0, 19, 11);

            var heldBy = bits.GetInt(0, 30, 3);
            switch (heldBy)
            {
                case 0:
                    Holder = ItemHolder.None;
                    break;
                case 1:
                    Holder = ItemHolder.TeamMember1;
                    break;
                case 2:
                    Holder = ItemHolder.TeamMember2;
                    break;
                case 3:
                    Holder = ItemHolder.TeamMember3;
                    break;
                case 4:
                    Holder = ItemHolder.TeamMember4;
                    break;
                default:
                    throw new ArgumentException("Invalid item holder: " + heldBy.ToString());
            }
        }

        public BitBlock ToBitBlock()
        {
            var bits = new BitBlock(33);
            bits[0] = IsValid;
            bits[1] = Flag1;
            bits[2] = Flag2;
            bits[3] = Flag3;
            bits[4] = Flag4;
            bits[5] = Flag5;
            bits[6] = Flag6;
            bits[7] = Flag7;

            bits.SetInt(0, 8, 11, Parameter);
            bits.SetInt(0, 19, 11, ID);
            bits.SetInt(0, 30, 3, (int)Holder);

            return bits;
        }

        /// <summary>
        /// Whether or not the current item is valid.
        /// </summary>
        /// <remarks>
        /// The IsValid flag indicates whether or not the item is actually an item.
        /// All logical items have it set to true, and any item with this flag set to false is ignored by the game.
        /// If this flag is false, this item should be considered non-existant.
        /// </remarks>
        public bool IsValid { get; set; }
        protected bool Flag1 { get; set; }
        protected bool Flag2 { get; set; }
        protected bool Flag3 { get; set; }
        protected bool Flag4 { get; set; }
        protected bool Flag5 { get; set; }
        protected bool Flag6 { get; set; }
        protected bool Flag7 { get; set; }

        public ItemHolder Holder { get; set; }

        public override object Clone()
        {
            return new SkyHeldItem(this.ToBitBlock());
        }

        public override bool Equals(object obj)
        {
            return obj is SkyHeldItem && ToBitBlock().Bits.SequenceEqual((obj as SkyHeldItem).ToBitBlock().Bits);
        }

        public override int GetHashCode()
        {
            return ID ^ Parameter ^ (int)Holder ^ (Flag1 ? 1 : 0) ^ (Flag2 ? 2 : 0) ^ (Flag3 ? 4 : 0) ^ (Flag4 ? 8 : 0) ^ (Flag5 ? 16 : 0) ^ (Flag6 ? 32 : 0) ^ (Flag7 ? 64 : 0);
        }

        public static bool operator ==(SkyHeldItem x, SkyHeldItem y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(SkyHeldItem x, SkyHeldItem y)
        {
            return (!(x == y));
        }
    }
}
