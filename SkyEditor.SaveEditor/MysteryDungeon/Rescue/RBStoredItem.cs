using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Rescue
{
    public class RBStoredItem
    {
        public RBStoredItem()
        {
        }

        public RBStoredItem(int itemID, int quantity)
        {
            ItemID = itemID;
            Quantity = quantity;
        }

        public int ItemID { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Lists.RBItems[ItemID]} ({Quantity})";
        }
    }
}
