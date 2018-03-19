using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    /// <summary>
    /// A stored item in the Pokémon Mystery Dungeon: Explorers series
    /// </summary>
    public interface IExplorersItem
    {
        /// <summary>
        /// The ID of the item
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Additional information about the item. The exact meaning is determined by <see cref="IsBox"/>, <see cref="IsUsedTM"/>, or <see cref="IsStackableItem"/>.
        /// </summary>
        int Parameter { get; set; }

        /// <summary>
        /// Whether or not the current item is a box. If true, <see cref="Parameter"/> refers to an item.
        /// </summary>
        bool IsBox { get; }

        /// <summary>
        /// Whether or not the current item is a Used TM. If true, <see cref="Parameter"/> refers to a move.
        /// </summary>
        bool IsUsedTM { get; }

        /// <summary>
        /// Whether or not the current item can stack. If true, <see cref="Parameter"/> refers to the quantity.
        /// </summary>
        bool IsStackableItem { get; }
    }
}
