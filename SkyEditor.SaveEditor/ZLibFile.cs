using SkyEditor.Core.IO;
using SkyEditor.Core.IO.PluginInfrastructure;
using SkyEditor.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    public class ZLibFile : IOpenableFile, IDetectableFileType
    {
        public async Task OpenFile(string filename, IFileSystem provider)
        {
            using (var file = new GenericFile())
            {
                await file.OpenFile(filename, provider);
                using (var compressed = new MemoryStream(await file.ReadAsync()))
                {
                    compressed.Seek(2, SeekOrigin.Begin);
                    using (var decompressed = new MemoryStream())
                    {
                        using (var zlib = new DeflateStream(compressed, CompressionMode.Decompress))
                        {
                            zlib.CopyTo(decompressed);
                        }
                        RawData = decompressed.ToArray();
                    }
                }
            }
        }

        /// <remarks>
        /// Auto-detection only supports up to 32MB files to avoid hogging all the ram.
        /// </remarks>
        public async Task<bool> IsOfType(GenericFile file)
        {
            if (file.Length > 2 && await file.ReadAsync(0) == 0x78 && new byte[] { 0x1, 0x9C, 0xDA}.Contains(await file.ReadAsync(1)) && file.Length < 32 * 1024 * 1024)
            {
                try
                {
                    using (var compressed = new MemoryStream(await file.ReadAsync()))
                    {
                        compressed.Seek(2, SeekOrigin.Begin);
                        using (var decompressed = new MemoryStream())
                        {
                            using (var zlib = new DeflateStream(compressed, CompressionMode.Decompress))
                            {
                                zlib.CopyTo(decompressed);
                            }
                            var rawData = decompressed.ToArray();
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        
        public byte[] RawData { get; set; }
    }
}
