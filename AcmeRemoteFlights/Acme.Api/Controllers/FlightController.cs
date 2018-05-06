using Acme.Api.App_Start;
using Acme.Data.Repositories.UnitOfWork;
using System;
using System.Web.Http;
using Acme.Core.Service;
using Acme.Api.Utils;

namespace Acme.Api.Controllers
{
    public class FlightController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapperRegister _Mapper;
        private readonly IFlightService _FlightService;

        public FlightController(IUnitOfWork unitOfWork, IMapperRegister mapper, IFlightService flightService)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _FlightService = flightService;
        }

        [Route(AcmeUri.FlightUri.AllFlights)]
        public IHttpActionResult GetFlights()
        {
            try
            {
                var flights = _FlightService.GetFlights(_UnitOfWork, _Mapper.GetMapper());

                if (flights?.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route(AcmeUri.FlightUri.AvailableFlights)]
        public IHttpActionResult GetAvailableFlights(TimeSpan startTime, TimeSpan endTime, int passengerCount)
        {
            try
            {
                var flights = _FlightService.GetFlights(_UnitOfWork, _Mapper.GetMapper(), 
                                                        startTime, endTime, passengerCount);

                if (flights?.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
