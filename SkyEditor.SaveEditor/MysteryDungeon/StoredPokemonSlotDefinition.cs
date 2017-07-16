using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon
{
    public class StoredPokemonSlotDefinition
    {
        public StoredPokemonSlotDefinition(int index, string areaName, int length)
        {
            Index = index;
            Name = areaName;
            Length = length;
            CurrentPokemonCount = 0;
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int CurrentPokemonCount { get; set; }
        
        public static StoredPokemonSlotDefinition FromLine(string Line, int Index)
        {
            var parts = Line.Split(':');
            return new StoredPokemonSlotDefinition(Index, parts[0].Trim(), int.Parse(parts[1].Trim()));
        }
        public static List<StoredPokemonSlotDefinition> FromLines(string lines)
        {
            var output = new List<StoredPokemonSlotDefinition>();
            int offset = 0;
            foreach (var item in lines.Split('\n'))
            {
                if (!lines.Trim().StartsWith("#"))
                {
                    output.Add(FromLine(lines.Trim(), offset));
                    offset += output.Last().Length;
                }
            }
            return output;
        }
        public override string ToString()
        {
            return Name + " (" + CurrentPokemonCount + "/" + Length + ")";
        }
    }

}
