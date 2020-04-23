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
    }
}