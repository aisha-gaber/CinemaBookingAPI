namespace CinemaBookingAPI.Models.Entities
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime ShowTimeValue { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}