using Acme.Core.Service;
using Acme.Data.Repositories.UnitOfWork;
using Unity;
using Unity.Lifetime;

namespace Acme.Api.App_Start
{
    public class IocConfiguration
    {
        private static UnityContainer _UnityContainer;
        public static UnityContainer GetIocConfiguration()
        {
            if (_UnityContainer != null)
            {
                return _UnityContainer;
            }

            _UnityContainer = new UnityContainer();

            _UnityContainer.RegisterType<IUnitOfWork, UnitOfWork>(new TransientLifetimeManager());
            _UnityContainer.RegisterType<IMapperRegister, MapperRegister>(new HierarchicalLifetimeManager());

            RegisterServices(_UnityContainer);

            return _UnityContainer;
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IFlightService, FlightSevice>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookingService, BookingService>(new HierarchicalLifetimeManager());
        }
    }
}