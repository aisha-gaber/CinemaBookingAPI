using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Repositories.Interfaces
{
    public interface IAuditoriumRepository
    {
        Task<List<Auditorium>> GetAllAsync();
        Task<Auditorium?> GetByIdAsync(int id);
        Task AddAsync(Auditorium auditorium);
        void Update(Auditorium auditorium);
        void Delete(Auditorium auditorium);
        Task<bool> SaveChangesAsync();
    }
}