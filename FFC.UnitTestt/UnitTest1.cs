using FFC.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace FFC.UnitTest
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
            NUnit.Framework.Assert.AreEqual(_uut._xValue, 921);
        }
    }
}
