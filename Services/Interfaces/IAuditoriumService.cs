using CinemaBookingAPI.Models.DTOs;

namespace CinemaBookingAPI.Services.Interfaces
{
    public interface IAuditoriumService
    {
        Task<List<AuditoriumDto>> GetAllAsync();
        Task<AuditoriumDto> GetByIdAsync(int id);
        Task<AuditoriumDto> CreateAsync(CreateAuditoriumRequest request);
        Task<AuditoriumDto> UpdateAsync(int id, UpdateAuditoriumRequest request);
        Task DeleteAsync(int id);
    }
}
