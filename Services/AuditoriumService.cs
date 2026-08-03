using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IAuditoriumRepository _repository;

        public AuditoriumService(IAuditoriumRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AuditoriumDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<AuditoriumDto> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id)
                ?? throw new Exception($"Auditorium with id {id} not found.");
            return MapToDto(item);
        }

        public async Task<AuditoriumDto> CreateAsync(CreateAuditoriumRequest request)
        {
            var auditorium = new Auditorium
            {
                RoomNumber = request.RoomNumber,
                Capacity = request.Capacity,
                Available = request.Available
            };

            await _repository.AddAsync(auditorium);
            await _repository.SaveChangesAsync();

            return MapToDto(auditorium);
        }

        public async Task<AuditoriumDto> UpdateAsync(int id, UpdateAuditoriumRequest request)
        {
            var auditorium = await _repository.GetByIdAsync(id)
                ?? throw new Exception($"Auditorium with id {id} not found.");

            auditorium.RoomNumber = request.RoomNumber;
            auditorium.Capacity = request.Capacity;
            auditorium.Available = request.Available;

            _repository.Update(auditorium);
            await _repository.SaveChangesAsync();

            return MapToDto(auditorium);
        }

        public async Task DeleteAsync(int id)
        {
            var auditorium = await _repository.GetByIdAsync(id)
                ?? throw new Exception($"Auditorium with id {id} not found.");

            _repository.Delete(auditorium);
            await _repository.SaveChangesAsync();
        }

        private static AuditoriumDto MapToDto(Auditorium a) => new()
        {
            Id = a.Id,
            RoomNumber = a.RoomNumber,
            Capacity = a.Capacity,
            Available = a.Available
        };
    }
}
