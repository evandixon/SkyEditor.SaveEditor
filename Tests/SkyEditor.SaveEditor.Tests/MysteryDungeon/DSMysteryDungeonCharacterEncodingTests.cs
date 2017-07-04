using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.Tests.MysteryDungeon
{
    [TestClass()]
    public class DSMysteryDungeonCharacterEncodingTests
    {
        public const string TestCategory = "Character Encoding";
        [TestMethod()]
        [TestCategory(TestCategory)]
        public void BasicNamesTests()
        {
            var e = new DSMysteryDungeonCharacterEncoding();
            string[] testNames = {
                "Riolu",
                "Poochyena",
                "Pikachu",
                "Test Name",
                "Accent Test: éèê",
                "♀♂",
                "",
                Environment.NewLine
            };
            foreach (var item in testNames)
            {
                var bytes = e.GetBytes(item);
                var back = e.GetString(bytes);
                Assert.AreEqual(item, back);
            }
        }

        [TestMethod()]
        [TestCategory(TestCategory)]
        public void BasicEscapeTests()
        {
            var e = new DSMysteryDungeonCharacterEncoding();
            string[] testNames = {
                "\\81FF",
                "This\\That",
                "Line End Test\\"
            };
            foreach (var item in testNames)
            {
                var bytes = e.GetBytes(item);
                var back = e.GetString(bytes);
                Assert.AreEqual(item, back);
            }
        }

        [TestMethod()]
        [TestCategory(TestCategory)]
        public void ByteCountTests()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("\\81FF", 2);
            data.Add("Riolu", 5);
            data.Add("Poochyena", 9);
            data.Add("This\\That", 9);

            var e = new DSMysteryDungeonCharacterEncoding();
            foreach (var item in data)
            {
                var count = e.GetByteCount(item.Key);
                Assert.AreEqual(item.Value, count);
            }
        }

        [TestMethod()]
        [TestCategory(TestCategory)]
        public void NullCharacterTest()
        {
            var e = new DSMysteryDungeonCharacterEncoding();
            byte[] sequence = {
                0x50,
                0x6f,
                0x6f,
                0x63,
                0x68,
                0x79,
                0x65,
                0x6e,
                0x61,
                0,
                0x52,
                0x69,
                0x6f,
                0x6c,
                0x75
            };
            var back = e.GetString(sequence);
            Assert.AreEqual("Poochyena", back);
        }

        [TestMethod()]
        [TestCategory(TestCategory)]
        public void BinaryToStringToBinary()
        {
            var e = new DSMysteryDungeonCharacterEncoding();
            byte[] sequence = {
                0x50,
                0x6f,
                0x6f,
                0x63,
                0x68,
                0x79,
                0x65,
                0x6e,
                0x61
            };
            var sequenceString = e.GetString(sequence);
            //Should be "Poochyena"
            byte[] convertedBack = e.GetBytes(sequenceString);
            Assert.IsTrue(sequence.SequenceEqual(convertedBack), "Resulting byte array is not equal to the original sequence");
        }

    }
}
