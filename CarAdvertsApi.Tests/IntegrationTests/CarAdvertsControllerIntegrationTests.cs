using System;
using CarAdvertApi.Controllers;
using CarAdvertApi.Models;
using Xunit;
using CarAdvertsApi.Tests.Helpers;
using CarAdvertsApi.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CarAdvertsApi.Models;
using System.Collections.Generic;

namespace CarAdvertsApi.Tests.IntegrationTests
{
    public class CarAdvertsControllerIntegrationTests
    {
        readonly CarAdvertsController _controller;

        readonly CarAdvertsRepository _service;

        readonly AppDbContext _context;

        public CarAdvertsControllerIntegrationTests()
        {
            _context = TestHelpers.CreateAppDbContext(Guid.NewGuid().ToString());
            _service = new CarAdvertsRepository(_context);
            _controller = new CarAdvertsController(_service);
        }

        [Fact]
        public async Task PostAndGet_OneElement()
        {
            // act
            var postResponse = await _controller.Post(TestHelpers.CreateCarAdvert("test"));
            IActionResult getResponse = await _controller.GetList();

            // assert
            Assert.IsType<NoContentResult>(postResponse);
            var okResult = Assert.IsType<OkObjectResult>(getResponse);
            Assert.Single((IEnumerable<CarAdvert>)okResult.Value);
        }

        [Fact]
        public async Task CreateEditDelete()
        {
            // arrange
            var carAdvert = TestHelpers.CreateCarAdvert("CreateEditDelete");

            // act
            await _controller.Post(carAdvert);

            var getResponse = await _controller.GetList();
            var okResult = Assert.IsType<OkObjectResult>(getResponse);
            Assert.Single((IEnumerable<CarAdvert>)okResult.Value);

            carAdvert.Price = 100000;
            var putResponse = await _controller.Put(carAdvert.Id.Value, carAdvert);
            Assert.IsType<OkResult>(putResponse);

            var deleteResponse = await _controller.Delete(carAdvert.Id.Value);
            Assert.IsType<NoContentResult>(deleteResponse);

            var getAgainResponse = await _controller.GetList();
            var getAgainResponseOk = Assert.IsType<OkObjectResult>(getAgainResponse);
            Assert.Empty((IEnumerable<CarAdvert>)getAgainResponseOk.Value);
        }
    }
}
