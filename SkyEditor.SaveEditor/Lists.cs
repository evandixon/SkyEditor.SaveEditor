using SkyEditor.SaveEditor.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor
{
    public static class Lists
    {
        public static Dictionary<int, string> SkyLocations { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.SkyLocations);
        public static Dictionary<int, string> TDLocations { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.TDLocations);
        public static Dictionary<int, string> RBLocations { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.RBLocations);

        public static Dictionary<int, string> ExplorersMoves { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.SkyMoves);
        public static Dictionary<int, string> RBMoves { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.RBMoves);

        public static Dictionary<int, string> SkyItems { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.SkyItems);
        public static Dictionary<int, string> TDItems { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.TDItems);
        public static Dictionary<int, string> RBItems { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.RBItems);

        public static Dictionary<int, string> ExplorersPokemon { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.SkyPokemon);
        public static Dictionary<int, string> RBPokemon { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.RBPokemon);

        public static Dictionary<int, string> RBBaseTypes { get; } = BasicIniDictionaryFile.GetDictionary(ListResources.RBBaseTypes);        
    }
}
