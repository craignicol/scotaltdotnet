using NUnit.Framework;

namespace ScotAltNet.Kata
{
    [TestFixture]
    public class UrnfieldTests
    {

        [Test()]
        public void ConvertForwardSlashToOne_Convert_Success()
        {
            string result = NumberConverter.ConvertToUrnfield(1);
            Assert.AreEqual("/", result);
        }

        [Test]
        public void ConvertToUrnfieldTwo_Convert_Success()
        {
            string result = NumberConverter.ConvertToUrnfield(2);
            Assert.AreEqual("//", result);
        }

        [Test]
        public void ConvertToUrnfield3_Convert_Success()
        {
            string result = NumberConverter.ConvertToUrnfield(3);
            Assert.AreEqual("///", result);
        }

        [Test]
        public void ConvertToUrnfield4_Convert_Success()
        {
            string result = NumberConverter.ConvertToUrnfield(4);
            Assert.AreEqual("////", result);
        }

        [Test]
        public void ConvertToUrnfield5_Convert_Success()
        {
            string result = NumberConverter.ConvertToUrnfield(5);
            Assert.AreEqual(@"\", result);
        }

        [Test]
        public void ConvertToUrnfield6To10()
        {
            Assert.AreEqual(@"/\", NumberConverter.ConvertToUrnfield(6));
            Assert.AreEqual(@"//\", NumberConverter.ConvertToUrnfield(7));
            Assert.AreEqual(@"///\", NumberConverter.ConvertToUrnfield(8));
            Assert.AreEqual(@"////\", NumberConverter.ConvertToUrnfield(9));
            Assert.AreEqual(@"\\", NumberConverter.ConvertToUrnfield(10));
        }

        [Test]
        public void GetRemanderReturns3whenGiven3()
        {
            double result = NumberConverter.GetRemainder(3);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void TestEdgeCases()
        {
            Assert.AreEqual(@"////\\\\\", NumberConverter.ConvertToUrnfield(29));
        }

        
        
 
    }
}
