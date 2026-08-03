using CinemaBookingAPI.Models.DTOs;

namespace CinemaBookingAPI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllAsync(int? customerId, string? customerName, int? showTimeId, string? status);
        Task<BookingDto> GetByIdAsync(int id);
        Task<BookingDto> CreateAsync(CreateBookingRequest request);
        Task CancelAsync(int id);
    }
}