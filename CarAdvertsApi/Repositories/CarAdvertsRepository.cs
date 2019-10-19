using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAdvertApi.Models;
using CarAdvertsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAdvertsApi.Repositories
{
    public class CarAdvertsRepository : BaseRepository, ICarAdvertsRepository
    {
        public CarAdvertsRepository(AppDbContext carAdventContext) : base(carAdventContext)
        {
        }

        public async Task<IEnumerable<CarAdvert>> FindCarAdvertsAsync()
        {
            return await _context.CarAdverts.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task SaveCarAdvertAsync(CarAdvert carAdvert)
        {
            _context.Entry(carAdvert).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
    }
}
