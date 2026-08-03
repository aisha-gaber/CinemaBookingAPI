namespace CinemaBookingAPI.Models.Entities
{
    public class Movie

    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }

        public ICollection<ShowTime> Shows { get; set; } = new List<ShowTime>();
    }
}