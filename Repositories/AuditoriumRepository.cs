using CinemaBookingAPI.Data;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingAPI.Repositories
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly AppDbContext _context;

        public AuditoriumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auditorium>> GetAllAsync() => await _context.Auditoriums.ToListAsync();

        public async Task<Auditorium?> GetByIdAsync(int id) => await _context.Auditoriums.FindAsync(id);

        public async Task AddAsync(Auditorium auditorium) => await _context.Auditoriums.AddAsync(auditorium);

        public void Update(Auditorium auditorium) => _context.Auditoriums.Update(auditorium);

        public void Delete(Auditorium auditorium) => _context.Auditoriums.Remove(auditorium);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
