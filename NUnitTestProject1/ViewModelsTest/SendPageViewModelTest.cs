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

        [TestCase("0")]
        [TestCase("100")]
        [TestCase("1000")]
        [TestCase("10000")]
        [TestCase("32767")]
        public void XValue_ValidValue_Succes(string value)
        {
            _uut.XValue = value;
            Assert.AreEqual(_uut._xValue, Int32.Parse(value));
        }
    }
}