using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector.Integration.WebApi;
using System.Web;
using System.Web.Optimization;
using SimpleInjector.Integration.Web.Mvc;
using Status.DAL;

namespace Status.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var context = new StatusContext("StatusDBConnectionString");
            var container = SimpleInjectorConfig.CreateContainer(context);
            
           
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
