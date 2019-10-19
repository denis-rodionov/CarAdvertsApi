using System;
using CarAdvertsApi.Models;
using Xunit;

namespace CarAdvertsApi.Tests.Models
{
    public class CarAdvertTests
    {
        [Fact]
        public void TestValidation_NewCar_CorrectCase()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "sdfdfgdgf",
                Fuel = CarAdvertsApi.Models.Enums.CarFuelTypes.Diesel,
                Price = 23345,
                New = true
            };

            // act
            var errorList = carAdvert.Validate(null);

            // assert
            Assert.Empty(errorList);
        }

        [Fact]
        public void TestValidation_NewCar_ToManyParameters()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "sdfdfgdgf",
                Fuel = CarAdvertsApi.Models.Enums.CarFuelTypes.Diesel,
                Price = 23345,
                New = true,
                Mileage = 1000
            };

            // act
            var errorList = carAdvert.Validate(null);

            // assert
            Assert.NotEmpty(errorList);
        }

        [Fact]
        public void TestValidation_UsedCar_NormalCase()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "sdfdfgdgf",
                Fuel = CarAdvertsApi.Models.Enums.CarFuelTypes.Diesel,
                Price = 23345,
                New = false,
                Mileage = 213234,
                FirstRegistrationDate = new DateTime()
            };

            // act
            var errorList = carAdvert.Validate(null);

            // assert
            Assert.Empty(errorList);
        }

        [Fact]
        public void TestValidation_UsedCar_MissingParameters()
        {
            // arrange
            var carAdvert = new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = "sdfdfgdgf",
                Fuel = CarAdvertsApi.Models.Enums.CarFuelTypes.Diesel,
                Price = 23345,
                New = false
            };

            // act
            var errorList = carAdvert.Validate(null);

            // assert
            Assert.NotEmpty(errorList);
        }
    }
}
