using SkyEditor.SaveEditor.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class ExplorersItem : IExplorersItem, IClonable
    {
        public ExplorersItem()
        {
        }

        public ExplorersItem(int id, int parameter)
        {            
            ID = id;
            Parameter = parameter;
        }

        /// <summary>
        /// The ID of the item
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name => (ID >= 0 && Lists.SkyItems.Count > ID) ?
            Lists.SkyItems[ID] 
            : string.Format(Language.UnknownItem, ID.ToString());

        /// <summary>
        /// Gets or sets the contained item if this is a box or a used TM. If this is not a box or a used TM, this will return 0, and changes will not be applied.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// If the item is a Used TM (when the ID is 187), thrown when setting the property if the target value is not a TM item (if the value is less than 188).
        /// If the item is a box, thrown when setting the property if the target value is out of range.
        /// </exception>
        public int ContainedItemID
        {
            get
            {
                if (IsUsedTM)
                {
                    return Parameter + 188;
                }
                else if (IsBox)
                {
                    return Parameter;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IsUsedTM)
                {
                    if (value < 188 || value > (Math.Pow(2, 11) - 1))
                    {
                        throw new IndexOutOfRangeException(Language.Error_UsedTMParameterIndexOutOfRange);
                    }

                    Parameter = value - 188;
                }
                else if (IsBox)
                {
                    if (value > (Math.Pow(2, 11) - 1))
                    {
                        throw new IndexOutOfRangeException();
                    }

                    Parameter = value;
                }
                // else do nothing
            }
        }

        /// <summary>
        /// Gets or sets the number of items in the stack. If the item is not stackable, this will always return 1, and changes will not apply.
        /// </summary>
        public int Quantity
        {
            get
            {
                if (IsStackableItem)
                {
                    // Ensure parameter is within bounds                    
                    if (Parameter < -1)
                    {
                        Parameter = 0;
                    }

                    if (Parameter > 127 )
                    {
                        Parameter = 127;
                    }

                    return Parameter;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (IsStackableItem)
                {
                    Parameter = value;
                }
                // else do nothing
            }
        }

        /// <summary>
        /// The raw parameter indicating either held item or quantity, depending on the context
        /// </summary>
        public int Parameter { get; set; }

        /// <summary>
        /// Whether or not the current item is a box. If true, <see cref="Parameter"/> refers to an item.
        /// </summary>
        public bool IsBox => ID >= 364 && ID <= 399;

        /// <summary>
        /// Whether or not the current item is a Used TM. If true, <see cref="Parameter"/> refers to an item with an ID greater than 187.
        /// </summary>
        public bool IsUsedTM => ID == 187;

        /// <summary>
        /// Whether or not the current item can stack. If true, <see cref="Parameter"/> refers to the quantity.
        /// </summary>
        public bool IsStackableItem => ID >= 1 && ID <= 9;

        public virtual object Clone()
        {
            return new ExplorersItem(ID, Parameter);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is ExplorersItem && ID == (obj as ExplorersItem).ID && Parameter == (obj as ExplorersItem).Parameter;
        }

        public override int GetHashCode()
        {
            return ID ^ Parameter;
        }

        public static bool operator ==(ExplorersItem x, ExplorersItem y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ExplorersItem x, ExplorersItem y)
        {
            return (!(x == y));
        }
    }
}
