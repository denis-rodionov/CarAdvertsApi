using System;
using CarAdvertApi.Models;
using CarAdvertsApi.Models;
using CarAdvertsApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarAdvertsApi.Tests.Helpers
{
    public static class TestHelpers
    {
        public static CarAdvert CreateCarAdvert(string title)
        {
            return new CarAdvert
            {
                Id = Guid.NewGuid(),
                Title = title,
                Fuel = CarFuelTypes.Diesel,
                New = false,
                Price = 5000,
                Mileage = 100000,
                FirstRegistrationDate = new DateTime(2011, 02, 19)
            };
        }

        public static AppDbContext CreateAppDbContext(string contextName)
        {
            var contextBuilder = new DbContextOptionsBuilder<AppDbContext>();
            contextBuilder.UseInMemoryDatabase(contextName);
            return new AppDbContext(contextBuilder.Options);
        }
    }
}
