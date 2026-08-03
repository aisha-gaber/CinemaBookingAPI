using Microsoft.EntityFrameworkCore;
using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<ShowTime>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Shows)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<ShowTime>()
                .HasOne(s => s.Auditorium)
                .WithMany(a => a.Shows)
                .HasForeignKey(s => s.AuditoriumId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ShowTime)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ShowTimeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}