using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkyQuicksaveAttack
    {
        public const int BitLength = 48;

        public SkyQuicksaveAttack()
        {
        }

        public SkyQuicksaveAttack(BitBlock bits)
        {
            IsValid = bits[0];
            IsLinked = bits[1];
            IsSwitched = bits[2];
            IsSet = bits[3];
            IsSealed = bits[4];
            Unknown = bits.GetRange(5, 11);
            ID = bits.GetInt(0, 16, 16);
            PP = bits.GetInt(0, 32, 8);
            PowerBoost = bits.GetInt(0, 40, 8);
        }

        public BitBlock ToBitBlock()
        {
            var bits = new BitBlock(BitLength);
            bits[0] = IsValid;
            bits[1] = IsLinked;
            bits[2] = IsSwitched;
            bits[3] = IsSet;
            bits[4] = IsSealed;
            bits.SetRange(5, 11, Unknown);
            bits.SetInt(0, 16, 16, ID);
            bits.SetInt(0, 32, 8, PP);
            bits.SetInt(0, 40, 8, PowerBoost);
            return bits;
        }

        private BitBlock Unknown { get; set; }

        /// <summary>
        /// Whether or not the current move is valid.
        /// </summary>
        /// <remarks>
        /// The IsValid flag indicates whether or not the item is actually a move.
        /// All moves have it set to true, and any move with this flag set to false is ignored by the game.
        /// If this flag is false, this move should be considered non-existant.
        /// </remarks>
        public bool IsValid { get; set; }

        /// <summary>
        /// Whether or not the move is linked with the next move
        /// </summary>
        public bool IsLinked { get; set; }

        /// <summary>
        /// Whether or not the move is enabled for AI to use
        /// </summary>
        public bool IsSwitched { get; set; }

        /// <summary>
        /// Whether or not the move is set as the default move
        /// </summary>
        /// <remarks>
        /// The default move can be used in-game by pressing L + A
        /// </remarks>
        public bool IsSet { get; set; }

        /// <summary>
        /// ID of the move
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Power boost of the move, resulting from drinking Ginseng
        /// </summary>
        public int PowerBoost { get; set; }

        /// <summary>
        /// Whether or not the move has been sealed, preventing its use
        /// </summary>
        public bool IsSealed { get; set; }

        /// <summary>
        /// Power points of the move
        /// </summary>
        public int PP { get; set; }

        public static explicit operator ExplorersAttack(SkyQuicksaveAttack attack)
        {
            return new ExplorersAttack
            {
                IsValid = attack.IsValid,
                IsLinked = attack.IsLinked,
                IsSwitched = attack.IsSwitched,
                IsSet = attack.IsSet,
                ID = attack.ID,
                PowerBoost = attack.PowerBoost
            };
        }

        public static explicit operator ExplorersActiveAttack(SkyQuicksaveAttack attack)
        {
            return new ExplorersActiveAttack
            {
                IsValid = attack.IsValid,
                IsLinked = attack.IsLinked,
                IsSwitched = attack.IsSwitched,
                IsSet = attack.IsSet,
                IsSealed = attack.IsSealed,
                ID = attack.ID,
                PP = attack.PP,
                PowerBoost = attack.PowerBoost
            };
        }
    }
}
