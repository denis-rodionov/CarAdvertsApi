using System;
using System.Threading.Tasks;
using CarAdvertApi.Controllers;
using CarAdvertsApi.Repositories;
using CarAdvertsApi.Repositories.impl;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarAdvertsApi.Tests
{
    public class CarAdvertsControllerTest
    {
        readonly Mock<ICarAdvertsRepository> _carAdvertRepository;

        readonly CarAdvertsController _controller;

        public CarAdvertsControllerTest()
        {
            _carAdvertRepository = new Mock<ICarAdvertsRepository>();
            _controller = new CarAdvertsController(_carAdvertRepository.Object);
        }

        [Fact]
        public async Task GetErrorMessage()
        {
            // arrange
            var ex = new Exception("ExceptionMessage");
            _carAdvertRepository.Setup(m => m.FindCarAdvertsAsync("Id", SortOrder.ASK)).ThrowsAsync(ex);

            // act
            var response = await _controller.GetList("Id", "ASK");

            // assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
