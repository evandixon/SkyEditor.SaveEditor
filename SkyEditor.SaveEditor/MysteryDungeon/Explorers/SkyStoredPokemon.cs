using SkyEditor.Core.IO;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkyStoredPokemon : IExplorersStoredPokemon, IOpenableFile, ISavableAs, IOnDisk
    {
        public const int BitLength = 362;
        public const string MimeType = "application/x-sky-pokemon";

        public event EventHandler FileSaved;

        public SkyStoredPokemon()
        {
            IQMap = new BitBlock(69);
        }

        public SkyStoredPokemon(BitBlock bits)
        {
            Initialize(bits);
        }

        public async Task OpenFile(string filename, IFileSystem provider)
        {
            var toOpen = new BitBlockFile();
            await toOpen.OpenFile(filename, provider);

            // matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            for (int i = 1; i <= 8 - (BitLength % 8); i++)
            {
                toOpen.Bits.Bits.RemoveAt(0);
            }

            Initialize(toOpen.Bits);
        }

        public async Task Save(string filename, IFileSystem provider)
        {
            var toSave = new BitBlockFile();

            // matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            for (int i = 1; i <= 8 - (BitLength % 8); i++)
            {
                toSave.Bits.Bits.Add(false);
            }

            toSave.Bits.Bits.AddRange(GetStoredPokemonBits());
            await toSave.Save(filename, provider);
            FileSaved?.Invoke(this, new EventArgs());
        }

        public async Task Save(IFileSystem provider)
        {
            await Save(Filename, provider);
        }

        private void Initialize(BitBlock bits)
        {
            IsValid = bits[0];
            Level = bits.GetInt(0, 1, 7);
            ID = new ExplorersPokemonId(bits.GetInt(0, 8, 11));
            MetAt = bits.GetInt(0, 19, 8);
            MetFloor = bits.GetInt(0, 27, 7);
            Unk1 = bits[34];
            EvolvedAtLevel1 = bits.GetInt(0, 35, 7);
            EvolvedAtLevel2 = bits.GetInt(0, 42, 7);
            IQ = bits.GetInt(0, 49, 10);
            HP = bits.GetInt(0, 59, 10);
            Attack = bits.GetInt(0, 69, 8);
            SpAttack = bits.GetInt(0, 77, 8);
            Defense = bits.GetInt(0, 85, 8);
            SpDefense = bits.GetInt(0, 93, 8);
            Exp = bits.GetInt(0, 101, 24);
            IQMap = bits.GetRange(125, 69);
            Tactic = bits.GetInt(0, 194, 4);
            Attack1 = new ExplorersAttack(bits.GetRange(198, ExplorersAttack.BitLength));
            Attack2 = new ExplorersAttack(bits.GetRange(219, ExplorersAttack.BitLength));
            Attack3 = new ExplorersAttack(bits.GetRange(240, ExplorersAttack.BitLength));
            Attack4 = new ExplorersAttack(bits.GetRange(261, ExplorersAttack.BitLength));
            Name = bits.GetStringPMD(0, 282, 10);
        }

        public BitBlock GetStoredPokemonBits()
        {
            var bits = new BitBlock(BitLength);
            bits[0] = IsValid;
            bits.SetInt(0, 1, 7, Level);
            bits.SetInt(0, 8, 11, ID.RawID);
            bits.SetInt(0, 19, 8, MetAt);
            bits.SetInt(0, 27, 7, MetFloor);
            bits[34] = Unk1;
            bits.SetInt(0, 35, 7, EvolvedAtLevel1);
            bits.SetInt(0, 42, 7, EvolvedAtLevel2);
            bits.SetInt(0, 49, 10, IQ);
            bits.SetInt(0, 59, 10, HP);
            bits.SetInt(0, 69, 8, Attack);
            bits.SetInt(0, 77, 8, SpAttack);
            bits.SetInt(0, 85, 8, Defense);
            bits.SetInt(0, 93, 8, SpDefense);
            bits.SetInt(0, 101, 24, Exp);
            bits.SetRange(125, 69, IQMap);
            bits.SetInt(0, 194, 4, Tactic);
            bits.SetRange(198, ExplorersAttack.BitLength, Attack1.ToBitBlock());
            bits.SetRange(219, ExplorersAttack.BitLength, Attack2.ToBitBlock());
            bits.SetRange(240, ExplorersAttack.BitLength, Attack3.ToBitBlock());
            bits.SetRange(261, ExplorersAttack.BitLength, Attack4.ToBitBlock());
            bits.SetStringPMD(0, 282, 10, Name);
            return bits;
        }    

        public string Filename { get; set; }
        public bool IsValid { get; set; }
        public int Level { get; set; }
        public ExplorersPokemonId ID { get; set; }
        public int MetAt { get; set; }
        public int MetFloor { get; set; }
        public bool Unk1 { get; set; }
        public int EvolvedAtLevel1 { get; set; }
        public int EvolvedAtLevel2 { get; set; }
        public int IQ { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Exp { get; set; }
        public BitBlock IQMap { get; set; }
        public int Tactic { get; set; }
        public ExplorersAttack Attack1 { get; set; }
        public ExplorersAttack Attack2 { get; set; }
        public ExplorersAttack Attack3 { get; set; }
        public ExplorersAttack Attack4 { get; set; }
        public string Name { get; set; }

        public string GetDefaultExtension()
        {
            return "*.skypkm";
        }

        public IEnumerable<string> GetSupportedExtensions()
        {
            return new string[] { GetDefaultExtension() };
        }

        public override string ToString()
        {
            if (IsValid)
            {
                return string.Format(Resources.Language.SkyStoredPokemonToString, Name, Level, Lists.ExplorersPokemon[ID.ID]);
            }
            else
            {
                return Resources.Language.BlankPokemon;
            }
        }
    }
}
