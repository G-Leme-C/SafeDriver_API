using Microsoft.EntityFrameworkCore;
using SafeDriver.Domain.Entities;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.Domain.Data
{
    public class SafeDriverDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public SafeDriverDbContext(DbContextOptions<SafeDriverDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().OwnsOne<Coordinate>(e => e.Coordinates);
            modelBuilder.Entity<Trip>().OwnsOne<Coordinate>(t => t.StartingCoordinates);
            modelBuilder.Entity<Trip>().OwnsOne<Coordinate>(t => t.FinalCoordinates);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Driver>().ToTable("drivers");
        }
    }
}