using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon
{
    public class IDConversion
    {
        /// <summary>
        /// Converts a Pokémon ID from Explorers of Sky to Red/Blue Rescue Team.
        /// </summary>
        /// <param name="eosID">A Pokémon ID from Explorers of Sky.</param>
        /// <param name="throwOnUnsupported">Whether or not to throw an exception if the Pokémon from Explorers of Sky is not in Red/Blue Rescue Team.</param>
        /// <returns>An integer indicating the equivalent Red/Blue Rescue Team Pokémon, or -1 if the Pokémon is not in the game and <paramref name="throwOnUnsupported"/> is false.</returns>
        public static int ConvertEoSPokemonToRB(int eosID, bool throwOnUnsupported = false)
        {
            if (eosID == 554)
            {
                //Statue
                return 422;
            }
            else if (eosID == 553)
            {
                //Decoy
                return 421;
            }
            else if (eosID == 488)
            {
                //Munchlax
                return 420;
            }
            else if (eosID == 421)
            {
                return 419;
            }
            else if (eosID == 420)
            {
                return 418;
            }
            else if (eosID == 419)
            {
                return 417;
            }
            else if (eosID > 420)
            {
                if (throwOnUnsupported)
                {
                    throw new ArgumentException(nameof(eosID), "The given Explorers of Sky Pokémon is not a Red/Blue Rescue Team Pokémon.");
                }
                else
                {
                    return -1;
                }
            }
            else if (eosID >= 385)
            {
                return eosID - 4;
            }
            else if (eosID == 384)
            {
                //Shiny Celebi
                if (throwOnUnsupported)
                {
                    throw new ArgumentException(nameof(eosID), "Shiny/Pink Celebi is not in Red/Blue Rescue Team Pokémon.");
                }
                else
                {
                    return -1;
                }
            }
            else if (eosID >= 280)
            {
                return eosID - 3;
            }
            else if (eosID == 279)
            {
                if (throwOnUnsupported)
                {
                    throw new ArgumentException(nameof(eosID), "Purple Keckleon is not in Red/Blue Rescue Team Pokémon.");
                }
                else
                {
                    return -1;
                }
            }
            else if (eosID >= 229)
            {
                return eosID - 2;
            }
            else if (eosID == 228)
            {
                return 416;
            }
            else if (eosID == 227)
            {
                return 415;
            }
            else if (eosID < 0)
            {
                if (throwOnUnsupported)
                {
                    throw new ArgumentException(nameof(eosID), "Explorers of Sky Pokemon ID must be 0 or greater");
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return eosID;
            }
        }
    }
}