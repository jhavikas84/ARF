using System;
using System.Data.Entity.Validation;
using System.Linq;
using Acme.Data.Repositories.Entities;
using Acme.Data.Repositories.Repository;
using Acme.Data.Repositories.Utils;

namespace Acme.Data.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region [ Disposable Pattern ]

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _AcmeEntities.Dispose();
                }
            }
            _Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private AcmeDBEntities _AcmeEntities = new AcmeDBEntities();
        private IRepository<Booking> _BookingRepository;
        private IRepository<Flight> _FlightRepository;
        private IRepository<Passenger> _PassengerRepository;
        private IRepository<PassengerBooking> _PassengerBookingRepository;
        private IRepository<AcmeErrorLog> _ExceptionRepository;

        public IRepository<Booking> BookingRepository => _BookingRepository ?? (_BookingRepository = new Repository<Booking>(_AcmeEntities));
        public IRepository<Flight> FlightRepository => _FlightRepository ?? (_FlightRepository = new Repository<Flight>(_AcmeEntities));
        public IRepository<Passenger> PassengerRepository => _PassengerRepository ?? (_PassengerRepository = new Repository<Passenger>(_AcmeEntities));
        public IRepository<PassengerBooking> PassengerBookingRepository => _PassengerBookingRepository ?? (_PassengerBookingRepository = new Repository<PassengerBooking>(_AcmeEntities));
        public IRepository<AcmeErrorLog> ExceptionRepository => _ExceptionRepository ?? (_ExceptionRepository = new Repository<AcmeErrorLog>(_AcmeEntities));

        public void Save()
        {
            try
            {
                _AcmeEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                try
                {
                    ExceptionRepository.Add(Utility.GetErrorLogObject(ex, method.Name));
                    _AcmeEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    Utility.LogErrorInFile(e);
                }

                throw new DbEntityValidationException(
                    $@"{ex.Message} validation errors are {string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage))}",
                    ex.EntityValidationErrors);
            }
        }

        public void Refresh()
        {
            _AcmeEntities = new AcmeDBEntities();
            _BookingRepository = null;
            _PassengerRepository = null;
            _PassengerBookingRepository = null;
            _FlightRepository = null;
            _ExceptionRepository = null;
        }       
    }
}
