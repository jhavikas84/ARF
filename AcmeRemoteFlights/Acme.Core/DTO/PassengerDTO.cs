using System.Text;

namespace Acme.Core.DTO
{
    public class PassengerDTO
    {
        public int PassengerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PassengerName => GetPassengerName();
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        private string GetPassengerName()
        {
            StringBuilder sb = new StringBuilder(FirstName);

            if (!string.IsNullOrEmpty(MiddleName))
            {
                sb.Append($" {MiddleName}");
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                sb.Append($" {LastName}");
            }

            return sb.ToString();
        }
    }
}
