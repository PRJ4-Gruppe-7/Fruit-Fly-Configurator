using NUnit.Framework;
using FFC.ViewModels;
using System;

namespace FCC.Test.ViewModelsTest
{
    [TestFixture]
    public class TestSendPageVM
    {
        private SendPageViewModel _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new SendPageViewModel();
        }

        #region Properties

        ///////////////////////////////////////
        ////// Testing XValue/_xValue ////////
        /////////////////////////////////////
        
        [TestCase("0")]
        [TestCase("100")]
        [TestCase("1000")]
        [TestCase("10000")]
        [TestCase("2147483647")]
        public void XValue_ValidValue_Succes(string value)
        {
            _uut.XValue = value;
            Assert.AreEqual(Int32.Parse(value), _uut._xValue);
        }

        [TestCase("-1")]
        [TestCase("2147483649")]
        [TestCase("-2147483649")]
        [TestCase("test")]
        [TestCase("t232")]
        public void XValue_InvalidValue_Fail(string value)
        {
            _uut.XValue = value;
            Assert.AreEqual(0, _uut._xValue);
        }


        ///////////////////////////////////////
        ////// Testing YValue/_yValue ////////
        /////////////////////////////////////
        
        [TestCase("0")]
        [TestCase("100")]
        [TestCase("1000")]
        [TestCase("10000")]
        [TestCase("2147483647")]
        public void YValue_ValidValue_Succes(string value)
        {
            _uut.YValue = value;
            Assert.AreEqual(Int32.Parse(value), _uut._yValue);
        }

        [TestCase("-1")]
        [TestCase("2147483649")]
        [TestCase("-2147483649")]
        [TestCase("test")]
        [TestCase("t232")]
        public void YValue_InvalidValue_Fail(string value)
        {
            _uut.YValue = value;
            Assert.AreEqual(0, _uut._yValue);
        }

        ///////////////////////////////////////
        ////// Testing RSSIValue/_rssi ////////
        /////////////////////////////////////

        [TestCase("0")]
        [TestCase("100")]
        [TestCase("1000")]
        [TestCase("10000")]
        [TestCase("2147483647")]
        public void RSSIValue_ValidValue_Succes(string value)
        {
            _uut.RSSIValue = value;
            Assert.AreEqual(Int32.Parse(value), _uut._rssi);
        }

        [TestCase("-1")]
        [TestCase("2147483649")]
        [TestCase("-2147483649")]
        [TestCase("test")]
        [TestCase("t232")]
        public void RSSIValue_InvalidValue_Fail(string value)
        {
            _uut.RSSIValue = value;
            Assert.AreEqual(0, _uut._rssi);
        }

        #endregion

        #region Commands

        ///////////////////////////////////////
        ////// Testing XValue/_xValue ////////
        /////////////////////////////////////

        [Test]
        public void IncrementCommand_X_ValidValue_Succes()
        {
            for(int i = 0; i < 10; i++)
                _uut.IncrementCommand.Execute("X");
            Assert.AreEqual(10, _uut._xValue);
        }

        #endregion
    }
}