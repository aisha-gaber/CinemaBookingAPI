using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie?> GetByNameAsync(string name);
        Task AddAsync(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
        Task<bool> SaveChangesAsync();
    }
}