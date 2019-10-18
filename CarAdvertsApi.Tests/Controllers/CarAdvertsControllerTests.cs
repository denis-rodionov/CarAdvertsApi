using System;
using System.Collections.Generic;
using CarAdvertApi.Controllers;
using CarAdvertsApi.Models;
using Xunit;

namespace CarAdvertsApi.Tests
{
    public class CarAdvertsControllerTest
    {
        readonly CarAdvertsController _controller;

        public CarAdvertsControllerTest()
        {
            _controller = new CarAdvertsController();
        }

        [Fact]
        public void Get_NotEmptyList()
        {
            // act
            IEnumerable<CarAdvert> carAdvertsList = _controller.Get();

            // assert
            Assert.NotEmpty(carAdvertsList);
        }
    }
}
