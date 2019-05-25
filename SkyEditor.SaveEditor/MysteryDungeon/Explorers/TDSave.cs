using SkyEditor.Core.IO;
using SkyEditor.Core.IO.PluginInfrastructure;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class TDSave : BitBlockFile, IDetectableFileType
    {
        public class TDOffsets
        {
            // Checksums
            public virtual int ChecksumEnd => 0xDC7B;
            public virtual int BackupSaveStart => 0x10000;
            public virtual int QuicksaveStart => 0x2E000;
            public virtual int QuicksaveChecksumStart => 0x2E004;
            public virtual int QuicksaveChecksumEnd => 0x2E0FF;

            // General
            public virtual int TeamNameStart => 0x96F7 * 8;
            public virtual int TeamNameLength => 10;

            // Held Items
            public virtual int HeldItemOffset => 0x8B71 * 8;
            public virtual int HeldItemCount => 48;
            public virtual int HeldItemLength => 31;

            // Stored Pokemon
            public virtual int StoredPokemonOffset => 0x460 * 8 + 3;
            public virtual int StoredPokemonLength => 388;
            public virtual int StoredPokemonCount => 550;

            // Active Pokemon
            public virtual int ActivePokemonOffset => 0x83CB * 8;
            public virtual int ActivePokemonLength => 544;
            public virtual int ActivePokemonCount => 4;
        }

        public TDSave()
        {
            Offsets = new TDOffsets();
        }

        public TDSave(IEnumerable<byte> rawData) : base(rawData)
        {
            Offsets = new TDOffsets();
            Init();
        }

        public TDOffsets Offsets { get; set; }

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
        public void RecalculateChecksums()
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

        ///// <summary>
        ///// The money held by the player in the main game
        ///// </summary>
        //public int HeldMoney { get; set; }

        ///// <summary>
        ///// The money held in the special episode
        ///// </summary>
        //public int SpEpisodeHeldMoney { get; set; }

        ///// <summary>
        ///// The money stored in the Duskall bank
        ///// </summary>
        //public int StoredMoney { get; set; }

        ///// <summary>
        ///// The number of adventures had by the main team
        ///// </summary>
        //public int NumberOfAdventures { get; set; }

        ///// <summary>
        ///// The rank points held by the main game's exploration team
        ///// </summary>
        //public int ExplorerRankPoints { get; set; }

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
        }

        private void SaveGeneral()
        {
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName);
        }

        #endregion

        #region Items

        ///// <summary>
        ///// The items stored in Kangaskhan's warehouse
        ///// </summary>
        //public List<TDHeldItem> StoredItems { get; set; }

        /// <summary>
        /// The items in the bag in the main game
        /// </summary>
        public List<TDHeldItem> HeldItems { get; set; }

        private void LoadItems(int baseOffset)
        {
            //// Stored items
            //StoredItems = new List<TDHeldItem>();
            //var ids = Bits.GetRange(baseOffset * 8 + Offsets.StoredItemOffset, 11 * 1000);
            //var parameters = Bits.GetRange(baseOffset * 8 + Offsets.StoredItemOffset + (11 * 1000), 11 * 1000);
            //for (int i = 0; i < 1000; i++)
            //{
            //    var id = ids.GetNextInt(11);
            //    var param = parameters.GetNextInt(11);
            //    if (id > 0)
            //    {
            //        StoredItems.Add(new TDHeldItem(id, param));
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            // Held Items
            HeldItems = new List<TDHeldItem>();

            // - Main Game
            for (int i = 0; i < 50; i++)
            {
                var item = new TDHeldItem(Bits.GetRange(baseOffset * 8 + Offsets.HeldItemOffset + (i * Offsets.HeldItemLength), Offsets.HeldItemLength));
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
            //// Stored items
            //var ids = new BitBlock(11 * 1000);
            //var parameters = new BitBlock(11 * 1000);
            //for (int i = 0; i < 1000; i++)
            //{
            //    if (StoredItems.Count > i)
            //    {
            //        ids.SetNextInt(11, StoredItems[i].ID);
            //        parameters.SetNextInt(11, StoredItems[i].Parameter);
            //    }
            //    else
            //    {
            //        ids.SetNextInt(11, 0);
            //        parameters.SetNextInt(11, 0);
            //    }
            //}
            //Bits.SetRange(Offsets.StoredItemOffset, 11 * 1000, ids);
            //Bits.SetRange(Offsets.StoredItemOffset + 11 * 1000, 11 * 1000, parameters);

            // Held items
            for (int i = 0; i < 50; i++)
            {
                var index = Offsets.HeldItemOffset + i * Offsets.HeldItemLength;
                if (HeldItems.Count > i)
                {
                    Bits.SetRange(index, Offsets.HeldItemLength, HeldItems[i].ToBitBlock());
                }
                else
                {
                    Bits.SetRange(index, Offsets.HeldItemLength, new BitBlock(Offsets.HeldItemLength));
                }
            }           
        }
        #endregion

        #region Stored Pokemon

        private void LoadStoredPokemon(int baseOffset)
        {
            StoredPokemon = new List<TDStoredPokemon>();
            for (int i = 0; i < Offsets.StoredPokemonCount; i++)
            {
                var pkm = new TDStoredPokemon(Bits.GetRange(baseOffset * 8 + Offsets.StoredPokemonOffset + i * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength));

                if (!pkm.IsValid)
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

        public List<TDStoredPokemon> StoredPokemon { get; set; }

        #endregion

        #region Active Pokemon

        private void LoadActivePokemon(int baseOffset)
        {
            ActivePokemon = new List<TDActivePokemon>();
            for (int i = 0; i < Offsets.ActivePokemonCount; i++)
            {
                var main = new TDActivePokemon(Bits.GetRange(baseOffset * 8 + Offsets.ActivePokemonOffset + i * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength));
                
                if (main.IsValid)
                {
                    ActivePokemon.Add(main);
                }
            }
        }

        private void SaveActivePokemon()
        {
            // Write the Active Pokemon
            for (int i = 0; i < Offsets.ActivePokemonCount; i++)
            {
                if (ActivePokemon.Count > i)
                {
                    Bits.SetRange(Offsets.ActivePokemonOffset + i * Offsets.ActivePokemonLength, ActivePokemon[i].GetActivePokemonBits());
                }
                else
                {
                    Bits.SetRange(Offsets.ActivePokemonOffset + i * Offsets.ActivePokemonLength, new BitBlock(Offsets.ActivePokemonLength));
                }
            }
        }

        private int ActivePokemon1RosterIndex { get; set; }
        private int ActivePokemon2RosterIndex { get; set; }
        private int ActivePokemon3RosterIndex { get; set; }
        private int ActivePokemon4RosterIndex { get; set; }
        public List<TDActivePokemon> ActivePokemon { get; set; }
        public List<TDActivePokemon> SpEpisodeActivePokemon { get; set; }

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
            LoadStoredPokemon(baseOffset);
            LoadActivePokemon(baseOffset);
        }

        protected override void PreSave()
        {
            SaveGeneral();
            SaveItems();
            SaveStoredPokemon();
            SaveActivePokemon();

            // Copy primary save to backup save
            Bits.SetRange(Offsets.BackupSaveStart + 4, Bits.GetRange(4, Offsets.BackupSaveStart - 4));

            // Checksums
            RecalculateChecksums();
            Bits.SetUInt(0, 0, 32, PrimaryChecksum);
            Bits.SetUInt(Offsets.BackupSaveStart, 0, 32, SecondaryChecksum);
            Bits.SetUInt(Offsets.QuicksaveStart, 0, 32, QuicksaveChecksum);
        }

        /// <summary>
        /// Determines whether or not the given file is a save file for Pokémon Mystery Dungeon: Explorers of Time and Darkness.
        /// </summary>
        /// <param name="file">The file to be checked</param>
        /// <returns>A boolean indicating whether or not the given file is supported by this class</returns>
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
