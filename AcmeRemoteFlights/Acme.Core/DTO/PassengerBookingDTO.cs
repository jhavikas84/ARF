namespace Acme.Core.DTO
{
    public class PassengerBookingDTO
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public int? PassengerId { get; set; }
    }
}
