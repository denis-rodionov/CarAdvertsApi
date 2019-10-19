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
    }
}
