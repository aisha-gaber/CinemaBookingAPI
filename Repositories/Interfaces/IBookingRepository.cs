using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        Task<bool> SaveChangesAsync();
    }
}
