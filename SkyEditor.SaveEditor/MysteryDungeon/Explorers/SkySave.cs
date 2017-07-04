﻿using SkyEditor.Core.IO;
using SkyEditor.SaveEditor.Extensions;
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

            // General
            public virtual int TeamNameStart => 0x994E * 8;
            public virtual int TeamNameLength => 10;
            public virtual int HeldMoney => 0x990C * 8 + 6;
            public virtual int SpEpisodeHeldMoney => 0x990F * 8 + 6;
            public virtual int StoredMoney => 0x9915 * 8 + 6;
            public virtual int NumberOfAdventures => 0x8B70 * 8;
            public virtual int ExplorerRank => 0x9958 * 8;

            // Items
            public virtual int StoredItemOffset => 0x8E0C * 8 + 6;
            public virtual int HeldItemOffset => 0x8BA2 * 8;
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
        protected void RecalculateChecksums()
        {
            PrimaryChecksum = CalculatePrimaryChecksum();
            SecondaryChecksum = CalculateSecondaryChecksum();
            QuicksaveChecksum = CalculateQuicksaveChecksum();
        }

        #endregion

        #region General
        /// <summary>
        /// The team name of the main game's exploration team
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// The money held by the player in the main game
        /// </summary>
        public int HeldMoney { get; set; }

        /// <summary>
        /// The money held in the special episode
        /// </summary>
        public int SpEpisodeHeldMoney { get; set; }

        /// <summary>
        /// The money stored in the Duskall bank
        /// </summary>
        public int StoredMoney { get; set; }

        /// <summary>
        /// The number of adventures had by the main team
        /// </summary>
        public int NumberOfAdventures { get; set; }

        /// <summary>
        /// The rank points held by the main game's exploration team
        /// </summary>
        public int ExplorerRankPoints { get; set; }

        /// <summary>
        /// The rank held by the main game's exploration team
        /// </summary>
        /// <remarks>This proeprty wraps <see cref="ExplorerRankPoints"/>, so setting this property will reduce the number of explorer rank points held by the team.</remarks>
        public SkyExplorerRank ExplorerRank
        {
            get
            {
                if (ExplorerRankPoints >= 100000)
                    return SkyExplorerRank.Guildmaster;
                else if (ExplorerRankPoints >= 25000)
                    return SkyExplorerRank.Master3Star;
                else if (ExplorerRankPoints >= 21000)
                    return SkyExplorerRank.Master2Star;
                else if (ExplorerRankPoints >= 17000)
                    return SkyExplorerRank.Master1Star;
                else if (ExplorerRankPoints >= 13500)
                    return SkyExplorerRank.Master;
                else if (ExplorerRankPoints >= 10500)
                    return SkyExplorerRank.Hyper;
                else if (ExplorerRankPoints >= 7500)
                    return SkyExplorerRank.Ultra;
                else if (ExplorerRankPoints >= 5000)
                    return SkyExplorerRank.Super;
                else if (ExplorerRankPoints >= 3200)
                    return SkyExplorerRank.Diamond;
                else if (ExplorerRankPoints >= 1600)
                    return SkyExplorerRank.Gold;
                else if (ExplorerRankPoints >= 400)
                    return SkyExplorerRank.Silver;
                else if (ExplorerRankPoints >= 100)
                    return SkyExplorerRank.Bronze;
                else
                    return SkyExplorerRank.Normal;
            }
            set
            {
                ExplorerRankPoints = (int)value;
            }
        }

        private void LoadGeneral(int baseOffset)
        {
            TeamName = Bits.GetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength);
            HeldMoney = Bits.GetInt(0, Offsets.HeldMoney, 24);
            SpEpisodeHeldMoney = Bits.GetInt(0, Offsets.SpEpisodeHeldMoney, 24);
            StoredMoney = Bits.GetInt(0, Offsets.StoredMoney, 24);
            NumberOfAdventures = Bits.GetInt(0, Offsets.NumberOfAdventures, 32);
            ExplorerRankPoints = Bits.GetInt(0, Offsets.ExplorerRank, 32);
        }

        private void SaveGeneral()
        {
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName);
            Bits.SetInt(0, Offsets.HeldMoney, 24, HeldMoney);
            Bits.SetInt(0, Offsets.SpEpisodeHeldMoney, 24,SpEpisodeHeldMoney);
            Bits.SetInt(0, Offsets.StoredMoney, 24, StoredMoney);
            Bits.SetInt(0, Offsets.NumberOfAdventures, 32, NumberOfAdventures);
            Bits.SetInt(0, Offsets.ExplorerRank, 32,ExplorerRankPoints);
        }

        #endregion

        #region Items
        /// <summary>
        /// The items stored in Kangaskhan's warehouse
        /// </summary>
        public List<SkyItem> StoredItems { get; set; }

        /// <summary>
        /// The items in the bag in the main game
        /// </summary>
        public List<SkyHeldItem> HeldItems { get; set; }

        /// <summary>
        /// The items in the bag in the special episode
        /// </summary>
        public List<SkyHeldItem> SpEpisodeHeldItems { get; set; }

        /// <summary>
        /// The items in the bag in friend rescue
        /// </summary>
        public List<SkyHeldItem> FriendRescueHeldItems { get; set; }

        private void LoadItems(int baseOffset)
        {
            // Stored items
            StoredItems = new List<SkyItem>();
            var ids = Bits.GetRange(baseOffset + Offsets.StoredItemOffset, 11 * 1000);
            var parameters = Bits.GetRange(baseOffset + Offsets.StoredItemOffset + (11 * 1000), 11 * 1000);
            for (int i = 0; i < 1000; i++)
            {
                var id = ids.GetNextInt(11);
                var param = parameters.GetNextInt(11);
                if (id > 0)
                {
                    StoredItems.Add(new SkyItem(id, param));
                }
                else
                {
                    break;
                }
            }

            // Held Items
            HeldItems = new List<SkyHeldItem>();
            SpEpisodeHeldItems = new List<SkyHeldItem>();
            FriendRescueHeldItems = new List<SkyHeldItem>();

            // - Main Game
            for (int i = 0; i < 50; i++)
            {
                var item = new SkyHeldItem(Bits.GetRange(baseOffset + Offsets.HeldItemOffset + (i * 33), 33));
                if (item.IsValid)
                {
                    HeldItems.Add(item);
                }
                else
                {
                    break;
                }
            }

            // - Sp. Episode
            for (int i = 50; i < 100; i++)
            {
                var item = new SkyHeldItem(Bits.GetRange(baseOffset + Offsets.HeldItemOffset + (i * 33), 33));
                if (item.IsValid)
                {
                    SpEpisodeHeldItems.Add(item);
                }
                else
                {
                    break;
                }
            }

            // - Friend Rescue
            for (int i = 100; i < 150; i++)
            {
                var item = new SkyHeldItem(Bits.GetRange(baseOffset + Offsets.HeldItemOffset + (i * 33), 33));
                if (item.IsValid)
                {
                    FriendRescueHeldItems.Add(item);
                }
                else
                {
                    break;
                }
            }
        }

        private void SaveItems()
        {
            // Stored items
            var ids = new BitBlock(11 * 1000);
            var parameters = new BitBlock(11 * 1000);
            for (int i = 0; i < 1000; i++)
            {
                if (StoredItems.Count > i)
                {
                    ids.SetNextInt(11, StoredItems[i].ID);
                    parameters.SetNextInt(11, StoredItems[i].Parameter);
                }
                else
                {
                    ids.SetNextInt(11, 0);
                    parameters.SetNextInt(11, 0);
                }
            }
            Bits.SetRange(Offsets.StoredItemOffset, 11 * 1000, ids);
            Bits.SetRange(Offsets.StoredItemOffset + 11 * 1000, 11 * 1000, parameters);

            // Held items
            for (int i = 0; i < 50; i++)
            {
                var index = Offsets.HeldItemOffset + i * 33;
                if (HeldItems.Count > i)
                {
                    Bits.SetRange(index, 33, HeldItems[i].ToBitBlock());
                }
                else
                {
                    Bits.SetRange(index, 33, new BitBlock(33));
                }
            }

            // Sp. Episode Held items
            for (int i = 0; i < 50; i++)
            {
                var index = Offsets.HeldItemOffset + (i + 50) * 33;
                if (SpEpisodeHeldItems.Count > i)
                {
                    Bits.SetRange(index, 33, SpEpisodeHeldItems[i].ToBitBlock());
                }
                else
                {
                    Bits.SetRange(index, 33, new BitBlock(33));
                }
            }

            // Friend RescueHeld items
            for (int i = 0; i < 50; i++)
            {
                var index = Offsets.HeldItemOffset + (i + 100) * 33;
                if (HeldItems.Count > i)
                {
                    Bits.SetRange(index, 33, HeldItems[i].ToBitBlock());
                }
                else
                {
                    Bits.SetRange(index, 33, new BitBlock(33));
                }
            }
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

            LoadGeneral(baseOffset);
            LoadItems(baseOffset);
        }

        protected override void PreSave()
        {
            SaveGeneral();
            SaveItems();

            // Copy primary save to backup save
            Bits.SetRange(Offsets.BackupSaveStart + 4, Bits.GetRange(4, Offsets.BackupSaveStart - 4));

            // Checksums
            RecalculateChecksums();
            Bits.SetUInt(0, 0, 32, PrimaryChecksum);
            Bits.SetUInt(Offsets.BackupSaveStart, 0, 32, SecondaryChecksum);
            Bits.SetUInt(Offsets.QuicksaveStart, 0, 32, QuicksaveChecksum);            
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
