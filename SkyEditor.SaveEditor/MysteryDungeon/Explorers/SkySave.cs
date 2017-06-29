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

        public override async Task OpenFile(string filename, IIOProvider provider)
        {
            await base.OpenFile(filename, provider);
            Init();
        }

        private void Init()
        {

        }

        private void PreSave()
        {

        }

        public override async Task Save(string filename, IIOProvider provider)
        {
            PreSave();
            await base.Save(filename, provider);
        }

        public override byte[] ToByteArray()
        {
            PreSave();
            FixChecksum();
            return base.ToByteArray();
        }

        protected override void FixChecksum()
        {
            base.FixChecksum();

            throw new NotImplementedException();
        }

        public Task<bool> IsOfType(GenericFile file)
        {
            throw new NotImplementedException();
        }
    }
}
