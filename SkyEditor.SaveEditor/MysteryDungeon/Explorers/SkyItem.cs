using SkyEditor.SaveEditor.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class SkyItem : IExplorersItem, IClonable
    {
        public SkyItem()
        {
        }

        public SkyItem(int id, int parameter)
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
        /// The contained item if this is a box or a used TM
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// If the item is a Used TM (when the ID is 187), thrown when setting the property if the target value is not a TM item (if the value is less than 188).
        /// If the item is a box, thrown when setting the property if the target value is out of range.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when setting the property if the item is neither a box nor a Used TM.
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
                else
                {
                    throw new NotSupportedException(Language.Error_CantChangeContainedItem);
                }
            }
        }

        /// <summary>
        /// The number of items in the stack.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// Thrown when setting the property if the current item is not stackable.
        /// </exception>
        public int Quantity
        {
            get
            {
                if (IsStackableItem)
                {
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
                else
                {
                    throw new NotSupportedException(Language.Error_CantChangeItemQuantity);
                }
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
            return new SkyItem(ID, Parameter);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is SkyItem && ID == (obj as SkyItem).ID && Parameter == (obj as SkyItem).Parameter;
        }

        public static bool operator ==(SkyItem x, SkyItem y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(SkyItem x, SkyItem y)
        {
            return (!(x == y));
        }
    }
}
