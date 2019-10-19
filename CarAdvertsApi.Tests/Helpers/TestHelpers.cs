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
                Title = title
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
