using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class ExplorersPokemonId
    {
        public ExplorersPokemonId()
        {

        }

        public ExplorersPokemonId(int rawID)
        {
            RawID = rawID;
        }

        public int RawID { get; set; }

        public int ID
        {
            get
            {
                if (RawID >= 600)
                {
                    return RawID - 600;
                }
                else
                {
                    return RawID;
                }
            }
            set
            {
                if (RawID >= 600)
                {
                    RawID = value + 600;
                }
                else
                {
                    RawID = value;
                }
            }
        }

        public bool IsFemale
        {
            get
            {
                return RawID >= 600;
            }
            set
            {
                if (RawID >= 600)
                {
                    if (value)
                    {
                        // Do nothing
                    }
                    else
                    {
                        RawID -= 600;
                    }
                }
                else
                {
                    if (value)
                    {
                        RawID += 600;
                    }
                    else
                    {
                        // Do nothing
                    }
                }
            }
        }
    }
}
