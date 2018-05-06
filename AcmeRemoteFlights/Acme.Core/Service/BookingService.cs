using System;
using System.Collections.Specialized;
using Acme.Core.DTO;
using Acme.Data.Repositories.UnitOfWork;
using AutoMapper;
using System.Linq;
using Acme.Data.Repositories.Entities;
using System.Collections.Generic;

namespace Acme.Core.Service
{
    public class BookingService : IBookingService
    {
        public enum BookingFilter
        {
            PassengerName,
            BookingDate,
            ArrivalCity,
            DepartureCity,
            FlightNumber
        }

        public int CreateBooking(IUnitOfWork unitOfWork, IMapper mapper, BookingDTO booking)
        {
            try
            {
                // save booking
                var flightBooking = new Booking()
                {
                    BookingAmount = booking.BookingAmount,
                    BookingDate = booking.BookingDate,
                    FlightNumber = booking.FlightNumber
                };

                unitOfWork.BookingRepository.Add(flightBooking);
                unitOfWork.Save();

                var theBookingId = flightBooking.BookingId;
                int thePassengerId = 0;
                var passengers = new List<Passenger>();
                passengers.AddRange(mapper.Map<IList<PassengerDTO>, IList<Passenger>>(booking.PassengerList));

                foreach (var item in passengers)
                {
                    // check whether the passenger exists??
                    if (!unitOfWork.PassengerRepository.GetAll().Any(p => p.EmailId.Equals(item.EmailId)))
                    {
                        // Save the Passenger details
                        unitOfWork.PassengerRepository.Add(item);
                        unitOfWork.Save();
                        thePassengerId = item.PassengerId;
                    }
                    else
                    {
                        thePassengerId = unitOfWork.PassengerRepository.GetAll()
                                                   .Where(p => p.EmailId.Equals(item.EmailId))
                                                   .FirstOrDefault()
                                                   .PassengerId;
                    }

                    // Save Passenger Booking details
                    var passengerBooking = new PassengerBookingDTO()
                    {
                        BookingId = theBookingId,
                        PassengerId = thePassengerId
                    };

                    unitOfWork.PassengerBookingRepository.Add(mapper.Map<PassengerBookingDTO, PassengerBooking>(passengerBooking));
                    unitOfWork.Save();
                }

                return theBookingId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<BookingDTO> GetBookingDetails(IUnitOfWork unitOfWork, IMapper mapper, IDictionary<string, string> filters)
        {
            var bookingDetails = unitOfWork.BookingRepository.GetAll();

            foreach (var filter in filters)
            {
                if (filter.Key.Equals(BookingFilter.PassengerName.ToString(), StringComparison.InvariantCulture))
                {
                    var passenegrDetails = (from p in unitOfWork.PassengerRepository.GetAll()
                                            join pb in unitOfWork.PassengerBookingRepository.GetAll()
                                            on p.PassengerId equals pb.PassengerId
                                            where p.PassengerName.Equals(filter.Value, StringComparison.InvariantCulture)
                                            select pb).FirstOrDefault();

                    bookingDetails = unitOfWork.BookingRepository.GetAll().Where(b => b.BookingId == passenegrDetails.BookingId).ToList();
                }

                if (filter.Key.Equals(BookingFilter.BookingDate.ToString(), StringComparison.InvariantCulture))
                {
                    bookingDetails = unitOfWork.BookingRepository.GetAll().Where(b => b.BookingDate.Value.ToString("dd/MM/yyyy").Equals(filter.Value)).ToList();
                }

                if (filter.Key.Equals(BookingFilter.ArrivalCity.ToString(), StringComparison.InvariantCulture))
                {
                    bookingDetails = unitOfWork.BookingRepository.GetAll().Where(b => b.Flight.ArrivalCity.Equals(filter.Value)).ToList();
                }

                if (filter.Key.Equals(BookingFilter.DepartureCity.ToString(), StringComparison.InvariantCulture))
                {
                    bookingDetails = unitOfWork.BookingRepository.GetAll().Where(b => b.Flight.DepartureCity.Equals(filter.Value)).ToList();
                }

                if (filter.Key.Equals(BookingFilter.FlightNumber.ToString(), StringComparison.InvariantCulture))
                {
                    bookingDetails = unitOfWork.BookingRepository.GetAll().Where(b => b.FlightNumber.Equals(filter.Value)).ToList();
                }
            }


            var bookingList = new List<BookingDTO>();

            foreach (var item in bookingDetails)
            {
                BookingDTO bookingInformation = new BookingDTO
                {
                    PassengerList = new List<PassengerDTO>(),
                    BookingAmount = item.BookingAmount,
                    BookingDate = item.BookingDate.Value,
                    BookingId = item.BookingId,
                    FlightNumber = item.FlightNumber                    
                };

                foreach (var passenger in item.PassengerBookings)
                {
                    bookingInformation.PassengerList.Add(mapper.Map<Passenger, PassengerDTO>(passenger.Passenger));
                }

                bookingList.Add(bookingInformation);
            }

            return bookingList;

            //var booking = bookingDetails.FirstOrDefault();
            //if (booking != null)
            //{
            //    List<PassengerDTO> passengerList = new List<PassengerDTO>();

            //    foreach (var item in booking.PassengerBookings)
            //    {
            //        passengerList.Add(mapper.Map<Passenger, PassengerDTO>(item.Passenger));
            //    }

            //    var bookingInformation = mapper.Map<Booking, BookingDTO>(bookingDetails.FirstOrDefault());

            //    if (bookingInformation.PassengerList == null)
            //    {
            //        bookingInformation.PassengerList = new List<PassengerDTO>();
            //    }
            //    bookingInformation.PassengerList.AddRange(passengerList);

            //    return bookingInformation;
            //}
            //else
            //    return null;

        }
    }
}
