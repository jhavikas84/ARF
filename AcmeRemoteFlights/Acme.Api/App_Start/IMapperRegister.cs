using AutoMapper;

namespace Acme.Api.App_Start
{
    public interface IMapperRegister
    {
        IMapper GetMapper();
        TMappedTo Map<TMappedFrom, TMappedTo>(TMappedFrom from);
    }
}