using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon
{
    public interface IMysteryDungeonNdsAttack
    {
        /// <summary>
        /// Whether or not the move is linked with the next move
        /// </summary>
        bool IsLinked { get; set; }

        /// <summary>
        /// Whether or not the move is enabled for AI to use
        /// </summary>
        bool IsSwitched { get; set; }

        /// <summary>
        /// Whether or not the move is set as the default move
        /// </summary>
        /// <remarks>
        /// The default move can be used in-game by pressing L + A
        /// </remarks>
        bool IsSet { get; set; }

        /// <summary>
        /// ID of the move
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Power boost of the move, resulting from drinking Ginseng
        /// </summary>
        int PowerBoost { get; set; }
    }
}
