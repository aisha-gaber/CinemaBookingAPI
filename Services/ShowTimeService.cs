using CinemaBookingAPI.Exceptions;
using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowTimeRepository _repository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAuditoriumRepository _auditoriumRepository;

        public ShowTimeService(IShowTimeRepository repository, IMovieRepository movieRepository, IAuditoriumRepository auditoriumRepository)
        {
            _repository = repository;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
        }

        public async Task<List<ShowTimeDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<ShowTimeDto> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id)
                ?? throw new ShowTimeNotFoundException($"ShowTime with id {id} not found.");
            return MapToDto(item);
        }

        public async Task<ShowTimeDto> CreateAsync(CreateShowTimeRequest request)
        {
            var movie = await _movieRepository.GetByIdAsync(request.MovieId)
                ?? throw new MovieNotFoundException($"Movie with id {request.MovieId} not found.");
            var auditorium = await _auditoriumRepository.GetByIdAsync(request.AuditoriumId)
                ?? throw new Exception($"Auditorium with id {request.AuditoriumId} not found.");

            var showTime = new ShowTime
            {
                ShowTimeValue = request.ShowTimeValue,
                MovieId = request.MovieId,
                AuditoriumId = request.AuditoriumId
            };

            await _repository.AddAsync(showTime);
            await _repository.SaveChangesAsync();

            showTime.Movie = movie;
            showTime.Auditorium = auditorium;

            return MapToDto(showTime);
        }

        public async Task<ShowTimeDto> UpdateAsync(int id, UpdateShowTimeRequest request)
        {
            var showTime = await _repository.GetByIdAsync(id)
                ?? throw new ShowTimeNotFoundException($"ShowTime with id {id} not found.");

            showTime.ShowTimeValue = request.ShowTimeValue;
            showTime.MovieId = request.MovieId;
            showTime.AuditoriumId = request.AuditoriumId;

            _repository.Update(showTime);
            await _repository.SaveChangesAsync();

            return MapToDto(showTime);
        }

        public async Task DeleteAsync(int id)
        {
            var showTime = await _repository.GetByIdAsync(id)
                ?? throw new ShowTimeNotFoundException($"ShowTime with id {id} not found.");

            _repository.Delete(showTime);
            await _repository.SaveChangesAsync();
        }

        private static ShowTimeDto MapToDto(ShowTime s) => new()
        {
            Id = s.Id,
            ShowTimeValue = s.ShowTimeValue,
            MovieId = s.MovieId,
            MovieName = s.Movie?.Name ?? "",
            AuditoriumId = s.AuditoriumId,
            AuditoriumRoomNumber = s.Auditorium?.RoomNumber ?? 0
        };
    }
}