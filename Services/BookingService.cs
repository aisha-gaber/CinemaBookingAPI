using CinemaBookingAPI.Enums;
using CinemaBookingAPI.Exceptions;
using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly IEmailService _emailService;

        public BookingService(IBookingRepository repository, ICustomerRepository customerRepository,
            IShowTimeRepository showTimeRepository, IEmailService emailService)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _showTimeRepository = showTimeRepository;
            _emailService = emailService;
        }

        public async Task<List<BookingDto>> GetAllAsync(int? customerId, string? customerName, int? showTimeId, string? status)
        {
            var bookings = await _repository.GetAllAsync();

            if (customerId.HasValue)
                bookings = bookings.Where(b => b.CustomerId == customerId.Value).ToList();

            if (!string.IsNullOrWhiteSpace(customerName))
                bookings = bookings.Where(b => b.Customer.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (showTimeId.HasValue)
                bookings = bookings.Where(b => b.ShowTimeId == showTimeId.Value).ToList();

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                bookings = bookings.Where(b => b.Status == parsedStatus).ToList();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id)
                ?? throw new BookingNotFoundException($"Booking with id {id} not found.");
            return MapToDto(booking);
        }

        public async Task<BookingDto> CreateAsync(CreateBookingRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId)
                ?? throw new CustomerNotFoundException($"Customer with id {request.CustomerId} not found.");

            var showTime = await _showTimeRepository.GetByIdAsync(request.ShowTimeId)
                ?? throw new ShowTimeNotFoundException($"ShowTime with id {request.ShowTimeId} not found.");

            var booking = new Booking
            {
                BookingDate = DateTime.UtcNow,
                Status = BookingStatus.Pending,
                CustomerId = request.CustomerId,
                ShowTimeId = request.ShowTimeId
            };

            await _repository.AddAsync(booking);
            await _repository.SaveChangesAsync();

            booking.Customer = customer;
            booking.ShowTime = showTime;

            await _emailService.SendBookingConfirmationAsync(customer.Email, customer.Name, booking.Id);

            return MapToDto(booking);
        }

        public async Task CancelAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id)
                ?? throw new BookingNotFoundException($"Booking with id {id} not found.");

            booking.Status = BookingStatus.Cancelled;
            _repository.Update(booking);
            await _repository.SaveChangesAsync();

            await _emailService.SendBookingCancellationAsync(booking.Customer.Name, booking.Customer.Name, booking.Id);
        }

        private static BookingDto MapToDto(Booking b) => new()
        {
            Id = b.Id,
            BookingDate = b.BookingDate,
            Status = b.Status,
            CustomerId = b.CustomerId,
            CustomerName = b.Customer?.Name ?? "",
            ShowTimeId = b.ShowTimeId
        };
    }
}