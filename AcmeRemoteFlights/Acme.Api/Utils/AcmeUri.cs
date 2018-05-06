using System.Configuration;

namespace Acme.Api.Utils
{
    public class AcmeUri
    {
        public static readonly string BaseUri = ConfigurationManager.AppSettings["BaseUrl"].ToString();

        public class FlightUri
        {
            public const string AllFlights = @"api/flights";
            public const string AvailableFlights = @"api/availableFlights";
        }

        public class PassengerUri
        {
            public const string Passenger = @"api/passengers";
        }

        public class BookingUri
        {
            public const string CreateBooking = "api/createBooking";
            public const string AllBookingsByFilter = "api/bookingsByFilter";
            public const string AllBookingsByPassengerName = "api/bookingsByPassengerName/{0}";
            public const string AllBookingsByDate = @"api/bookingsByDate/{0}";
            public const string AllBookingsByArrivalCity = @"api/bookingsByArrivalCity/{0}";
            public const string AllBookingsByDepartureCity = @"api/bookingsByDepartureCity{0}";
            public const string AllBookingsByFlightNumber = @"api/bookingsByFlightNumber{0}";
        }
    }
}