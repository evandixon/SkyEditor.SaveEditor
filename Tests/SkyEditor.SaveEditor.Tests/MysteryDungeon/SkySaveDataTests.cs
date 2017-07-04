﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.Tests.MysteryDungeon
{
    [TestClass]
    public class SkySaveDataTests
    {
        public const string TestCategory = "Explorers of Sky Data Tests";

        private SkySave GetTestSave()
        {
            return new SkySave(DataUtil.GetBinaryResource("EoS.sav"));
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestChecksumsValid()
        {
            var save = GetTestSave();
            Assert.IsTrue(save.IsPrimaryChecksumValid());
            Assert.IsTrue(save.IsSecondaryChecksumValid());
            Assert.IsTrue(save.IsQuickSaveChecksumValid());
        }

        private void TestItem(SkyItem item, int id, int containedItem, int quantity, bool isBox, bool isUsedTM, bool isStackable)
        {
            Assert.AreEqual(id, item.ID);
            Assert.AreEqual(containedItem, item.ContainedItem);
            Assert.AreEqual(quantity, item.Quantity);
            Assert.AreEqual(isBox, item.IsBox);
            Assert.AreEqual(isUsedTM, item.IsUsedTM);
            Assert.AreEqual(isStackable, item.IsStackableItem);
        }

        private void TestHeldItem(SkyHeldItem item, int id, int containedItem, int quantity, bool isBox, bool isUsedTM, bool isStackable, int heldBy)
        {
            Assert.IsTrue(item.IsValid, "Invalid items shouldn't be accessible");
            Assert.AreEqual(id, item.ID);
            Assert.AreEqual(containedItem, item.ContainedItem);
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
        }

        public void EnsureNoSpEpisodeHeldItems()
        {
            Assert.AreEqual(0, GetTestSave().SpEpisodeHeldItems.Count);
        }

        public void EnsureNoFriendRescueHeldItems()
        {
            Assert.AreEqual(0, GetTestSave().FriendRescueHeldItems.Count);
        }
    }
}
