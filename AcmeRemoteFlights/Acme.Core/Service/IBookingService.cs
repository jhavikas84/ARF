using Acme.Core.DTO;
using Acme.Data.Repositories.UnitOfWork;
using AutoMapper;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Acme.Core.Service
{
    public interface IBookingService
    {
        int CreateBooking(IUnitOfWork unitOfWork, IMapper mapper, BookingDTO booking);

        IList<BookingDTO> GetBookingDetails(IUnitOfWork unitOfWork, IMapper mapper, IDictionary<string, string> filters);
    }
}
