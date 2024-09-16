using Owin;
using Microsoft.Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Builder;

[assembly: OwinStartup(typeof(A_AAsesoresAPI.Startup))]
namespace A_AAsesoresAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartOwin();
        }
        private void StartOwin()
        {
            // Crear un contexto de aplicación Owin
            var appBuilder = new AppBuilder();
            var startup = new Startup();
            startup.Configuration(appBuilder);
        }

    }
}
