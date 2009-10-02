using NUnit.Framework;

namespace ScotAltNet.Kata
{
    [TestFixture]
    public class RomanNumerialTests
    {
        [Test]
        public void OneToRomanNumerial()
        {
            Assert.AreEqual("I", NumberConverter.ConvertToRoman(1));
        }

        [Test]
        public void TwoToRomanNumerial()
        {
            Assert.AreEqual("II", NumberConverter.ConvertToRoman(2));
        }

        [Test]
        public void ThreeToRomanNumerial()
        {
            Assert.AreEqual("III", NumberConverter.ConvertToRoman(3));
        }

        [Test]
        public void FourToRomanNumerial()
        {
            Assert.AreEqual("IV", NumberConverter.ConvertToRoman(4));
        }

        
        [Test]
        public void FiveToRomanNumerial()
        {
            Assert.AreEqual("V", NumberConverter.ConvertToRoman(5));
        }

        [Test]
        public void HundredToRomanNumerial()
        {
            Assert.AreEqual("C", NumberConverter.ConvertToRoman(100));
        }

        [Test]
        public void SixToRomanNumerial()
        {
            Assert.AreEqual("VI", NumberConverter.ConvertToRoman(6));
        }
        
        [Test]
        public void SevenToEight()
        {
            Assert.AreEqual("VII", NumberConverter.ConvertToRoman(7));
            Assert.AreEqual("VIII", NumberConverter.ConvertToRoman(8));
        }

        [Test]
        public void NineToTenToRomanNumerial()
        {
            Assert.AreEqual("IX", NumberConverter.ConvertToRoman(9));
            Assert.AreEqual("X", NumberConverter.ConvertToRoman(10));
        }


        [Test]
        public void BigNumbersToRomanNumerial()
        {
            Assert.AreEqual("M", NumberConverter.ConvertToRoman(1000));
            Assert.AreEqual("XII", NumberConverter.ConvertToRoman(12));
            Assert.AreEqual("XIII", NumberConverter.ConvertToRoman(13));
            Assert.AreEqual("XIV", NumberConverter.ConvertToRoman(14));
            Assert.AreEqual("XV", NumberConverter.ConvertToRoman(15));
        }

        [Test]
        public void SomeRandomNumbersToRomanNumerial()
        {
            Assert.AreEqual("XXV", NumberConverter.ConvertToRoman(25));
            Assert.AreEqual("XL", NumberConverter.ConvertToRoman(40));
            Assert.AreEqual("XCIX", NumberConverter.ConvertToRoman(99));
            Assert.AreEqual("DCLXVI", NumberConverter.ConvertToRoman(666));
            
        }

        [Test]
        public void ConvertElevenToXI()
        {
            Assert.AreEqual("XI", NumberConverter.ConvertToRoman(11));
        }


        [Test]
        public void NumberToIntArray()
        {
            Assert.AreEqual(
            new int[] {1,0},
            NumberConverter.GetIntArrayFromNumber(10));

            Assert.AreEqual(
            new int[] { 1, 4, 4, 4 },
            NumberConverter.GetIntArrayFromNumber(1444));

            Assert.AreEqual(
            new int[] { 1, 2, 4, 9 },
            NumberConverter.GetIntArrayFromNumber(1249));

            Assert.AreEqual(
            new int[] { 3, 4, 9, 9 },
            NumberConverter.GetIntArrayFromNumber(3499));

            Assert.AreEqual(
            new int[] { 1 },
            NumberConverter.GetIntArrayFromNumber(1));

            Assert.AreEqual(
            new int[] { 2, 5, 6 },
            NumberConverter.GetIntArrayFromNumber(256));
        }
    
    }
}
