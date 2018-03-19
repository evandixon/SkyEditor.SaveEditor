using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.Tests
{
    [TestClass]
    public class BitBlockTests
    {
        public const string TestCategory = "BitBlock Tests";

        [TestMethod]
        [TestCategory(TestCategory)]
        public void GetInt_ByteAligned()
        {
            var testBlock = new BitBlock(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 5, 0, 0, 0 });
            Assert.AreEqual(-1, testBlock.GetInt(0, 0, 32));
            Assert.AreEqual(5, testBlock.GetInt(4, 0, 32));
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void GetUInt_ByteAligned()
        {
            var testBlock = new BitBlock(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 5, 0, 0, 0 });
            Assert.AreEqual(uint.MaxValue, testBlock.GetUInt(0, 0, 32));
            Assert.AreEqual((uint)5, testBlock.GetUInt(4, 0, 32));
        }
    }
}
