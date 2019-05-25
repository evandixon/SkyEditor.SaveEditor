using SkyEditor.Core.IO;
using SkyEditor.Core.Utilities;
using SkyEditor.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    public class BitBlockFile : IOpenableFile, ISavableAs, IOnDisk, INamed
    {
        public BitBlockFile()
        {
            Bits = new BitBlock(0);
        }

        public BitBlockFile(IEnumerable<byte> rawData)
        {
            Bits = new BitBlock(rawData);
        }

        public event EventHandler FileSaved;

        public BitBlock Bits { get; set; }

        public string Filename { get; set; }

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    return Path.GetFileName(Filename);
                }
                else
                {
                    return _name;
                }
            }
            set
            {
                _name = value;
            }
        }
        private string _name;
        
        private IFileSystem CurrentFileSystem { get; set; }

        public virtual async Task OpenFile(string filename, IFileSystem provider)
        {
            Filename = filename;
            CurrentFileSystem = provider;
            using (var f = new GenericFile())
            {
                f.EnableInMemoryLoad = true;
                f.IsReadOnly = true;
                await f.OpenFile(filename, provider);

                Bits = new BitBlock(0);
                ProcessRawData(f);
            }
        }

        private void ProcessRawData(GenericFile file)
        {
            for (int i = 0;i<file.Length;i++)
            {
                Bits.AppendByte(file.Read(i));
            }
        }

        protected virtual void PreSave()
        {
        }

        public virtual async Task Save(string filename, IFileSystem provider)
        {
            PreSave();
            var buffer = new byte[(int)Math.Ceiling(Bits.Count / (decimal)8) - 1];
            using (var f = new GenericFile())
            {
                f.CreateFile(buffer);
                for (int i = 0;i<buffer.Length;i++)
                {
                    await f.WriteAsync(i, (byte)Bits.GetInt(i, 0, 8));
                }
                await f.Save(filename, provider);
            }
            FileSaved?.Invoke(this, new EventArgs());
        }

        public async Task Save(IFileSystem provider)
        {
            await Save(Filename, provider);
        }

        public virtual byte[] ToByteArray()
        {
            PreSave();
            return Bits.ToByteArray();
        } 

        public virtual string GetDefaultExtension()
        {
            return "*.sav";
        }

        public virtual IEnumerable<string> GetSupportedExtensions()
        {
            return new[] { "*.sav" };
        }

    }
}
