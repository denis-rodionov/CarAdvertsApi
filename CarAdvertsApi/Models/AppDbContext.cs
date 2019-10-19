using CarAdvertsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAdvertApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<CarAdvert> CarAdverts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarAdvert>().ToTable("CarAdvets");
            modelBuilder.Entity<CarAdvert>().HasKey(p => p.Id);
        }
    }
}