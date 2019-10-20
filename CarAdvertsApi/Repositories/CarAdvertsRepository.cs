using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarAdvertApi.Models;
using CarAdvertsApi.Models;
using CarAdvertsApi.Repositories.impl;
using Microsoft.EntityFrameworkCore;

namespace CarAdvertsApi.Repositories
{
    public class CarAdvertsRepository : BaseRepository, ICarAdvertsRepository
    {
        public CarAdvertsRepository(AppDbContext carAdventContext) : base(carAdventContext)
        {
        }

        public async Task<IEnumerable<CarAdvert>> FindCarAdvertsAsync(string sortKey, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.ASK)
                return await _context.CarAdverts.OrderBy(sortKey).ToListAsync();
            else
                return await _context.CarAdverts.OrderByDescending(sortKey).ToListAsync();
        }

        public async Task<IEnumerable<CarAdvert>> FindCarAdvertsAsync()
        {
            return await _context.CarAdverts.ToListAsync();
        }

        public async Task SaveCarAdvertAsync(CarAdvert carAdvert)
        {
            _context.Entry(carAdvert).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAdvertAsync(CarAdvert carAdvert)
        {
            _context.Entry(carAdvert).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<CarAdvert> GetCarAdvertAsync(Guid id)
        {
            var entity = await _context.CarAdverts.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }
    }
}
