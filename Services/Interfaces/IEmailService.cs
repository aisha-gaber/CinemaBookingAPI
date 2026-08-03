namespace CinemaBookingAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(string customerEmail, string customerName, int bookingId);
        Task SendBookingCancellationAsync(string customerEmail, string customerName, int bookingId);
    }
}