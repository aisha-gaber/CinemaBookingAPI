using CinemaBookingAPI.Enums;

namespace CinemaBookingAPI.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int ShowTimeId { get; set; }
    }

    public class CreateBookingRequest
    {
        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }
    }
}