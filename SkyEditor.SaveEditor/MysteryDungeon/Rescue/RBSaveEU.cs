using SkyEditor.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Rescue
{
    public class RBSaveEU : RBSave
    {
        public class RBEUOffsets : RBSave.RBOffsets
        {
            // General
            public override int TeamNameStart => 0x4ECC * 8;
            public override int HeldMoneyOffset => 0x4E70 * 8;
            public override int StoredMoneyOffset => 0x4E73 * 8;
            public override int RescuePointsOffset => 0x4ED7 * 8;

            // Stored Items
            public override int StoredItemOffset => 0x4D2F * 8 - 2;

            // Held Items
            public override int HeldItemOffset => 0x4CF4 * 8;

            // Stored Pokemon
            public override int StoredPokemonOffset => (0x5B7 * 8 + 3) - (323 * 9);
        }

        public RBSaveEU() : base()
        {
            Offsets = new RBEUOffsets();
        }

        public RBSaveEU(IEnumerable<byte> rawData) : base(rawData)
        {
            Offsets = new RBEUOffsets();
        }

        /// <summary>
        /// Determines whether or not the given file is a save file for Pokémon Mystery Dungeon: Explorers of Time and Darkness.
        /// </summary>
        /// <param name="file">The file to be checked</param>
        /// <returns>A boolean indicating whether or not the given file is supported by this class</returns>
        public override async Task<bool> IsOfType(GenericFile file)
        {
            if (file.Length > Offsets.ChecksumEnd)
            {
                return await file.ReadUInt32Async(0) == (Checksums.Calculate32BitChecksum(file, 4, Offsets.ChecksumEnd) - 1);
            }
            else
            {
                return false;
            }
        }

        public override uint CalculatePrimaryChecksum()
        {
            return base.CalculatePrimaryChecksum() - 1;
        }

        public override uint CalculateSecondaryChecksum()
        {
            return base.CalculateSecondaryChecksum() - 1;
        }

        protected override void RecalculateChecksums()
        {
            base.RecalculateChecksums();
            PrimaryChecksum -= 1;
            SecondaryChecksum -= 1;
        }
    }
}
