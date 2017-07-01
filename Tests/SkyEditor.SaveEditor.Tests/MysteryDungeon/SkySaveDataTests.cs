using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    }
}
