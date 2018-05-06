using Acme.Core.DTO;
using Acme.Data.Repositories.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Acme.Core.Service
{
    public interface IFlightService
    {
        IList<FlightDTO> GetFlights(IUnitOfWork unitOfWork, IMapper mapper);
        IList<FlightDTO> GetFlights(IUnitOfWork unitOfWork, IMapper mapper, TimeSpan startTime, TimeSpan endTime, int passengerCount);
    }
}
