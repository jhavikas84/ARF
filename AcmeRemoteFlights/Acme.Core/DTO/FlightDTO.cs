using System;

namespace Acme.Core.DTO
{
    public class FlightDTO
    {
        public string FlightNumber { get; set; }
        public string FlightName { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SeatCapacity { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public decimal BasePrice { get; set; }
        public double? TaxPercentage { get; set; }
        public double? DiscountRate { get; set; }
    }
}
