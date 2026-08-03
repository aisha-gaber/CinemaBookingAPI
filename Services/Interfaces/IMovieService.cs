using CinemaBookingAPI.Models.DTOs;

namespace CinemaBookingAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<PagedResult<MovieDtoV2>> GetAllAsync(string? search, string? genre, string? sortBy, bool descending, int page, int pageSize);
        Task<MovieDtoV2> GetByIdAsync(int id);
        Task<MovieDtoV2> CreateAsync(CreateMovieRequest request);
        Task<MovieDtoV2> UpdateAsync(int id, UpdateMovieRequest request);
        Task DeleteAsync(int id);
    }
}