using ArquitectChallenge.Domain;
using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Domain.Utilities;
using NUnit.Framework;
using System;

namespace ArquitectChallenge.UnitTests
{
    [TestFixture]
    public class EventDataTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region " TEST CASES "

        [TestCase("")]
        public void EventWithError(string value)
        {
            var numericEvent = new EventData { Valor = value, Tag = "brasil.centrooeste", Timestamp = GetCurrentTimeStamp() };
            numericEvent.PrepareModel();
            Assert.AreEqual(EnumStatus.Error, numericEvent.Status);
        }

        [TestCase("First value")]
        [TestCase("Second value")]
        [TestCase("Third value")]
        [TestCase("1º value")]
        [TestCase("2º value")]
        [TestCase("3º value")]
        [TestCase("")]
        public void IsntNumericEvent(string value)
        {
            var numericEvent = new EventData { Valor = value, Tag = "brasil.centrooeste", Timestamp = GetCurrentTimeStamp() };
            numericEvent.PrepareModel();
            Assert.IsFalse(numericEvent.IsNumeric);
        }

        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("1")]
        public void IsNumericEvent(string value)
        {
            var numericEvent = new EventData { Valor = value, Tag = "brasil.centrooeste", Timestamp = GetCurrentTimeStamp() };
            numericEvent.PrepareModel();
            Assert.IsTrue(numericEvent.IsNumeric);
        }

        [TestCase("1")]
        [TestCase("1 Event")]
        public void SucessfulEvent(string value)
        {
            var numericEvent = new EventData { Valor = value, Tag = "brasil.centrooeste", Timestamp = GetCurrentTimeStamp() };
            numericEvent.PrepareModel();
            Assert.AreEqual(EnumStatus.Done, numericEvent.Status);
        }

        #endregion " TEST CASES "

        #region " PRIVATE METHODS "

        private long GetCurrentTimeStamp()
        {
            return DateTime.Now.DateTimeToUnixTimestamp();
        }

        #endregion " PRIVATE METHODS "
    }
}