using CinemaBookingAPI.Models.DTOs;

namespace CinemaBookingAPI.Services.Interfaces
{
    public interface IShowTimeService
    {
        Task<List<ShowTimeDto>> GetAllAsync();
        Task<ShowTimeDto> GetByIdAsync(int id);
        Task<ShowTimeDto> CreateAsync(CreateShowTimeRequest request);
        Task<ShowTimeDto> UpdateAsync(int id, UpdateShowTimeRequest request);
        Task DeleteAsync(int id);
    }
}