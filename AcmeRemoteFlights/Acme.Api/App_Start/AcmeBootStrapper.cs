using System.Web.Hosting;
using System.Web.Http;
using Unity.WebApi;

namespace Acme.Api.App_Start
{
    public class AcmeBootStrapper : IRegisteredObject
    {
        private static AcmeBootStrapper _Instance = null;
        private static readonly object _SyncLock = new object();

        private AcmeBootStrapper()
        {
        }

        public static AcmeBootStrapper Instance
        {
            get
            {
                lock (_SyncLock)
                {
                    if (_Instance == null)
                    {
                        lock (_SyncLock)
                        {
                            if (_Instance == null)
                            {
                                _Instance = new AcmeBootStrapper();
                            }
                        }
                    }
                }

                return _Instance;
            }
        }
        public void Start(HttpConfiguration config)
        {
            HostingEnvironment.RegisterObject(this);

            var container = IocConfiguration.GetIocConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public void Stop()
        {
            lock (_SyncLock)
            {
                HostingEnvironment.UnregisterObject(this);
            }
        }

        public void Stop(bool immediate)
        {
            Stop();
        }
    }
}