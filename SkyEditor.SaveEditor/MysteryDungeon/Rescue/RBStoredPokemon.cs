using SkyEditor.Core.IO;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Rescue
{
    public class RBStoredPokemon
    {
        public const int BitLength = 362;
        public const string MimeType = "application/x-rb-pokemon";

        public event EventHandler FileSaved;

        public RBStoredPokemon()
        {
            Unk1 = new BitBlock(23);
            Unk2 = new BitBlock(43);
        }

        public RBStoredPokemon(BitBlock bits)
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
            Level = bits.GetInt(0, 0, 7);
            ID = bits.GetInt(0, 7, 9);
            MetAt = bits.GetInt(0, 16, 7);
            Unk1 = bits.GetRange(23, 21);
            IQ = bits.GetInt(0, 44, 10);
            HP = bits.GetInt(0, 54, 10);
            Attack = bits.GetInt(0, 64, 8);
            SpAttack = bits.GetInt(0, 72, 8);
            Defense = bits.GetInt(0, 80, 8);
            SpDefense = bits.GetInt(0, 88, 8);
            Exp = bits.GetInt(0, 96, 24);
            Unk2 = bits.GetRange(120, 43);
            Attack1 = new RBAttack(bits.GetRange(163, RBAttack.BitLength));
            Attack2 = new RBAttack(bits.GetRange(183, RBAttack.BitLength));
            Attack3 = new RBAttack(bits.GetRange(203, RBAttack.BitLength));
            Attack4 = new RBAttack(bits.GetRange(223, RBAttack.BitLength));
            Name = bits.GetStringPMD(0, 243, 10);
        }

        public BitBlock GetStoredPokemonBits()
        {
            var bits = new BitBlock(BitLength);
            bits.SetInt(0, 0, 7, Level);
            bits.SetInt(0, 7, 9, ID);
            bits.SetInt(0, 16, 7, MetAt);
            bits.SetRange(23, 21, Unk1);
            bits.SetInt(0, 44, 10, IQ);
            bits.SetInt(0, 54, 10, HP);
            bits.SetInt(0, 64, 8, Attack);
            bits.SetInt(0, 72, 8, SpAttack);
            bits.SetInt(0, 80, 8, Defense);
            bits.SetInt(0, 88, 8, SpDefense);
            bits.SetInt(0, 96, 24, Exp);
            bits.SetRange(120, 43, Unk2);
            bits.SetRange(163, RBAttack.BitLength, Attack1.ToBitBlock());
            bits.SetRange(183, RBAttack.BitLength, Attack2.ToBitBlock());
            bits.SetRange(203, RBAttack.BitLength, Attack3.ToBitBlock());
            bits.SetRange(223, RBAttack.BitLength, Attack4.ToBitBlock());
            bits.SetStringPMD(0, 243, 10, Name);
            return bits;
        }

        public string Filename { get; set; }
        public int Level { get; set; }
        public int ID { get; set; }
        public int MetAt { get; set; }
        public BitBlock Unk1 { get; set; }
        public int IQ { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Exp { get; set; }
        public BitBlock Unk2 { get; set; }
        public RBAttack Attack1 { get; set; }
        public RBAttack Attack2 { get; set; }
        public RBAttack Attack3 { get; set; }
        public RBAttack Attack4 { get; set; }
        public string Name { get; set; }
        

        public string GetDefaultExtension()
        {
            return "*.rbpkm";
        }

        public IEnumerable<string> GetSupportedExtensions()
        {
            return new string[] { GetDefaultExtension() };
        }

        public override string ToString()
        {
            if (ID > 0)
            {
                return string.Format(Resources.Language.SkyStoredPokemonToString, Name, Level, Lists.RBPokemon[ID]);
            }
            else
            {
                return Resources.Language.BlankPokemon;
            }
        }
    }
}
