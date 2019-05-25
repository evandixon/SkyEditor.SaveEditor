using SkyEditor.Core.IO;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Rescue
{
    public class RBSave : BitBlockFile
    {
        public class RBOffsets
        {
            // Checksums
            public virtual int ChecksumEnd => 0x57D0;
            public virtual int BackupSaveStart => 0x6000;

            // General
            public virtual int TeamNameStart => 0x4EC8 * 8;
            public virtual int TeamNameLength => 10;
            public virtual int BaseTypeOffset => 0x67 * 8;
            public virtual int HeldMoneyOffset => 0x4E6C * 8;
            public virtual int HeldMoneyLength => 24;
            public virtual int StoredMoneyOffset => 0x4E6F * 8;
            public virtual int StoredMoneyLength => 24;
            public virtual int RescuePointsOffset => 0x4ED3 * 8;
            public virtual int RescuePointsLength => 32;

            // Stored Items
            public virtual int StoredItemOffset => 0x4D2B * 8 - 2;
            public virtual int StoredItemCount => 239;

            // Held Items
            public virtual int HeldItemOffset => 0x4CF0 * 8;
            public virtual int HeldItemCount => 20;
            public virtual int HeldItemLength => 23;

            // Stored Pokemon
            public virtual int StoredPokemonOffset => (0x5B3 * 8 + 3) - (323 * 9);
            public virtual int StoredPokemonLength => 323;
            public virtual int StoredPokemonCount => 407 + 6;
        }

        public RBSave()
        {
            Offsets = new RBOffsets();
        }

        public RBSave(IEnumerable<byte> rawData) : base(rawData)
        {
            Offsets = new RBOffsets();
            Init();
        }

        public RBOffsets Offsets { get; set; }

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
        public virtual uint CalculatePrimaryChecksum()
        {
            return Checksums.Calculate32BitChecksum(Bits, 4, Offsets.ChecksumEnd);
        }

        /// <summary>
        /// Calculates the checksum of the backup save
        /// </summary>
        public virtual uint CalculateSecondaryChecksum()
        {
            return Checksums.Calculate32BitChecksum(Bits, Offsets.BackupSaveStart + 4, Offsets.BackupSaveStart + Offsets.ChecksumEnd);
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
        /// Updates all checksums to match current save data
        /// </summary>
        protected virtual void RecalculateChecksums()
        {
            PrimaryChecksum = CalculatePrimaryChecksum();
            SecondaryChecksum = CalculateSecondaryChecksum();
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
        /// The money stored in the bank
        /// </summary>
        public int StoredMoney { get; set; }

        /// <summary>
        /// The rank points held by the main game's rescue team
        /// </summary>
        public int RescueTeamPoints { get; set; }

        public int BaseType { get; set; }

        ///// <summary>
        ///// The rank held by the main game's exploration team
        ///// </summary>
        ///// <remarks>This proeprty wraps <see cref="ExplorerRankPoints"/>, so setting this property will reduce the number of explorer rank points held by the team.</remarks>
        //public TDExplorerRank ExplorerRank
        //{
        //    get
        //    {
        //        if (ExplorerRankPoints >= 62500)
        //            return TDExplorerRank.Master;
        //        else if (ExplorerRankPoints >= 15000)
        //            return TDExplorerRank.Hyper;
        //        else if (ExplorerRankPoints >= 10000)
        //            return TDExplorerRank.Ultra;
        //        else if (ExplorerRankPoints >= 6000)
        //            return TDExplorerRank.Super;
        //        else if (ExplorerRankPoints >= 3200)
        //            return TDExplorerRank.Diamond;
        //        else if (ExplorerRankPoints >= 1600)
        //            return TDExplorerRank.Gold;
        //        else if (ExplorerRankPoints >= 400)
        //            return TDExplorerRank.Silver;
        //        else if (ExplorerRankPoints >= 100)
        //            return TDExplorerRank.Bronze;
        //        else
        //            return TDExplorerRank.Normal;
        //    }
        //    set
        //    {
        //        ExplorerRankPoints = (int)value;
        //    }
        //}

        private void LoadGeneral(int baseOffset)
        {
            TeamName = Bits.GetStringPMD(0, baseOffset + Offsets.TeamNameStart, Offsets.TeamNameLength);
            HeldMoney = Bits.GetInt(0, Offsets.HeldMoneyOffset, Offsets.HeldMoneyLength);
            StoredMoney = Bits.GetInt(0, Offsets.StoredMoneyOffset, Offsets.StoredMoneyLength);
            RescueTeamPoints = Bits.GetInt(0, Offsets.RescuePointsOffset, Offsets.RescuePointsLength);
            BaseType = Bits.GetInt(0, Offsets.BaseTypeOffset, 8);
        }

        private void SaveGeneral()
        {
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName);
            Bits.SetInt(0, Offsets.HeldMoneyOffset, Offsets.HeldMoneyLength, HeldMoney);
            Bits.SetInt(0, Offsets.StoredMoneyOffset, Offsets.StoredMoneyLength, StoredMoney);
            Bits.SetInt(0, Offsets.RescuePointsOffset, Offsets.RescuePointsLength, RescueTeamPoints);
            Bits.SetInt(0, Offsets.BaseTypeOffset, 8, BaseType);
        }

        #endregion

        #region Items

        /// <summary>
        /// The items stored in Kangaskhan's warehouse
        /// </summary>
        public List<RBStoredItem> StoredItems { get; set; }

        /// <summary>
        /// The items in the bag in the main game
        /// </summary>
        public List<RBHeldItem> HeldItems { get; set; }

        private void LoadItems(int baseOffset)
        {
            // Stored items
            StoredItems = new List<RBStoredItem>();
            var block = Bits.GetRange(Offsets.StoredItemOffset, Offsets.StoredItemCount * 10);
            for (int i = 0; i < Offsets.StoredItemCount; i++)
            {
                var quantity = block.GetNextInt(10);
                if (quantity > 0)
                {
                    StoredItems.Add(new RBStoredItem(i + 1, quantity));
                }
            }

            // Held Items
            HeldItems = new List<RBHeldItem>();

            // - Main Game
            for (int i = 0; i < 50; i++)
            {
                var item = new RBHeldItem(Bits.GetRange(baseOffset + Offsets.HeldItemOffset + (i * 33), 33));
                if (item.IsValid)
                {
                    HeldItems.Add(item);
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
            var compiledItems = new Dictionary<int, int>(); // Key = item ID, value = quantity
            // - Combine the quantities
            foreach (var item in StoredItems)
            {
                if (!compiledItems.ContainsKey(item.ItemID))
                {
                    compiledItems.Add(item.ItemID, 0);
                }
                compiledItems[item.ItemID] = Math.Min(item.Quantity + compiledItems[item.ItemID], 1024);
            }
            // - Update the save
            var block = new BitBlock(Offsets.StoredItemCount * 10);
            for (int i = 0;i<Offsets.StoredItemCount;i++)
            {
                if (compiledItems.ContainsKey(i+1))
                {
                    block.SetNextInt(10, compiledItems[i + 1]);
                }
                else
                {
                    block.SetNextInt(10, 0);
                }
            }
            Bits.SetRange(Offsets.StoredItemOffset, Offsets.StoredItemCount * 10, block);

            // Held items
            for (int i = 0; i < 50; i++)
            {
                var index = Offsets.HeldItemOffset + i * 33;
                if (HeldItems.Count > i)
                {
                    Bits.SetRange(index, 33, HeldItems[i].GetHeldItemBits());
                }
                else
                {
                    Bits.SetRange(index, 33, new BitBlock(33));
                }
            }
        }
        #endregion

        #region Stored Pokemon

        private void LoadStoredPokemon(int baseOffset)
        {
            StoredPokemon = new List<RBStoredPokemon>();
            for (int i = 0; i < Offsets.StoredPokemonCount; i++)
            {
                var pkm = new RBStoredPokemon(Bits.GetRange(baseOffset + Offsets.StoredPokemonOffset + i * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength));

                if (pkm.ID <= 0)
                {
                    break;
                }

                StoredPokemon.Add(pkm);
            }
        }

        private void SaveStoredPokemon()
        {
            for (int i = 0; i < Offsets.StoredPokemonCount; i++)
            {
                if (StoredPokemon.Count > i)
                {
                    Bits.SetRange(Offsets.StoredPokemonOffset + i * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength, StoredPokemon[i].GetStoredPokemonBits());
                }
                else
                {
                    Bits.SetRange(Offsets.StoredPokemonOffset + i * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength, new BitBlock(Offsets.StoredPokemonLength));
                }
            }
        }

        public List<RBStoredPokemon> StoredPokemon { get; set; }

        #endregion

        #region Functions

        public override async Task OpenFile(string filename, IFileSystem provider)
        {
            await base.OpenFile(filename, provider);
            Init();
        }

        private void Init()
        {
            // Checksums
            PrimaryChecksum = Bits.GetUInt(0, 0, 32);
            SecondaryChecksum = Bits.GetUInt(Offsets.BackupSaveStart, 0, 32);

            // Use the backup save if the first one's checksum is not valid
            // If both are invalid, use the first one
            var baseOffset = 0;
            if (!IsPrimaryChecksumValid() && IsSecondaryChecksumValid())
            {
                baseOffset = Offsets.BackupSaveStart;
            }

            LoadGeneral(baseOffset);
            LoadItems(baseOffset);
            LoadStoredPokemon(baseOffset);
        }

        protected override void PreSave()
        {
            SaveGeneral();
            SaveItems();
            SaveStoredPokemon();

            // Copy primary save to backup save
            Bits.SetRange(Offsets.BackupSaveStart + 4, Bits.GetRange(4, Offsets.BackupSaveStart - 4));

            // Checksums
            RecalculateChecksums();
            Bits.SetUInt(0, 0, 32, PrimaryChecksum);
            Bits.SetUInt(Offsets.BackupSaveStart, 0, 32, SecondaryChecksum);
        }

        /// <summary>
        /// Determines whether or not the given file is a save file for Pokémon Mystery Dungeon: Explorers of Time and Darkness.
        /// </summary>
        /// <param name="file">The file to be checked</param>
        /// <returns>A boolean indicating whether or not the given file is supported by this class</returns>
        public virtual async Task<bool> IsOfType(GenericFile file)
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
