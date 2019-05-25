using SkyEditor.Core.IO;
using SkyEditor.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkyQuicksavePokemon : IOpenableFile, ISavableAs, IOnDisk
    {
        public const int ByteLength = 429;
        public const int BitLength = ByteLength * 8;

        public event EventHandler FileSaved;

        public SkyQuicksavePokemon()
        {
            Unk1 = new BitBlock(80);
            Unk2 = new BitBlock(48);
            Unk3 = new BitBlock(48);
            Unk4 = new BitBlock(32);
            Unk5 = new BitBlock(2408);
            Unk6 = new BitBlock(592);
        }

        public SkyQuicksavePokemon(BitBlock bits)
        {
            Initialize(bits);
        }

        public void Initialize(BitBlock bits)
        {
            Unk1 = bits.GetRange(0, 80);
            TransformedID = new ExplorersPokemonId(bits.GetInt(0, 80, 16));
            ID = new ExplorersPokemonId(bits.GetInt(0, 96, 16));
            Unk2 = bits.GetRange(112, 48);
            Level = bits.GetInt(0, 144, 8);
            Unk3 = bits.GetRange(152, 48);
            CurrentHP = bits.GetInt(0, 192, 16);
            MaxHP = bits.GetInt(0, 208, 16);
            HPBoost = bits.GetInt(0, 224, 16);
            Unk4 = bits.GetRange(240, 32);
            Attack = bits.GetInt(0, 256, 8);
            Defense = bits.GetInt(0, 264, 8);
            SpAttack = bits.GetInt(0, 272, 8);
            SpDefense = bits.GetInt(0, 280, 8);
            Exp = bits.GetInt(0, 280, 32);
            Unk5 = bits.GetRange(320, 2408);
            Attack1 = new SkyQuicksaveAttack(bits.GetRange(2696 + 0 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength));
            Attack2 = new SkyQuicksaveAttack(bits.GetRange(2696 + 1 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength));
            Attack3 = new SkyQuicksaveAttack(bits.GetRange(2696 + 2 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength));
            Attack4 = new SkyQuicksaveAttack(bits.GetRange(2696 + 3 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength));
            Unk6 = bits.GetRange(2840, 592);
        }

        public BitBlock GetQuicksavePokemonBits()
        {
            var bits = new BitBlock(BitLength);
            bits.SetRange(0, 80, Unk1);
            bits.SetInt(0, 80, 16, TransformedID.ID);
            bits.SetInt(0, 96, 16, ID.ID);
            bits.SetRange(112, 48, Unk2);
            bits.SetInt(0, 144, 8, Level);
            bits.SetRange(152, 48, Unk3);
            bits.SetInt(0, 192, 16, CurrentHP);
            bits.SetInt(0, 208, 16, MaxHP);
            bits.SetRange(240, 32, Unk4);
            bits.SetInt(0, 256, 8, Attack);
            bits.SetInt(0, 264, 8, Defense);
            bits.SetInt(0, 272, 8, SpAttack);
            bits.SetInt(0, 280, 8, SpDefense);
            bits.SetInt(0, 288, 32, Exp);
            bits.SetRange(320, 2408, Unk5);
            bits.SetRange(2696 + 0 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength, Attack1.ToBitBlock());
            bits.SetRange(2696 + 1 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength, Attack2.ToBitBlock());
            bits.SetRange(2696 + 2 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength, Attack3.ToBitBlock());
            bits.SetRange(2696 + 3 * SkyQuicksaveAttack.BitLength, SkyQuicksaveAttack.BitLength, Attack4.ToBitBlock());
            bits.SetRange(2840, 592, Unk6);
            return bits;
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

            toSave.Bits.Bits.AddRange(GetQuicksavePokemonBits());
            await toSave.Save(filename, provider);
            FileSaved?.Invoke(this, new EventArgs());
        }

        public async Task Save(IFileSystem provider)
        {
            await Save(Filename, provider);
        }

        public string GetDefaultExtension()
        {
            return "*.skypkmq";
        }

        public IEnumerable<string> GetSupportedExtensions()
        {
            return new string[] { GetDefaultExtension() };
        }

        public string Filename { get; set; }
        public BitBlock Unk1 { get; set; }
        public BitBlock Unk2 { get; set; }
        public BitBlock Unk3 { get; set; }
        public BitBlock Unk4 { get; set; }
        public BitBlock Unk5 { get; set; }
        public BitBlock Unk6 { get; set; }
        public bool IsValid { get; set; }
        public int Level { get; set; }
        public ExplorersPokemonId ID { get; set; }
        public ExplorersPokemonId TransformedID { get; set; }
        public int RosterNumber { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int HPBoost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Exp { get; set; }
        public SkyQuicksaveAttack Attack1 { get; set; }
        public SkyQuicksaveAttack Attack2 { get; set; }
        public SkyQuicksaveAttack Attack3 { get; set; }
        public SkyQuicksaveAttack Attack4 { get; set; }
    }
}
