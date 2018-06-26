using SkyEditor.SaveEditor.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor
{
    public static class Lists
    {
        public static Dictionary<int, string> SkyLocations { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("SkyLocations"));
        public static Dictionary<int, string> TDLocations { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("TDLocations"));
        public static Dictionary<int, string> RBLocations { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("RBLocations"));

        public static Dictionary<int, string> ExplorersMoves { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("SkyMoves"));
        public static Dictionary<int, string> RBMoves { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("RBMoves"));

        public static Dictionary<int, string> SkyItems { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("SkyItems"));
        public static Dictionary<int, string> SkyItemsMovesOnly { get; } = SkyItems.Where(x => x.Key >= 188 && x.Key < 364).ToDictionary(x => x.Key, x => x.Value);
        public static Dictionary<int, string> TDItems { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("TDItems"));
        public static Dictionary<int, string> TDItemsMovesOnly { get; } = TDItems.Where(x => x.Key >= 188 && x.Key < 364).ToDictionary(x => x.Key, x => x.Value);
        public static Dictionary<int, string> RBItems { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("RBItems"));

        public static Dictionary<int, string> ExplorersPokemon { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("SkyPokemon"));
        public static Dictionary<int, string> RBPokemon { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("RBPokemon"));

        public static Dictionary<int, string> RBBaseTypes { get; } = BasicIniDictionaryFile.GetDictionary(DataUtil.GetStringResource("RBBaseTypes"));        
    }
}
