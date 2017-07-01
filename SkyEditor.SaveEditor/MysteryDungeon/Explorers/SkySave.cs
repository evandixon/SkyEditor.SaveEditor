using SkyEditor.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkySave : BitBlockFile, IDetectableFileType
    {
        public class SkyOffsets
        {
            // Save framework (checksums, multi-save, etc.)
            public virtual int BackupSaveStart => 0xC800;
            public virtual int ChecksumEnd => 0xB65A;
            public virtual int QuicksaveStart => 0x19000;
            public virtual int QuicksaveChecksumStart => 0x19004;
            public virtual int QuicksaveChecksumEnd => 0x1E7FF;
        }

        public SkySave() : base()
        {
            Offsets = new SkyOffsets();
        }

        public SkySave(IEnumerable<byte> rawData) : base(rawData)
        {
            Offsets = new SkyOffsets();
            Init();
        }

        protected SkyOffsets Offsets { get; set; }

        #region Checksums

        /// <summary>
        /// Checksum of the primary save
        /// </summary>
        public uint PrimaryChecksum { get; set; }

        /// <summary>
        /// Checksum of the secondary save
        /// </summary>
        public uint SecondaryChecksum { get; set; }

        /// <summary>
        /// Checksum of the QuickSave
        /// </summary>
        public uint QuicksaveChecksum { get; set; }

        /// <summary>
        /// Calculates the checksum of the primary save
        /// </summary>
        public uint CalculatePrimaryChecksum()
        {
            return Checksums.Calculate32BitChecksum(Bits, 4, Offsets.ChecksumEnd);
        }

        /// <summary>
        /// Calculates the checksum of the backup save
        /// </summary>
        public uint CalculateSecondaryChecksum()
        {
            return Checksums.Calculate32BitChecksum(Bits, Offsets.BackupSaveStart + 4, Offsets.BackupSaveStart + Offsets.ChecksumEnd);
        }

        /// <summary>
        /// Calculates the checksum of the QuickSave
        /// </summary>
        public uint CalculateQuicksaveChecksum()
        {
            return Checksums.Calculate32BitChecksum(Bits, Offsets.QuicksaveChecksumStart, Offsets.QuicksaveChecksumEnd);
        }

        /// <summary>
        /// Determines whether or not the checksum of the primary save matches the primary save
        /// </summary>
        public bool IsPrimaryChecksumValid()
        {
            return PrimaryChecksum == CalculatePrimaryChecksum();
        }

        /// <summary>
        /// Determines whether or not the checksum of the backup save matches the backup save
        /// </summary>
        public bool IsSecondaryChecksumValid()
        {
            return SecondaryChecksum == CalculateSecondaryChecksum();
        }

        /// <summary>
        /// Determines whether or not the checksum of the QuickSave matches the QuickSave
        /// </summary>
        public bool IsQuickSaveChecksumValid()
        {
            return QuicksaveChecksum == CalculateQuicksaveChecksum();
        }

        /// <summary>
        /// Updates all checksums to match current save data
        /// </summary>
        protected void FixChecksums()
        {
            PrimaryChecksum = CalculatePrimaryChecksum();
            SecondaryChecksum = CalculateSecondaryChecksum();
            QuicksaveChecksum = CalculateQuicksaveChecksum();
        }

        #endregion

        #region Functions

        public override async Task OpenFile(string filename, IIOProvider provider)
        {
            await base.OpenFile(filename, provider);
            Init();
        }

        private void Init()
        {
            // Checksums
            PrimaryChecksum = Bits.GetUInt(0, 0, 32);
            SecondaryChecksum = Bits.GetUInt(Offsets.BackupSaveStart, 0, 32);
            QuicksaveChecksum = Bits.GetUInt(Offsets.QuicksaveStart, 0, 32);
            
            // Use the backup save if the first one's checksum is not valid
            // If both are invalid, use the first one
            var baseOffset = 0;
            if (!IsPrimaryChecksumValid() && IsSecondaryChecksumValid())
            {
                baseOffset = Offsets.BackupSaveStart;
            }
        }

        protected override void PreSave()
        {
            // Write properties to primary save
            FixChecksums();

            // Copy primary save to backup save
            Bits.SetRange(Offsets.BackupSaveStart + 4, Bits.GetRange(4, Offsets.BackupSaveStart - 4));
        }

        /// <summary>
        /// Determines whether or not the given file is a save file for Pokémon Mystery Dungeon: Explorers of Sky.
        /// </summary>
        /// <param name="file">The file to be checked</param>
        /// <returns>A boolean indicating whether or not the given file is supported by this class</returns>
        /// <remarks>US and EU regions are supported. JP is unknown.</remarks>
        public async Task<bool> IsOfType(GenericFile file)
        {
            if (file.Length > Offsets.ChecksumEnd)
            {
                return await file.ReadUInt32Async(0) == Checksums.Calculate32BitChecksum(file, 4, Offsets.ChecksumEnd);
            }
            else
            {
                return false;
            }
        }

        public override byte[] ToByteArray()
        {
            PreSave();
            return base.ToByteArray();
        }

        #endregion
    }
}
