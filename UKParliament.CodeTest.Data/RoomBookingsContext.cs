using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data
{
    public class RoomBookingsContext : DbContext
    {
        public RoomBookingsContext(DbContextOptions<RoomBookingsContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(p => new { p.Id, p.DateOfBirth, p.LastName, p.Name, p.PostCode });
            modelBuilder.Entity<Room>().HasKey(r => new { r.Name });
            modelBuilder.Entity<Room>().Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Room>().HasAlternateKey(r => r.Id);
        }
    }
}