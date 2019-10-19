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
            modelBuilder.Entity<CarAdvert>().ToTable("CarAdvets");
            modelBuilder.Entity<CarAdvert>().HasKey(p => p.Id);

            // The Null check validation does not currently work with InMemory database:
            // https://github.com/aspnet/EntityFrameworkCore/issues/7228
            modelBuilder.Entity<CarAdvert>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<CarAdvert>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<CarAdvert>().Property(p => p.Fuel).IsRequired();
            modelBuilder.Entity<CarAdvert>().Property(p => p.New).IsRequired();
        }
    }
}