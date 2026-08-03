using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Repositories.Interfaces
{
    public interface IShowTimeRepository
    {
        Task<List<ShowTime>> GetAllAsync();
        Task<ShowTime?> GetByIdAsync(int id);
        Task AddAsync(ShowTime showTime);
        void Update(ShowTime showTime);
        void Delete(ShowTime showTime);
        Task<bool> SaveChangesAsync();
    }
}