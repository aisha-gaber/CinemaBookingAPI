namespace CinemaBookingAPI.Models.DTOs
{
    public class AuditoriumDto
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }

    public class CreateAuditoriumRequest
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }

    public class UpdateAuditoriumRequest
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }
}