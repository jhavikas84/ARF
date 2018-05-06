using System;
using System.Collections.Generic;
using Acme.Core.DTO;
using Acme.Data.Repositories.Entities;
using Acme.Data.Repositories.UnitOfWork;
using AutoMapper;
using System.Linq;

namespace Acme.Core.Service
{
    public class FlightSevice : IFlightService
    {
        public IList<FlightDTO> GetFlights(IUnitOfWork unitOfWork, IMapper mapper)
        {
            try
            {
                var flights = unitOfWork.FlightRepository.GetAll();
                return mapper.Map<IList<Flight>, IList<FlightDTO>>(flights);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<FlightDTO> GetFlights(IUnitOfWork unitOfWork, IMapper mapper, TimeSpan startTime, TimeSpan endTime, int passengerCount)
        {
            try
            {
                var availableFlights = (from f in unitOfWork.FlightRepository.GetAll()
                                        join pf in unitOfWork.PassengerBookingRepository.GetAll()
                                        on f.FlightNumber equals pf.Booking.FlightNumber into flightCount
                                        where f.StartTime >= startTime
                                        && f.EndTime <= endTime
                                        && f.SeatCapacity - flightCount.Count() > passengerCount
                                        select f).Distinct().ToList();

                return mapper.Map<IList<Flight>, IList<FlightDTO>>(availableFlights);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
