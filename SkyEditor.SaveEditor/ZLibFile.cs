using SkyEditor.IO.FileSystem;
using System;
using System.IO;
using System.IO.Compression;

namespace SkyEditor.SaveEditor
{
    public class ZLibFile
    {
        public ZLibFile(string filename, IFileSystem fileSystem)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            using (var compressed = fileSystem.OpenFileReadOnly(filename))
            using (var decompressed = new MemoryStream())
            using (var zlib = new DeflateStream(compressed, CompressionMode.Decompress))
            {
                zlib.CopyTo(decompressed);
                RawData = decompressed.ToArray();
            }
        }
        
        public byte[] RawData { get; set; }
    }
}
