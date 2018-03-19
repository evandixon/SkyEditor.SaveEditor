using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public interface IExplorersActivePokemon
    {
        int RosterNumber { get; set; }
        int Level { get; set; }
        ExplorersPokemonId ID { get; set; }
        int MetAt { get; set; }
        int MetFloor { get; set; }
        int IQ { get; set; }
        int CurrentHP { get; set; }
        int MaxHP { get; set; }
        int Attack { get; set; }
        int Defense { get; set; }
        int SpAttack { get; set; }
        int SpDefense { get; set; }
        int Exp { get; set; }
        string Name { get; set; }

        ExplorersActiveAttack Attack1 { get; set; }
        ExplorersActiveAttack Attack2 { get; set; }
        ExplorersActiveAttack Attack3 { get; set; }
        ExplorersActiveAttack Attack4 { get; set; }
    }
}
