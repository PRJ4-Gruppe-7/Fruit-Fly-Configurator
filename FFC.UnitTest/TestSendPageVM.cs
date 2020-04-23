using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FFC.Models;
using FFC.Services;
using FFC.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

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
            Assert.AreEqual(_uut._xValue, 921);
        }
    }
}
