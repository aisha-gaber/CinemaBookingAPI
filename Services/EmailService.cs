using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendBookingConfirmationAsync(string customerEmail, string customerName, int bookingId)
        {
            _logger.LogInformation(
                "Email sent to {Email}: Hi {Name}, your booking #{BookingId} is confirmed!",
                customerEmail, customerName, bookingId);
            return Task.CompletedTask;
        }

        public Task SendBookingCancellationAsync(string customerEmail, string customerName, int bookingId)
        {
            _logger.LogInformation(
                "Email sent to {Email}: Hi {Name}, your booking #{BookingId} has been cancelled.",
                customerEmail, customerName, bookingId);
            return Task.CompletedTask;
        }
    }
}