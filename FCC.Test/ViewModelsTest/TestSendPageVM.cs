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

        /////////////////////////////////////
        //// Testing XValue/_xValue ////////
        ///////////////////////////////////

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(2147483646)]
        public void XValue_ValidValue_Succes(int value)
        {
            _uut.CurrentReference.x = value;
            Assert.AreEqual(value, _uut.CurrentReference.x);
        }

        [TestCase(2147483648)]
        public void XValue_InvalidValue_Fail(uint value)
        {
            _uut.CurrentReference.x = (Int32)value;
            Assert.AreEqual(0, _uut.CurrentReference.x);
        }


        ///////////////////////////////////////
        ////// Testing YValue/_yValue ////////
        /////////////////////////////////////

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(2147483646)]
        public void YValue_ValidValue_Succes(int value)
        {
            _uut.CurrentReference.y = value;
            Assert.AreEqual(value, _uut.CurrentReference.y);
        }

        [TestCase(2147483648)]
        public void YValue_InvalidValue_Fail(uint value)
        {
            _uut.CurrentReference.y = (Int32)value;
            Assert.AreEqual(0, _uut.CurrentReference.y);
        }


        //#endregion

        //#region Commands

        ///////////////////////////////////////
        ////// Testing IncrementCommand //////
        /////////////////////////////////////

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void IncrementCommand_x_ValidValue_Succes(int value)
        {
            for (int i = 0; i < value; i++)
                _uut.IncrementCommand.Execute("x");
            Assert.AreEqual(value, _uut.CurrentReference.x);
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void IncrementCommand_y_ValidValue_Succes(int value)
        {
            for (int i = 0; i < value; i++)
                _uut.IncrementCommand.Execute("y");
            Assert.AreEqual(value, _uut.CurrentReference.y);
        }

        /////////////////////////////////////////
        ////// DecrementXCommandCanExecute /////
        ///////////////////////////////////////

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(1000)]
        public void DecrementXCommandCanExecute_Succes(int value)
        {
            _uut.CurrentReference.x = value;
            Assert.IsTrue(_uut.DecrementXCommand.CanExecute(null));
        }

        [TestCase(0)]
        public void DecrementXCommandCanExecute_Fail(int value)
        {
            _uut.CurrentReference.x = value;
            Assert.IsFalse(_uut.DecrementXCommand.CanExecute(null));
        }

        ///////////////////////////////////////
        //// DecrementYCommandCanExecute /////
        /////////////////////////////////////

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(1000)]
        public void DecrementYCommandCanExecute_Succes(int value)
        {
            _uut.CurrentReference.y = value;
            Assert.IsTrue(_uut.DecrementYCommand.CanExecute(null));
        }

        [TestCase(0)]
        public void DecrementYCommandCanExecute_Fail(int value)
        {
            _uut.CurrentReference.y = value;
            Assert.IsFalse(_uut.DecrementYCommand.CanExecute(null));
        }

        #endregion
    }
}