using System;
using System.Collections.Generic;

namespace Acme.Core.DTO
{
    public class BookingDTO
    {
        public List<PassengerDTO> PassengerList { get; set; }

        public int BookingId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal? BookingAmount { get; set; }

    }
}
