using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FFC.Models;
using FFC.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;

namespace FFC.UnitTest
{
    [TestFixture]
    public class TestRestApiService
    {
        private IRestApiService _uut;
        private Reference _testObject;

        [SetUp]
        public void Setup()
        {
            _uut = new RestApiService();
            _testObject = new Reference();
        }

        [TestCase(0,0,0)]
        public async Task PostReferenceAsync_ValidContent_PostingSucceeded(int x, int y, int rssi1)
        {
            _testObject.x = x;
            _testObject.y = y;
            _testObject.rssI1 = rssi1;

            await _uut.PostReferenceAsync(_testObject);
        }
    }
}
