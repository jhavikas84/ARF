using Acme.Api.App_Start;
using Acme.Data.Repositories.UnitOfWork;
using System;
using System.Web.Http;
using Acme.Core.Service;
using Acme.Api.Utils;
using Acme.Core.DTO;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Acme.Api.Controllers
{
    public class BookingController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapperRegister _Mapper;
        private readonly IBookingService _BookingService;

        public BookingController(IUnitOfWork unitOfWork, IMapperRegister mapper, IBookingService bookingService)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _BookingService = bookingService;
        }

        [Route(AcmeUri.BookingUri.CreateBooking)]
        [HttpPost]
        public IHttpActionResult CreateBooking(BookingDTO booking)
        {
            try
            {
                var bookingId = _BookingService.CreateBooking(_UnitOfWork, _Mapper.GetMapper(), booking);

                if (bookingId > 0)
                {
                    return Ok(bookingId);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route(AcmeUri.BookingUri.AllBookingsByFilter)]
        [HttpGet]
        public IHttpActionResult GetBookingDetail(string passengerName = "", string bookingDate = "", string arrivalCity = "",
                                                  string departureCity = "", string flightNumber = "")
        {
            try
            {
                //NameValueCollection filters = new NameValueCollection();
                IDictionary<string, string> filters = new Dictionary<string, string>();

                #region [ Adding Filters ]

                if (!string.IsNullOrEmpty(passengerName))
                {
                    filters.Add(BookingService.BookingFilter.PassengerName.ToString(), passengerName);
                }

                if (!string.IsNullOrEmpty(bookingDate))
                {
                    if (DateTime.TryParse(bookingDate, out DateTime theBookingDate))
                    {
                        filters.Add(BookingService.BookingFilter.BookingDate.ToString(), bookingDate);
                    }
                    else
                    {
                        throw new Exception($"Invalid booking date parameter value: {bookingDate}");
                    }
                }

                if (!string.IsNullOrEmpty(arrivalCity))
                {
                    filters.Add(BookingService.BookingFilter.ArrivalCity.ToString(), arrivalCity);
                }

                if (!string.IsNullOrEmpty(departureCity))
                {
                    filters.Add(BookingService.BookingFilter.DepartureCity.ToString(), departureCity);
                }

                if (!string.IsNullOrEmpty(flightNumber))
                {
                    filters.Add(BookingService.BookingFilter.FlightNumber.ToString(), flightNumber);
                }

                #endregion 

                var bookingDTO = _BookingService.GetBookingDetails(_UnitOfWork, _Mapper.GetMapper(), filters);

                if (bookingDTO.Count > 0)
                {
                    return Ok(bookingDTO);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}