using SkyEditor.IO.Binary;
using SkyEditor.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    public class BitBlockFile
    {
        public BitBlockFile()
        {
            Bits = new BitBlock(0);
        }

        public BitBlockFile(IEnumerable<byte> rawData)
        {
            if (rawData == null)
            {
                throw new ArgumentNullException(nameof(rawData));
            }

            Bits = new BitBlock(rawData);
        }

        public BitBlockFile(string filename, IFileSystem fileSystem)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            if (fileSystem.GetFileLength(filename) > int.MaxValue)
            {
                throw new ArgumentException(Properties.Resources.BitBlockFile_FileTooLarge);
            }

            Filename = filename;
            using (var file = new BinaryFile(filename, fileSystem))
            {
                Bits = new BitBlock((int)file.Length * 8);
                for (int i = 0; i < file.Length; i++)
                {
                    Bits.SetInt(i, 0, 8, file.ReadByte(i));
                }
            }
        }

        public event EventHandler FileSaved;

        public BitBlock Bits { get; set; }

        public string Filename { get; set; }

        private IFileSystem FileSystem { get; set; }

        protected virtual void PreSave()
        {
        }

        public virtual async Task Save(string filename, IFileSystem fileSystem)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            PreSave();

            Filename = filename;
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            var buffer = new byte[(int)Math.Ceiling(Bits.Count / (decimal)8) - 1];
            using (var file = new BinaryFile(buffer))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    await file.WriteAsync(i, (byte)Bits.GetInt(i, 0, 8));
                }
                await file.Save(filename, fileSystem);
            }
            FileSaved?.Invoke(this, new EventArgs());
        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(Filename) || FileSystem == null)
            {
                throw new InvalidOperationException(Properties.Resources.BitBlockFile_ErrorSavedWithoutFilenameOrFilesystem);
            }

            await Save(Filename, FileSystem);
        }

        public virtual byte[] ToByteArray()
        {
            PreSave();
            return Bits.ToByteArray();
        }
    }
}
