using ArquitectChallenge.Domain.Utilities;
using NUnit.Framework;

namespace ArquitectChallenge.UnitTests
{
    [TestFixture]
    public class UtilExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("brasil", "brasil")]
        [TestCase("brasil.norte", "brasil")]
        [TestCase("brasil.norte.sensor01", "brasil")]
        [TestCase("brasil.nordeste", "brasil")]
        [TestCase("brasil.nordeste.sensor01", "brasil")]
        [TestCase("brasil.sudeste", "brasil")]
        [TestCase("brasil.sudeste.sensor01", "brasil")]
        [TestCase("brasil.sul", "brasil")]
        [TestCase("brasil.sul.sensor01", "brasil")]
        public void GetFirstTag(string tag, string firstTag)
        {
            Assert.AreEqual(firstTag, tag.FirstTag());
        }

        [TestCase("brasil.norte", "brasil.norte")]
        [TestCase("brasil.norte.sensor01", "brasil.norte")]
        [TestCase("brasil.nordeste", "brasil.nordeste")]
        [TestCase("brasil.nordeste.sensor01", "brasil.nordeste")]
        [TestCase("brasil.sudeste", "brasil.sudeste")]
        [TestCase("brasil.sudeste.sensor01", "brasil.sudeste")]
        [TestCase("brasil.sul", "brasil.sul")]
        [TestCase("brasil.sul.sensor01", "brasil.sul")]
        public void GetSecondTag(string tag, string firstTag)
        {
            Assert.AreEqual(firstTag, tag.SecondTag());
        }

        [TestCase("A")]
        [TestCase("B")]
        [TestCase("C 1")]
        public void IsntNumeric(string value)
        {
            Assert.IsFalse(value.IsNumeric());
        }

        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("1")]
        public void IsNumeric(string value)
        {
            Assert.IsTrue(value.IsNumeric());
        }
    }
}