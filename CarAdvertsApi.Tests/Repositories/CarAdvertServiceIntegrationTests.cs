using System;
using System.Threading.Tasks;
using CarAdvertApi.Models;
using CarAdvertsApi.Models;
using CarAdvertsApi.Models.Enums;
using CarAdvertsApi.Repositories;
using CarAdvertsApi.Tests.Helpers;
using Xunit;

namespace CarAdvertsApi.Tests.IntegrationTests
{
    public class CarAdvertsRepositoryIntegrationTests
    {
        readonly AppDbContext _context;

        readonly CarAdvertsRepository _repository;

        public CarAdvertsRepositoryIntegrationTests()
        {
            _context = TestHelpers.CreateAppDbContext(Guid.NewGuid().ToString());
            _repository = new CarAdvertsRepository(_context);
        }

        [Fact]
        public async Task TestSaveFind_AllFields()
        {
            // arrange
            var newCarAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "Opel Astra",
                Fuel = CarFuelTypes.Gasoline,
                Price = 5000,
                New = false,
                Mileage = 90000,
                FirstRegistrationDate = new DateTime(2011, 6, 23)
            };

            // act
            await _repository.SaveCarAdvertAsync(newCarAdvert);
            var results = await _repository.FindCarAdvertsAsync();

            // assert
            var actual = Assert.Single(results);
            Assert.Equal(newCarAdvert.Id, actual.Id);
            Assert.Equal(newCarAdvert.Title, actual.Title);
            Assert.Equal(newCarAdvert.Fuel, actual.Fuel);
            Assert.Equal(newCarAdvert.Price, actual.Price);
            Assert.Equal(newCarAdvert.New, actual.New);
            Assert.Equal(newCarAdvert.Mileage, actual.Mileage);
            Assert.Equal(newCarAdvert.FirstRegistrationDate, actual.FirstRegistrationDate);
        }

        [Fact]
        public async Task TestSave_FailIfAlreadyExists()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "Opel Astra"
            };
            await _repository.SaveCarAdvertAsync(carAdvert);

            var modifiedCarAdvent = new CarAdvert
            {
                Id = carAdvert.Id,
                Title = "Opel Corsa"
            };

            // act
            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.SaveCarAdvertAsync(modifiedCarAdvent));
        }
    }
}
