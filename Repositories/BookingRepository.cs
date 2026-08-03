using CinemaBookingAPI.Data;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllAsync() =>
            await _context.Bookings.Include(b => b.Customer).Include(b => b.ShowTime).ToListAsync();

        public async Task<Booking?> GetByIdAsync(int id) =>
            await _context.Bookings.Include(b => b.Customer).Include(b => b.ShowTime)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task AddAsync(Booking booking) => await _context.Bookings.AddAsync(booking);

        public void Update(Booking booking) => _context.Bookings.Update(booking);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
