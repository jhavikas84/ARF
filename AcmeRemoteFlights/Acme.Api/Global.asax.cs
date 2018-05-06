using Acme.Api.App_Start;
using System.Web.Http;

namespace Acme.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AcmeBootStrapper.Instance.Start(GlobalConfiguration.Configuration);
        }
    }
}
