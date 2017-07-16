using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public interface IExplorersStoredPokemon
    {
        int Level { get; set; }
        ExplorersPokemonId ID { get; set; }
        int MetAt { get; set; }
        int MetFloor { get; set; }
        int EvolvedAtLevel1 { get; set; }
        int EvolvedAtLevel2 { get; set; }
        int IQ { get; set; }
        int HP { get; set; }
        int Attack { get; set; }
        int Defense { get; set; }
        int SpAttack { get; set; }
        int SpDefense { get; set; }
        int Exp { get; set; }
        string Name { get; set; }
        int Tactic { get; set; }
        ExplorersAttack Attack1 { get; set; }
        ExplorersAttack Attack2 { get; set; }
        ExplorersAttack Attack3 { get; set; }
        ExplorersAttack Attack4 { get; set; }
    }
}
