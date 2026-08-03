using CinemaBookingAPI.Data;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingAPI.Repositories
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly AppDbContext _context;

        public ShowTimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowTime>> GetAllAsync() =>
            await _context.ShowTimes.Include(s => s.Movie).Include(s => s.Auditorium).ToListAsync();

        public async Task<ShowTime?> GetByIdAsync(int id) =>
            await _context.ShowTimes.Include(s => s.Movie).Include(s => s.Auditorium)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(ShowTime showTime) => await _context.ShowTimes.AddAsync(showTime);

        public void Update(ShowTime showTime) => _context.ShowTimes.Update(showTime);

        public void Delete(ShowTime showTime) => _context.ShowTimes.Remove(showTime);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
