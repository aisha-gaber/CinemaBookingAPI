using CinemaBookingAPI.Exceptions;
using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MovieDtoV2>> GetAllAsync(string? search, string? genre, string? sortBy, bool descending, int page, int pageSize)
        {
            var movies = await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
                movies = movies.Where(m => m.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(genre))
                movies = movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            movies = sortBy?.ToLower() switch
            {
                "name" => descending ? movies.OrderByDescending(m => m.Name).ToList() : movies.OrderBy(m => m.Name).ToList(),
                "releasedate" => descending ? movies.OrderByDescending(m => m.ReleaseDate).ToList() : movies.OrderBy(m => m.ReleaseDate).ToList(),
                _ => movies
            };

            var paged = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paged.Select(MapToDtoV2).ToList();
        }

        public async Task<MovieDtoV2> GetByIdAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id)
                ?? throw new MovieNotFoundException($"Movie with id {id} not found.");
            return MapToDtoV2(movie);
        }

        public async Task<MovieDtoV2> CreateAsync(CreateMovieRequest request)
        {
            var existing = await _repository.GetByNameAsync(request.Name);
            if (existing != null)
                throw new MovieAlreadyExistsException($"A movie named {request.Name} already exists.");

            var movie = new Movie
            {
                Name = request.Name,
                Genre = request.Genre,
                ReleaseDate = request.ReleaseDate,
                AvailableInCinema = request.AvailableInCinema
            };

            await _repository.AddAsync(movie);
            await _repository.SaveChangesAsync();

            return MapToDtoV2(movie);
        }

        public async Task<MovieDtoV2> UpdateAsync(int id, UpdateMovieRequest request)
        {
            var movie = await _repository.GetByIdAsync(id)
                ?? throw new MovieNotFoundException($"Movie with id {id} not found.");

            movie.Name = request.Name;
            movie.Genre = request.Genre;
            movie.ReleaseDate = request.ReleaseDate;
            movie.AvailableInCinema = request.AvailableInCinema;

            _repository.Update(movie);
            await _repository.SaveChangesAsync();

            return MapToDtoV2(movie);
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id)
                ?? throw new MovieNotFoundException($"Movie with id {id} not found.");

            _repository.Delete(movie);
            await _repository.SaveChangesAsync();
        }

        private static MovieDtoV2 MapToDtoV2(Movie movie) => new()
        {
            Id = movie.Id,
            Name = movie.Name,
            Genre = movie.Genre,
            ReleaseDate = movie.ReleaseDate,
            AvailableInCinema = movie.AvailableInCinema
        };
    }
}
