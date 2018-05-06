using Acme.Core.DTO;
using Acme.Data.Repositories.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Api.App_Start
{
    public class MapperRegister : IMapperRegister
    {
        private readonly IMapper _mapper;

        public MapperRegister()
        {
            MapperConfiguration map = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDTO, Flight>().ReverseMap();
                //cfg.CreateMap<Flight, FlightDTO>().ReverseMap();

                cfg.CreateMap<BookingDTO, Booking>().ReverseMap();
                //cfg.CreateMap<Booking, BookingDTO>().ReverseMap();

                cfg.CreateMap<PassengerDTO, Passenger>().ReverseMap();
                //cfg.CreateMap<Passenger, PassengerDTO>().ReverseMap();

                cfg.CreateMap<PassengerBookingDTO, PassengerBooking>().ReverseMap();
                //cfg.CreateMap<PassengerBooking, PassengerBookingDTO>().ReverseMap();

            });

            _mapper = map.CreateMapper();
        }
        public IMapper GetMapper()
        {
            return _mapper;
        }

        public TMappedTo Map<TMappedFrom, TMappedTo>(TMappedFrom from)
        {
            return _mapper.Map<TMappedFrom, TMappedTo>(from);
        }
    }
}