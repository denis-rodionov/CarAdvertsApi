using System;
using CarAdvertApi.Models;

namespace CarAdvertsApi.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
    }
}
