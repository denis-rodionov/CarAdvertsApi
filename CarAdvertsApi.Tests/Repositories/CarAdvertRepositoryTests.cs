using System;
using System.Linq;
using System.Threading.Tasks;
using CarAdvertApi.Models;
using CarAdvertsApi.Models;
using CarAdvertsApi.Models.Enums;
using CarAdvertsApi.Repositories;
using CarAdvertsApi.Repositories.impl;
using CarAdvertsApi.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarAdvertsApi.Tests.IntegrationTests
{
    public class CarAdvertsRepositoryIntegrationTests
    {
        readonly AppDbContext _context;

        readonly CarAdvertsRepository _repository;

        readonly CarAdvert advert0 = new CarAdvert
        {
            Id = Guid.Parse("3f9619ff-8b86-d011-b42d-00cf4fc964fa"),
            Title = "a",
            Fuel = CarFuelTypes.Gasoline,
            Price = 20,
            New = false,
            Mileage = 200,
            FirstRegistrationDate = new DateTime(2011, 02, 02)
        };

        readonly CarAdvert advert1 = new CarAdvert
        {
            Id = Guid.Parse("2f9619ff-8b86-d011-b42d-00cf4fc964fa"),
            Title = "b",
            Fuel = CarFuelTypes.Diesel,
            Price = 10,
            New = true
        };

        readonly CarAdvert advert2 = new CarAdvert
        {
            Id = Guid.Parse("1f9619ff-8b86-d011-b42d-00cf4fc964fa"),
            Title = "c",
            Fuel = CarFuelTypes.Gasoline,
            Price = 30,
            New = false,
            Mileage = 100,
            FirstRegistrationDate = new DateTime(2012, 02, 02)
        };

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

        [Fact]
        public async Task TestSorting_Id_Ask()
        {
            // arrange
            await _repository.SaveCarAdvertAsync(advert0);
            await _repository.SaveCarAdvertAsync(advert1);
            await _repository.SaveCarAdvertAsync(advert2);

            // act
            var sortedResult = await _repository.FindCarAdvertsAsync("Id", SortOrder.ASK);

            // assert
            // should be sorted by ID by default
            Assert.Equal(advert2.Id, sortedResult.ToArray()[0].Id);
            Assert.Equal(advert1.Id, sortedResult.ToArray()[1].Id);
            Assert.Equal(advert0.Id, sortedResult.ToArray()[2].Id);
        }

        [Fact]
        public async Task TestSorting_Price_Desk()
        {
            // arrange
            await _repository.SaveCarAdvertAsync(advert0);
            await _repository.SaveCarAdvertAsync(advert1);
            await _repository.SaveCarAdvertAsync(advert2);

            // act
            var sortedResult = await _repository.FindCarAdvertsAsync("Price", SortOrder.DESK);

            // assert
            // should be sorted by ID by default
            Assert.Equal(advert2.Id, sortedResult.ToArray()[0].Id);
            Assert.Equal(advert0.Id, sortedResult.ToArray()[1].Id);
            Assert.Equal(advert1.Id, sortedResult.ToArray()[2].Id);
        }

        [Fact]
        public async Task TestSorting_Mileage_Desk()
        {
            // arrange
            await _repository.SaveCarAdvertAsync(advert0);
            await _repository.SaveCarAdvertAsync(advert1);
            await _repository.SaveCarAdvertAsync(advert2);

            // act
            var sortedResult = await _repository.FindCarAdvertsAsync("mileage", SortOrder.DESK);

            // assert
            // should be sorted by ID by default
            Assert.Equal(advert0.Id, sortedResult.ToArray()[0].Id);
            Assert.Equal(advert2.Id, sortedResult.ToArray()[1].Id);
            Assert.Equal(advert1.Id, sortedResult.ToArray()[2].Id);
        }

        [Fact]
        public async Task TestUpdate_AllFields()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "Opel Astra",
                Fuel = CarFuelTypes.Gasoline,
                Price = 5000,
                New = false,
                Mileage = 90000,
                FirstRegistrationDate = new DateTime(2011, 6, 23)
            };
            await _repository.SaveCarAdvertAsync(carAdvert);

            carAdvert.Title = "Opel Corsa";
            carAdvert.Fuel = CarFuelTypes.Diesel;
            carAdvert.Price = 6000;
            carAdvert.New = false;
            carAdvert.Mileage = 10000;
            carAdvert.FirstRegistrationDate = new DateTime(2012, 6, 23);

            // act
            await _repository.UpdateCarAdvertAsync(carAdvert);

            // assert
            var results = await _repository.FindCarAdvertsAsync();
            var actual = Assert.Single(results);
            Assert.Equal(carAdvert.Id, actual.Id);
            Assert.Equal(carAdvert.Title, actual.Title);
            Assert.Equal(carAdvert.Fuel, actual.Fuel);
            Assert.Equal(carAdvert.Price, actual.Price);
            Assert.Equal(carAdvert.New, actual.New);
            Assert.Equal(carAdvert.Mileage, actual.Mileage);
            Assert.Equal(carAdvert.FirstRegistrationDate, actual.FirstRegistrationDate);
        }

        [Fact]
        public async Task TestUpdate_FailIfDoesNotExist()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "Opel Astra"
            };

            // act
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _repository.UpdateCarAdvertAsync(carAdvert));
        }

        [Fact]
        public async Task TestGet_Found()
        {
            // arrange
            var carAdvert = TestHelpers.CreateCarAdvert("TestGet_Found");
            await _repository.SaveCarAdvertAsync(carAdvert);

            // act
            var actual = await _repository.GetCarAdvertAsync(carAdvert.Id.Value);

            // assert
            Assert.NotNull(actual);
            Assert.Equal(carAdvert.Title, actual.Title);
        }

        [Fact]
        public async Task TestGet_NotFound()
        {
            // arrange
            var id = Guid.NewGuid();

            // act
            var actual = await _repository.GetCarAdvertAsync(id);

            // assert
            Assert.Null(actual);
        }
    }
}
