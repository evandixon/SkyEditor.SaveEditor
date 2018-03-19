using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public interface IExplorersActiveAttack : IMysteryDungeonNdsAttack
    {
        /// <summary>
        /// Whether or not the move has been sealed, preventing its use
        /// </summary>
        bool IsSealed { get; set; }

        /// <summary>
        /// Power points of the move
        /// </summary>
        int PP { get; set; }
    }
}
