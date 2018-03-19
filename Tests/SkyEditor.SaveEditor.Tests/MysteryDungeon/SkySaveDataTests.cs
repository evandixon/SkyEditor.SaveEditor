using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.Tests.MysteryDungeon
{
    [TestClass]
    public class SkySaveDataTests
    {
        public const string TestCategory = "Explorers of Sky Data Tests";

        private byte[] GetTestSaveData()
        {
            return DataUtil.GetBinaryResource("EoS.sav");
        }

        private SkySave GetTestSave()
        {
            return new SkySave(GetTestSaveData());
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestChecksumsValid()
        {
            var save = GetTestSave();
            Assert.IsTrue(save.IsPrimaryChecksumValid());
            Assert.IsTrue(save.IsSecondaryChecksumValid());
            Assert.IsTrue(save.IsQuickSaveChecksumValid());

            Assert.AreEqual(save.PrimaryChecksum, save.SecondaryChecksum, "Primary and backup checksums should match.");
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestFixChecksums()
        {
            var save = GetTestSave();
            var save2 = GetTestSave();

            save2.RecalculateChecksums();

            Assert.AreEqual(save.PrimaryChecksum, save2.PrimaryChecksum);
            Assert.AreEqual(save.SecondaryChecksum, save2.SecondaryChecksum);
            Assert.AreEqual(save.QuicksaveChecksum, save2.QuicksaveChecksum);

            Assert.AreEqual(save.PrimaryChecksum, save.SecondaryChecksum, "Primary and backup checksums should match.");
        }

        #region General

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_TeamName_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual("Blue", save.TeamName);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_TeamName_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual("Blue", newSave.TeamName);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_Adventres_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(128, save.NumberOfAdventures);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_Adventures_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(128, newSave.NumberOfAdventures);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_ExplorersRank_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(SkyExplorerRank.Diamond, save.ExplorerRank);
        }


        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_ExplorersRank_Edit()
        {
            var save = GetTestSave();

            save.ExplorerRank = SkyExplorerRank.Normal;
            Assert.AreEqual(SkyExplorerRank.Normal, save.ExplorerRank);
            Assert.AreEqual(0, save.ExplorerRankPoints);

            save.ExplorerRank = SkyExplorerRank.Guildmaster;
            Assert.AreEqual(SkyExplorerRank.Guildmaster, save.ExplorerRank);
            Assert.AreEqual(100000, save.ExplorerRankPoints);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_ExplorersRankPoints_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(3820, save.ExplorerRankPoints);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_ExplorersRankPoints_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(3820, newSave.ExplorerRankPoints);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_StoredMoney_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(44459, save.StoredMoney);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_StoredMoney_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(44459, newSave.StoredMoney);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_HeldMoney_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(42, save.HeldMoney);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_HeldMoney_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(42, newSave.HeldMoney);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_SpEpisodeHeldMoney_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(0, save.SpEpisodeHeldMoney);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void General_SpEpisodeHeldMoney_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(0, newSave.SpEpisodeHeldMoney);
        }

        #endregion

        #region Items

        private void TestItem(ExplorersItem item, int id, int containedItem, int quantity, bool isBox, bool isUsedTM, bool isStackable)
        {
            Assert.AreEqual(id, item.ID);
            Assert.AreEqual(containedItem, item.ContainedItemID);
            Assert.AreEqual(quantity, item.Quantity);
            Assert.AreEqual(isBox, item.IsBox);
            Assert.AreEqual(isUsedTM, item.IsUsedTM);
            Assert.AreEqual(isStackable, item.IsStackableItem);
        }

        private void TestHeldItem(SkyHeldItem item, int id, int containedItem, int quantity, bool isBox, bool isUsedTM, bool isStackable, int heldBy)
        {
            Assert.IsTrue(item.IsValid, "Invalid items shouldn't be accessible");
            Assert.AreEqual(id, item.ID);
            Assert.AreEqual(containedItem, item.ContainedItemID);
            Assert.AreEqual(quantity, item.Quantity);
            Assert.AreEqual(isBox, item.IsBox);
            Assert.AreEqual(isUsedTM, item.IsUsedTM);
            Assert.AreEqual(isStackable, item.IsStackableItem);
            Assert.AreEqual(heldBy, (int)item.Holder);
        }

        private void TestStoredItems(SkySave save)
        {
            Assert.AreEqual(321, save.StoredItems.Count);

            // Page 1
            TestItem(save.StoredItems[0], 37, 0, 1, false, false, false); // Def scarf
            TestItem(save.StoredItems[1], 37, 0, 1, false, false, false); // Def Scarf
            TestItem(save.StoredItems[2], 37, 0, 1, false, false, false); // Def Scarf
            TestItem(save.StoredItems[3], 37, 0, 1, false, false, false); // Def Scarf
            TestItem(save.StoredItems[4], 16, 0, 1, false, false, false); // Mobile Scarf
            TestItem(save.StoredItems[5], 51, 0, 1, false, false, false); // Pass Scarf
            TestItem(save.StoredItems[6], 27, 0, 1, false, false, false); // Pecha Scarf
            TestItem(save.StoredItems[7], 27, 0, 1, false, false, false); // Pecha Scarf

            // Page 2
            TestItem(save.StoredItems[8], 27, 0, 1, false, false, false); // Pecha Scarf
            TestItem(save.StoredItems[9], 27, 0, 1, false, false, false); // Pecha Scarf
            TestItem(save.StoredItems[10], 31, 0, 1, false, false, false); // Sneak Scarf
            TestItem(save.StoredItems[11], 17, 0, 1, false, false, false); // Heal Ribbon
            TestItem(save.StoredItems[12], 23, 0, 1, false, false, false); // Joy Ribbon
            TestItem(save.StoredItems[13], 23, 0, 1, false, false, false); // Joy Ribbon
            TestItem(save.StoredItems[14], 436, 0, 1, false, false, false); // Viridian Bow
            TestItem(save.StoredItems[15], 25, 0, 1, false, false, false); // Persim Band

            // ...

            // Page 4
            // ...
            TestItem(save.StoredItems[31], 2, 0, 8, false, false, true); // Iron Thorn (8)

            // Page 5 
            // ...
            TestItem(save.StoredItems[33], 7, 0, 20, false, false, true); // Gravelrock (20)

            // ...

            // Page 41
            TestItem(save.StoredItems[320], 714, 0, 1, false, false, false); // Grotle Twig
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void StoredItems_Read()
        {
            var save = GetTestSave();
            TestStoredItems(save);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void StoredItems_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            TestStoredItems(newSave);

            // Test raw data
            for (int i = 0; i < save.StoredItems.Count; i++)
            {
                Assert.AreEqual(save.StoredItems[i], newSave.StoredItems[i]);
            }
        }

        private void TestHeldItems(SkySave save)
        {
            Assert.AreEqual(12, save.HeldItems.Count);
            TestHeldItem(save.HeldItems[0], 131, 0, 1, false, false, false, 0); // Gray Gummi
            TestHeldItem(save.HeldItems[1], 128, 0, 1, false, false, false, 0); // Sky Gummi
            TestHeldItem(save.HeldItems[2], 123, 0, 1, false, false, false, 0); // Yellow Gummi
            TestHeldItem(save.HeldItems[3], 90, 0, 1, false, false, false, 0); // Chesto Berry
            TestHeldItem(save.HeldItems[4], 69, 0, 1, false, false, false, 0); // Heal Seed
            TestHeldItem(save.HeldItems[5], 83, 0, 1, false, false, false, 0); // Totter Seed
            TestHeldItem(save.HeldItems[6], 86, 0, 1, false, false, false, 0); // Warp Seed
            TestHeldItem(save.HeldItems[7], 86, 0, 1, false, false, false, 0); // Warp Seed

            TestHeldItem(save.HeldItems[8], 187, 215, 1, false, true, false, 0); // Used TM (Dig)
            TestHeldItem(save.HeldItems[9], 309, 0, 1, false, false, false, 0); // Mug Orb
            TestHeldItem(save.HeldItems[10], 84, 0, 1, false, false, false, 0); // Sleep Seed
            TestHeldItem(save.HeldItems[11], 373, 132, 1, true, false, false, 0); // Nifty Box (Purple Gummi)

        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void HeldItems_Read()
        {
            var save = GetTestSave();
            TestHeldItems(save);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void HeldItems_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            TestHeldItems(newSave);

            // Test raw data
            for (int i = 0; i < save.HeldItems.Count; i++)
            {
                Assert.AreEqual(save.HeldItems[i], newSave.HeldItems[i]);
            }
        }

        public void EnsureNoSpEpisodeHeldItems()
        {
            Assert.AreEqual(0, GetTestSave().SpEpisodeHeldItems.Count);
        }

        public void EnsureNoFriendRescueHeldItems()
        {
            Assert.AreEqual(0, GetTestSave().FriendRescueHeldItems.Count);
        }

        #endregion

        #region Stored Pokemon

        private void TestStoredPokemon(SkySave save)
        {
            Assert.AreEqual(97, save.StoredPokemon.Count);

            // Player
            Assert.AreEqual(490, save.StoredPokemon[0].ID.ID);
            Assert.AreEqual(false, save.StoredPokemon[0].ID.IsFemale);
            Assert.AreEqual(60, save.StoredPokemon[0].Level);
            Assert.AreEqual("Evan", save.StoredPokemon[0].Name);

            // Partner
            Assert.AreEqual(430, save.StoredPokemon[1].ID.ID);
            Assert.AreEqual(false, save.StoredPokemon[1].ID.IsFemale);
            Assert.AreEqual(59, save.StoredPokemon[1].Level);
            Assert.AreEqual("Empoleon", save.StoredPokemon[1].Name);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void StoredPokemon_Read()
        {
            var save = GetTestSave();
            TestStoredPokemon(save);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void StoredPokemon_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            TestStoredPokemon(newSave);

            // Ensure raw data is equal
            for (int i = 0; i < save.StoredPokemon.Count; i++)
            {
                Assert.IsTrue(save.StoredPokemon[i].GetStoredPokemonBits().Bits.SequenceEqual(newSave.StoredPokemon[i].GetStoredPokemonBits().Bits));
            }
        }

        #endregion

        #region Active Pokemon

        private void TestActivePokemon(SkySave save)
        {
            Assert.AreEqual(2, save.ActivePokemon.Count);

            // Player
            Assert.AreEqual(490, save.ActivePokemon[0].ID.ID);
            Assert.AreEqual(false, save.ActivePokemon[0].ID.IsFemale);
            Assert.AreEqual(60, save.ActivePokemon[0].Level);
            Assert.AreEqual("Evan", save.ActivePokemon[0].Name);

            // Partner
            Assert.AreEqual(430, save.ActivePokemon[1].ID.ID);
            Assert.AreEqual(false, save.ActivePokemon[1].ID.IsFemale);
            Assert.AreEqual(59, save.ActivePokemon[1].Level);
            Assert.AreEqual("Empoleon", save.ActivePokemon[1].Name);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void ActivePokemon_Read()
        {
            var save = GetTestSave();
            TestActivePokemon(save);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void ActivePokemon_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());

            TestActivePokemon(newSave);

            // Ensure raw data is equal
            for (int i = 0; i < save.ActivePokemon.Count; i++)
            {
                Assert.IsTrue(save.ActivePokemon[i].GetActivePokemonBits().Bits.SequenceEqual(newSave.ActivePokemon[i].GetActivePokemonBits().Bits));
            }
        }

        #endregion

        #region History

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPlayerPokemon_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(489, save.OriginalPlayerPokemon.ID);
            Assert.IsFalse(save.OriginalPlayerPokemon.IsFemale);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPlayerPokemon_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(489, newSave.OriginalPlayerPokemon.ID);
            Assert.IsFalse(newSave.OriginalPlayerPokemon.IsFemale);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPartnerPokemon_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(428, save.OriginalPartnerPokemon.ID);
            Assert.IsFalse(save.OriginalPartnerPokemon.IsFemale);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPartnerPokemon_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(428, newSave.OriginalPartnerPokemon.ID);
            Assert.IsFalse(newSave.OriginalPartnerPokemon.IsFemale);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPlayerName_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual("Evan", save.OriginalPlayerName);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPlayerName_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual("Evan", newSave.OriginalPlayerName);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPartnerName_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual("Piplup", save.OriginalPartnerName);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void OriginalPartnerName_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual("Piplup", newSave.OriginalPartnerName);
        }
        #endregion

        #region Settings


        [TestMethod]
        [TestCategory(TestCategory)]
        public void WindowFrameType_Read()
        {
            var save = GetTestSave();
            Assert.AreEqual(5, save.WindowFrameType);
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void WindowFrameType_Write()
        {
            var save = GetTestSave();
            var newSave = new SkySave(save.ToByteArray());
            Assert.AreEqual(5, save.WindowFrameType);
        }
        #endregion
    }
}
