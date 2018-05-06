using Acme.Data.Repositories.Entities;
using Acme.Data.Repositories.Repository;
using System;

namespace Acme.Data.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Booking> BookingRepository { get; }
        IRepository<Flight> FlightRepository { get; }
        IRepository<Passenger> PassengerRepository { get; }
        IRepository<PassengerBooking> PassengerBookingRepository { get; }
        IRepository<AcmeErrorLog> ExceptionRepository { get; }
        void Save();
        void Refresh();
    }
}
