using SkyEditor.IO.Binary;
using SkyEditor.IO.FileSystem;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using SkyEditor.SaveEditor.MysteryDungeon.Rescue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor
{
    public static class SaveLoader
    {
        /// <summary>
        /// Creates an instance of a save file, or returns null if the save type could not be determined
        /// </summary>
        /// <param name="filename">Path of the file to open</param>
        /// <param name="fileSystem">File system abstraction from which to load the file</param>
        /// <returns>An instance of a save file, or null if the save type could not be determined</returns>
        public static async Task<ISaveFile> LoadSaveFile(string filename, IFileSystem fileSystem)
        {
            var file = new BinaryFile(filename, fileSystem);
            if (await SkySave.IsOfType(file).ConfigureAwait(false))
            {
                return new SkySave(file);
            }
            else if (await TDSave.IsOfType(file).ConfigureAwait(false))
            {
                return new TDSave(file);
            }
            else if (await RBSave.IsOfType(file).ConfigureAwait(false))
            {
                return new RBSave(file);
            }
            else if (await RBSaveEU.IsOfType(file).ConfigureAwait(false))
            {
                return new RBSaveEU(file);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates an instance of a save file, or returns null if the save type could not be determined
        /// </summary>
        /// <param name="filename">Path of the file to open</param>
        /// <returns>An instance of a save file, or null if the save type could not be determined</returns>
        public static async Task<ISaveFile> LoadSaveFile(string filename)
        {
            return await LoadSaveFile(filename, PhysicalFileSystem.Instance).ConfigureAwait(false);
        }
    }
}
