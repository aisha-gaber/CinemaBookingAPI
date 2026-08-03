namespace CinemaBookingAPI.Models.DTOs
{
    public class ShowTimeDto
    {
        public int Id { get; set; }
        public DateTime ShowTimeValue { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public int AuditoriumId { get; set; }
        public int AuditoriumRoomNumber { get; set; }
    }

    public class CreateShowTimeRequest
    {
        public DateTime ShowTimeValue { get; set; }
        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }
    }

    public class UpdateShowTimeRequest
    {
        public DateTime ShowTimeValue { get; set; }
        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }
    }
}