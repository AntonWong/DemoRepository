using System.Web.Mvc;
using System.Web.Routing;
using ClientWeb.App_Start;

namespace ClientWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
