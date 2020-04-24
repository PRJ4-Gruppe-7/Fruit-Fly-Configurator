using FFC.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCC.Test.ViewModelsTest
{
    [TestFixture]
    class TestRefPointsVM
    {
        private RefPointsViewModel _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new RefPointsViewModel();
        }
    }
}
