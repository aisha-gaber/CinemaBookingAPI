namespace CinemaBookingAPI.Models.DTOs
{
    // v1: compact
    public class MovieDtoV1
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool AvailableInCinema { get; set; }
    }

    // v2: detailed
    public class MovieDtoV2
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
    }

    public class CreateMovieRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
    }

    public class UpdateMovieRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
    }
}