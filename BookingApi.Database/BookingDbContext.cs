using BookingApi.Saga;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Database;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<Traveler> Travelers { get; set; }

    public DbSet<BookingSagaData> BookingSagaData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingSagaData>().HasKey(s => s.CorrelationId);
    }
}