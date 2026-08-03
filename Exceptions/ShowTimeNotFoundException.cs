namespace CinemaBookingAPI.Exceptions
{
    public class ShowTimeNotFoundException : Exception
    {
        public ShowTimeNotFoundException(string message) : base(message) { }
    }
}
