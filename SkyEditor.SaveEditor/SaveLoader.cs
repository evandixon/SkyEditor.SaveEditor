using SkyEditor.IO.Binary;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    public static class SaveLoader
    {
        public static async Task<ISaveFile> LoadSaveFile(string filename, IFileSystem fileSystem)
        {
            var file = new BinaryFile(filename, fileSystem);
            if (await SkySave.IsOfType(file).ConfigureAwait(false))
            {
                return new SkySave(file);
            }

        }

        public static async Task<ISaveFile> LoadSaveFile(string filename)
        {
            return await LoadSaveFile(filename, PhysicalFileSystem.Instance).ConfigureAwait(false);
        }
    }
}
